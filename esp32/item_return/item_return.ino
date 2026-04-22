/**
 * ESP32 + PN532 Item Return Sketch
 *
 * Flow:
 *   1. Connect to WiFi
 *   2. Login via POST /api/v1/auth/login-mobile → get JWT
 *   3. Scan item RFID tag → GET /api/v1/items/rfid/{uid} → resolve itemId
 *   4. GET /api/v1/lentItems/borrowed → find active lent item for this itemId
 *   5. PATCH /api/v1/lentItems/{lentItemId} with { status: "Returned", returnedAt: <now> }
 *      to mark the item as returned
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
#include <time.h>

// ── Config ────────────────────────────────────────────────────────────────────
#define WIFI_SSID      "EjGwapo2.4G"
#define WIFI_PASSWORD  "vigheadTHEG0D2.4G"
#define API_BASE_URL   "http://192.168.1.4:5289"

#define API_IDENTIFIER "christian"
#define API_PASSWORD   "@Password123"

// NTP server for getting current time
#define NTP_SERVER     "pool.ntp.org"
#define GMT_OFFSET_SEC 28800  // GMT+8 (Philippines)
#define DAYLIGHT_OFFSET_SEC 0

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

  Serial.println("\n=== RFID Item Return Station ===");

  nfc.begin();
  uint32_t ver = nfc.getFirmwareVersion();
  if (!ver) {
    Serial.println("ERROR: PN532 not found. Check wiring/I2C switches.");
    while (1);
  }
  Serial.printf("PN532 firmware v%d.%d\n", (ver >> 16) & 0xFF, (ver >> 8) & 0xFF);
  nfc.SAMConfig();

  connectWiFi();
  
  // Configure time
  configTime(GMT_OFFSET_SEC, DAYLIGHT_OFFSET_SEC, NTP_SERVER);
  Serial.println("Syncing time with NTP server...");
  delay(2000);
  
  login();

  Serial.println("\nReady — scan an item tag to mark it as returned.");
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
      returnItem(lentItemId);
    }
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

// Get current time in ISO 8601 format
String getCurrentTimestamp() {
  struct tm timeinfo;
  if (!getLocalTime(&timeinfo)) {
    Serial.println("Failed to obtain time");
    return "";
  }
  
  char buffer[25];
  strftime(buffer, sizeof(buffer), "%Y-%m-%dT%H:%M:%S", &timeinfo);
  return String(buffer);
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

// Calls GET /api/v1/lentItems/borrowed → finds active lent item for this itemId
String findActiveLentItem(const String& itemId) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); login(); }

  String url = String(API_BASE_URL) + "/api/v1/lentItems/borrowed";
  Serial.printf("GET %s\n", url.c_str());

  HTTPClient http;
  http.begin(url);
  http.addHeader("Authorization", "Bearer " + jwtToken);

  int code = http.GET();
  String resp = http.getString();
  http.end();

  Serial.printf("HTTP %d\n", code);

  if (code == 401) { Serial.println("Token expired, re-logging in..."); login(); return ""; }
  if (code != 200) { Serial.printf("✗ Failed to get borrowed items: %d\n", code); return ""; }

  DynamicJsonDocument doc(8192);
  deserializeJson(doc, resp);
  JsonArray items = doc["data"].as<JsonArray>();

  for (JsonObject item : items) {
    String lentItemItemId = item["item"]["id"].as<String>();
    if (lentItemItemId == itemId) {
      String lentItemId = item["id"].as<String>();
      String borrower = item["borrowerFullName"].as<String>();
      Serial.printf("✓ Found active lent item: %s (Borrower: %s)\n", lentItemId.c_str(), borrower.c_str());
      return lentItemId;
    }
  }

  Serial.println("✗ No active borrowed record found for this item.");
  return "";
}

// Calls PATCH /api/v1/lentItems/{lentItemId} with { status: "Returned", returnedAt: <now> }
void returnItem(const String& lentItemId) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); login(); }

  String timestamp = getCurrentTimestamp();
  if (timestamp.length() == 0) {
    Serial.println("✗ Cannot get current time, aborting return.");
    return;
  }

  String url = String(API_BASE_URL) + "/api/v1/lentItems/" + lentItemId;
  Serial.printf("PATCH %s\n", url.c_str());

  StaticJsonDocument<256> doc;
  doc["status"] = "Returned";
  doc["returnedAt"] = timestamp;
  String body;
  serializeJson(doc, body);

  HTTPClient http;
  http.begin(url);
  http.addHeader("Content-Type", "application/json");
  http.addHeader("Authorization", "Bearer " + jwtToken);

  int code = http.PATCH(body);
  String resp = http.getString();
  http.end();

  Serial.printf("HTTP %d — %s\n", code, resp.c_str());

  if (code == 200)
    Serial.printf("✓ Item marked as RETURNED at %s\n", timestamp.c_str());
  else if (code == 401) {
    Serial.println("✗ Unauthorized — re-logging in..."); login();
  } else if (code == 404)
    Serial.println("✗ Lent item not found.");
  else
    Serial.printf("✗ Unexpected: %d\n", code);
}
