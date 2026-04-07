/**
 * ESP32 + PN532 Location Tracking Sketch
 *
 * Flow:
 *   1. Connect to WiFi
 *   2. Login via POST /api/v1/auth/login-mobile → get JWT
 *   3. Scan item RFID tag → GET /api/v1/items/rfid/{uid} → resolve itemId
 *   4. PATCH /api/v1/items/{itemId} with { location: LOCATION_LABEL }
 *      to update the item's current physical location
 *
 * Each ESP32 unit is deployed at a fixed location (e.g. a room or cabinet).
 * Set LOCATION_LABEL to identify where this unit is installed.
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
#define WIFI_SSID      "EjGwapo2.4G"
#define WIFI_PASSWORD  "vigheadTHEG0D2.4G"
#define API_BASE_URL   "http://192.168.1.7:5289"

#define API_IDENTIFIER "admin"
#define API_PASSWORD   "@Pass123"

// Label for this unit's physical location — change per deployment
#define LOCATION_LABEL "Room 111"

// ── PN532 ─────────────────────────────────────────────────────────────────────
#define PN532_IRQ   4
#define PN532_RESET 2

Adafruit_PN532 nfc(PN532_IRQ, PN532_RESET);

// ── Globals ───────────────────────────────────────────────────────────────────
String jwtToken    = "";
String lastUid     = "";
unsigned long lastScanTime = 0;

#define SCAN_COOLDOWN_MS 2000

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
  login();

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
    updateLocation(itemId);
  }
}

// ── Helpers ───────────────────────────────────────────────────────────────────

void connectWiFi() {
  Serial.printf("Connecting to %s", WIFI_SSID);
  WiFi.begin(WIFI_SSID, WIFI_PASSWORD);
  while (WiFi.status() != WL_CONNECTED) { delay(500); Serial.print("."); }
  Serial.printf("\nConnected — IP: %s\n", WiFi.localIP().toString().c_str());
}

void login() {
  String url = String(API_BASE_URL) + "/api/v1/auth/login-mobile";
  Serial.printf("Logging in as %s...\n", API_IDENTIFIER);

  StaticJsonDocument<128> doc;
  doc["identifier"] = API_IDENTIFIER;
  doc["password"]   = API_PASSWORD;
  String body;
  serializeJson(doc, body);

  HTTPClient http;
  http.setConnectTimeout(5000);
  http.setTimeout(10000);
  http.begin(url);
  http.addHeader("Content-Type", "application/json");

  int code = http.POST(body);
  String resp = http.getString();
  http.end();

  if (code != 200) {
    Serial.printf("Login failed (%d) — halting.\n", code);
    while (1);
  }

  StaticJsonDocument<1024> respDoc;
  deserializeJson(respDoc, resp);
  jwtToken = respDoc["data"]["accessToken"].as<String>();
  Serial.println("Login successful.");
}

String getUidString(uint8_t* buf, uint8_t len) {
  String s = "";
  for (uint8_t i = 0; i < len; i++) {
    if (buf[i] < 0x10) s += "0";
    s += String(buf[i], HEX);
  }
  s.toUpperCase();
  return s;
}

// Calls GET /api/v1/items/rfid/{uid} → returns itemId GUID or empty string on failure
String resolveItem(const String& rfidUid) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); login(); }

  String url = String(API_BASE_URL) + "/api/v1/items/rfid/" + rfidUid;
  Serial.printf("GET %s\n", url.c_str());

  HTTPClient http;
  http.begin(url);
  http.addHeader("Authorization", "Bearer " + jwtToken);

  int code = http.GET();
  String resp = http.getString();
  http.end();

  Serial.printf("HTTP %d\n", code);

  if (code == 401) { Serial.println("Token expired, re-logging in..."); login(); return ""; }
  if (code == 404) { Serial.println("✗ Item not registered."); return ""; }
  if (code != 200) { Serial.printf("✗ Unexpected error: %d\n", code); return ""; }

  StaticJsonDocument<512> doc;
  deserializeJson(doc, resp);
  String id       = doc["data"]["id"].as<String>();
  String itemName = doc["data"]["itemName"].as<String>();
  Serial.printf("✓ Item: %s (ID: %s)\n", itemName.c_str(), id.c_str());
  return id;
}

// Calls POST /api/v1/items/{itemId}/update-location with { location }
void updateLocation(const String& itemId) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); login(); }

  String url = String(API_BASE_URL) + "/api/v1/items/" + itemId + "/update-location";
  Serial.printf("POST %s\n", url.c_str());

  StaticJsonDocument<128> doc;
  doc["location"] = LOCATION_LABEL;
  String body;
  serializeJson(doc, body);

  HTTPClient http;
  http.begin(url);
  http.addHeader("Content-Type", "application/json");
  http.addHeader("Authorization", "Bearer " + jwtToken);

  int code = http.POST(body);
  String resp = http.getString();
  http.end();

  Serial.printf("HTTP %d — %s\n", code, resp.c_str());

  if (code == 200 || code == 204)
    Serial.printf("✓ Location updated to \"%s\"\n", LOCATION_LABEL);
  else if (code == 401) {
    Serial.println("✗ Unauthorized — re-logging in..."); login();
  } else if (code == 404)
    Serial.println("✗ Item not found.");
  else
    Serial.printf("✗ Unexpected: %d\n", code);
}
