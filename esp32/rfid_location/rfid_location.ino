/**
 * ESP32 + PN532 Location Tracking Sketch
 *
 * Flow:
 *   1. Connect to WiFi
 *   2. Scan item RFID tag → GET /api/v1/items/rfid/{uid}  → resolve itemId
 *   3. GET /api/v1/lentItems/borrowed                     → find active lent record
 *   4. PATCH /api/v1/lentItems/{lentItemId} { room: LOCATION_LABEL }
 *
 * This unit is always-on and passive — no web trigger needed.
 * Deploy one unit per room/location. Set LOCATION_LABEL per unit.
 *
 * All endpoints are AllowAnonymous — no login required.
 *
 * Wiring (PN532 → ESP32) — I2C mode:
 *   VCC → 3.3V  |  GND → GND
 *   SDA → GPIO 21  |  SCL → GPIO 22
 *   IRQ → GPIO 4   |  RSTO → GPIO 2
 *
 * Libraries: Adafruit PN532, ArduinoJson
 */

#include <Wire.h>
#include <Adafruit_PN532.h>
#include <WiFi.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>

// ── Config ────────────────────────────────────────────────────────────────────
#define WIFI_SSID        "EjGwapo2.4G"
#define WIFI_PASSWORD    "vigheadTHEG0D2.4G"
#define API_BASE_URL     "http://192.168.1.17:5289"

// Label for this unit's physical location — change per deployment
#define LOCATION_LABEL   "Room 111"

#define SCAN_COOLDOWN_MS 2000

// ── PN532 ─────────────────────────────────────────────────────────────────────
#define PN532_IRQ   4
#define PN532_RESET 2

Adafruit_PN532 nfc(PN532_IRQ, PN532_RESET);

// ── Globals ───────────────────────────────────────────────────────────────────
String lastUid         = "";
unsigned long lastScanTime = 0;

// ─────────────────────────────────────────────────────────────────────────────

void setup() {
  Serial.begin(115200);
  delay(500);

  Serial.println("\n=== RFID Location Tracker ===");
  Serial.printf("Location: %s\n", LOCATION_LABEL);

  nfc.begin();
  uint32_t ver = nfc.getFirmwareVersion();
  if (!ver) {
    Serial.println("ERROR: PN532 not found. Check wiring/I2C switches.");
    while (1);
  }
  Serial.printf("PN532 firmware v%d.%d\n", (ver >> 16) & 0xFF, (ver >> 8) & 0xFF);
  nfc.SAMConfig();

  connectWiFi();

  Serial.println("\nReady — scan an item tag to update its location.");
}

void loop() {
  uint8_t uid[7];
  uint8_t uidLength;

  if (!nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A, uid, &uidLength, 1000)) return;

  String uidStr = getUidString(uid, uidLength);
  unsigned long now = millis();

  if (uidStr == lastUid && (now - lastScanTime) < SCAN_COOLDOWN_MS) return;
  lastUid      = uidStr;
  lastScanTime = now;

  Serial.printf("\nScanned tag: %s\n", uidStr.c_str());

  String itemId = resolveItem(uidStr);
  if (itemId.length() > 0) {
    String lentItemId = findActiveLentItem(itemId);
    if (lentItemId.length() > 0) {
      updateLocation(lentItemId);
    }
  }

  // Reconnect WiFi after each scan to clear stale socket state
  WiFi.disconnect(false);
  delay(300);
  WiFi.reconnect();
  unsigned long start = millis();
  while (WiFi.status() != WL_CONNECTED && millis() - start < 5000) { delay(200); }
}

// ── WiFi ──────────────────────────────────────────────────────────────────────

void connectWiFi() {
  Serial.printf("Connecting to %s", WIFI_SSID);
  WiFi.begin(WIFI_SSID, WIFI_PASSWORD);
  while (WiFi.status() != WL_CONNECTED) { delay(500); Serial.print("."); }
  Serial.printf("\nConnected — IP: %s\n", WiFi.localIP().toString().c_str());
}

// ── UID Helper ────────────────────────────────────────────────────────────────

String getUidString(uint8_t* buf, uint8_t len) {
  String s = "";
  for (uint8_t i = 0; i < len; i++) { if (buf[i] < 0x10) s += "0"; s += String(buf[i], HEX); }
  s.toUpperCase();
  return s;
}

// ── API Calls ─────────────────────────────────────────────────────────────────

// GET /api/v1/items/rfid/{uid} → returns itemId or empty string
String resolveItem(const String& rfidUid) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); }

  String url = String(API_BASE_URL) + "/api/v1/items/rfid/" + rfidUid;
  Serial.printf("GET %s\n", url.c_str());

  HTTPClient http;
  http.setConnectTimeout(5000);
  http.setTimeout(10000);
  http.begin(url);

  int code = http.GET();
  String resp = http.getString();
  http.end();

  Serial.printf("HTTP %d\n", code);

  if (code == 404) { Serial.println("✗ Item not registered."); return ""; }
  if (code != 200) { Serial.printf("✗ Unexpected error: %d\n", code); return ""; }

  StaticJsonDocument<512> doc;
  deserializeJson(doc, resp);
  String id       = doc["data"]["id"].as<String>();
  String itemName = doc["data"]["itemName"].as<String>();
  Serial.printf("✓ Item: %s (ID: %s)\n", itemName.c_str(), id.c_str());
  return id;
}

// GET /api/v1/lentItems/borrowed → finds active lent record for this itemId
String findActiveLentItem(const String& itemId) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); }

  String url = String(API_BASE_URL) + "/api/v1/lentItems/borrowed";
  Serial.printf("GET %s\n", url.c_str());

  HTTPClient http;
  http.setConnectTimeout(5000);
  http.setTimeout(10000);
  http.begin(url);

  int code = http.GET();
  String resp = http.getString();
  http.end();

  Serial.printf("HTTP %d\n", code);

  if (code != 200) { Serial.printf("✗ Failed to get borrowed items: %d\n", code); return ""; }

  DynamicJsonDocument doc(4096);
  DeserializationError err = deserializeJson(doc, resp);
  if (err) { Serial.printf("✗ JSON parse error: %s\n", err.c_str()); return ""; }

  JsonArray items = doc["data"].as<JsonArray>();
  for (JsonObject item : items) {
    if (String(item["item"]["id"].as<String>()) == itemId) {
      String lentItemId = item["id"].as<String>();
      Serial.printf("✓ Found active lent item: %s\n", lentItemId.c_str());
      return lentItemId;
    }
  }

  Serial.println("✗ No active borrowed record found for this item.");
  return "";
}

// PATCH /api/v1/lentItems/{lentItemId} → updates room to LOCATION_LABEL
void updateLocation(const String& lentItemId) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); }

  String url = String(API_BASE_URL) + "/api/v1/lentItems/" + lentItemId;
  Serial.printf("PATCH %s\n", url.c_str());

  StaticJsonDocument<128> doc;
  doc["room"] = LOCATION_LABEL;
  String body;
  serializeJson(doc, body);

  HTTPClient http;
  http.setConnectTimeout(5000);
  http.setTimeout(10000);
  http.begin(url);
  http.addHeader("Content-Type", "application/json");

  int code = http.PATCH(body);
  http.end();

  Serial.printf("HTTP %d\n", code);

  if (code == 200)
    Serial.printf("✓ Location updated to \"%s\"\n", LOCATION_LABEL);
  else if (code == 404)
    Serial.println("✗ Lent item not found.");
  else
    Serial.printf("✗ Unexpected: %d\n", code);
}
