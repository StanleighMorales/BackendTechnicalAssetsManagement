/**
 * ESP32 + PN532 RFID Registration Sketch
 * NFC Tag: NTAG213 (or any ISO14443A tag)
 *
 * Flow:
 *   1. Connect to WiFi
 *   2. Login via POST /api/v1/auth/login-mobile → get JWT
 *   3. Scan tag → POST /api/v1/items/{itemId}/register-rfid (Bearer token)
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
#define WIFI_SSID      "YOUR_WIFI_SSID"
#define WIFI_PASSWORD  "YOUR_WIFI_PASSWORD"
#define API_BASE_URL   "http://172.20.10.X:5289"   // your PC's IP

#define API_IDENTIFIER "admin"
#define API_PASSWORD   "@Pass123"

#define TARGET_ITEM_ID "982173E2-5096-4717-8DAB-20F76B1F4BDC"

// ── PN532 ─────────────────────────────────────────────────────────────────────
#define PN532_IRQ   4
#define PN532_RESET 2

Adafruit_PN532 nfc(PN532_IRQ, PN532_RESET);

// ── State ─────────────────────────────────────────────────────────────────────
String jwtToken    = "";
bool   registered  = false;

// ─────────────────────────────────────────────────────────────────────────────

void setup() {
  Serial.begin(115200);
  delay(500);

  Serial.println("\n=== RFID Registration Mode ===");
  Serial.printf("Target item: %s\n\n", TARGET_ITEM_ID);

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

  Serial.println("\nReady — scan a tag to register it...");
}

void loop() {
  if (registered) { delay(1000); return; }

  uint8_t uid[7];
  uint8_t uidLength;

  if (!nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A, uid, &uidLength, 1000)) return;

  String uidStr = getUidString(uid, uidLength);
  Serial.printf("\nTag: %s\n", uidStr.c_str());

  bool ok = registerRfid(uidStr);
  if (ok) {
    Serial.println("✓ Registered successfully.");
    registered = true;
  } else {
    Serial.println("✗ Failed. Retrying in 2s...");
    delay(2000);
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
  String loginUrl = String(API_BASE_URL) + "/api/v1/auth/login-mobile";
  Serial.printf("Logging in as %s...\n", API_IDENTIFIER);
  Serial.printf("URL: %s\n", loginUrl.c_str());

  StaticJsonDocument<128> doc;
  doc["identifier"] = API_IDENTIFIER;
  doc["password"]   = API_PASSWORD;
  String body;
  serializeJson(doc, body);

  HTTPClient http;
  http.setConnectTimeout(5000);
  http.setTimeout(10000);
  http.begin(loginUrl);
  http.addHeader("Content-Type", "application/json");

  int code = http.POST(body);
  String resp = http.getString();
  http.end();

  Serial.printf("HTTP code: %d\n", code);
  Serial.printf("Response: %s\n", resp.c_str());

  if (code != 200) {
    Serial.println("Login failed — halting.");
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

bool registerRfid(const String& rfidUid) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); login(); }

  String url = String(API_BASE_URL) + "/api/v1/items/" + TARGET_ITEM_ID + "/register-rfid";
  Serial.printf("POST %s\n", url.c_str());

  StaticJsonDocument<128> doc;
  doc["rfidUid"] = rfidUid;
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

  if (code == 401) { Serial.println("Token expired, re-logging in..."); login(); return false; }
  return code == 200;
}
