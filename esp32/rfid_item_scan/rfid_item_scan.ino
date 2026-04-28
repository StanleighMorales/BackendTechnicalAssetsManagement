/**
 * ESP32 + PN532 Item Scan Sketch — Guest Borrow / Reserve
 *
 * Flow:
 *   1. Connect to WiFi
 *   2. Poll GET /api/v1/item-scan-sessions/pending every 2s  ← idles until web triggers
 *   3. Session found → scan item NFC tag
 *   4. GET /api/v1/items/rfid/{uid} → resolve itemId + itemName
 *   5. POST /api/v1/item-scan-sessions/{id}/complete → pass itemId + itemName back to web
 *   6. Web receives itemId → opens guest borrow/reserve form pre-filled with item
 *   7. Reset → back to polling
 *
 * The ESP32 does NOT create the lent record.
 * Staff fills in guest details on the web and submits.
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

#define SCAN_COOLDOWN_MS 2000
#define POLL_INTERVAL_MS 2000

// ── PN532 ─────────────────────────────────────────────────────────────────────
#define PN532_IRQ   4
#define PN532_RESET 2

Adafruit_PN532 nfc(PN532_IRQ, PN532_RESET);

// ── State machine ─────────────────────────────────────────────────────────────
enum State { POLLING, WAIT_ITEM, SUBMITTING };

State  currentState  = POLLING;
String sessionId     = "";
String lastUid       = "";
unsigned long lastScanTime = 0;
unsigned long lastPollTime = 0;

// ─────────────────────────────────────────────────────────────────────────────

void setup() {
  Serial.begin(115200);
  delay(500);

  Serial.println("\n=== RFID Item Scan Station (Guest Borrow/Reserve) ===");

  nfc.begin();
  uint32_t ver = nfc.getFirmwareVersion();
  if (!ver) {
    Serial.println("ERROR: PN532 not found. Check wiring/I2C switches.");
    while (1);
  }
  Serial.printf("PN532 firmware v%d.%d\n", (ver >> 16) & 0xFF, (ver >> 8) & 0xFF);
  nfc.SAMConfig();

  connectWiFi();

  Serial.println("\nPolling for item scan sessions from web...");
}

void loop() {
  unsigned long now = millis();

  // ── POLLING: wait for web to create a session ─────────────────────────────
  if (currentState == POLLING) {
    if (now - lastPollTime >= POLL_INTERVAL_MS) {
      lastPollTime = now;
      checkPendingSession();
    }
    return;
  }

  // ── SUBMITTING: blocked until processItemScan() finishes ──────────────────
  if (currentState == SUBMITTING) return;

  // ── WAIT_ITEM: read item NFC tag ───────────────────────────────────────────
  uint8_t uid[7];
  uint8_t uidLength;
  if (!nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A, uid, &uidLength, 1000)) return;

  String uidStr = getUidString(uid, uidLength);
  if (uidStr == lastUid && (now - lastScanTime) < SCAN_COOLDOWN_MS) return;
  lastUid      = uidStr;
  lastScanTime = now;

  Serial.printf("\nScanned tag: %s\n", uidStr.c_str());

  currentState = SUBMITTING;
  processItemScan(uidStr);
  resetSession();
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

// Poll GET /api/v1/item-scan-sessions/pending — 204 = nothing, 200 = go
void checkPendingSession() {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); }

  String url = String(API_BASE_URL) + "/api/v1/item-scan-sessions/pending";
  HTTPClient http;
  http.setConnectTimeout(5000);
  http.setTimeout(10000);
  http.begin(url);

  int code = http.GET();
  String resp = http.getString();
  http.end();

  if (code == 204) return;  // nothing pending — keep polling silently
  if (code != 200) { Serial.printf("✗ Poll error: %d\n", code); return; }

  StaticJsonDocument<256> doc;
  deserializeJson(doc, resp);
  sessionId = doc["data"]["id"].as<String>();

  Serial.println("\n─────────────────────────────────────────");
  Serial.println("✓ Item scan session triggered from web!");
  Serial.printf("  Session ID: %s\n", sessionId.c_str());
  Serial.println("─────────────────────────────────────────");
  Serial.println("\nTap the item NFC tag on the scanner...");

  currentState = WAIT_ITEM;
}

// Resolve item from tag then complete the session
void processItemScan(const String& rfidUid) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); }

  // ── Step 1: resolve item from tag ─────────────────────────────────────────
  String itemUrl = String(API_BASE_URL) + "/api/v1/items/rfid/" + rfidUid;
  Serial.printf("GET %s\n", itemUrl.c_str());

  HTTPClient http1;
  http1.setConnectTimeout(5000);
  http1.setTimeout(10000);
  http1.begin(itemUrl);

  int itemCode = http1.GET();
  String itemResp = http1.getString();
  http1.end();

  Serial.printf("HTTP %d\n", itemCode);

  if (itemCode == 404) {
    Serial.println("✗ Item not registered.");
    completeSession(false, "", "", "", "Item tag not registered.");
    return;
  }
  if (itemCode != 200) {
    Serial.printf("✗ Item lookup error: %d\n", itemCode);
    completeSession(false, "", "", "", "Item lookup error " + String(itemCode));
    return;
  }

  StaticJsonDocument<512> itemDoc;
  deserializeJson(itemDoc, itemResp);
  String itemId   = itemDoc["data"]["id"].as<String>();
  String itemName = itemDoc["data"]["itemName"].as<String>();

  Serial.printf("✓ Item: %s (ID: %s)\n", itemName.c_str(), itemId.c_str());

  // ── Step 2: complete the session — pass itemId + rfidUid + itemName to web ─
  completeSession(true, itemId, itemName, rfidUid, "");
}

// POST /api/v1/item-scan-sessions/{id}/complete
void completeSession(bool success, const String& itemId, const String& itemName,
                     const String& rfidUid, const String& errorMsg) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); }

  String url = String(API_BASE_URL) + "/api/v1/item-scan-sessions/" + sessionId + "/complete";
  Serial.printf("POST %s\n", url.c_str());

  StaticJsonDocument<256> doc;
  doc["success"]  = success;
  doc["itemName"] = itemName;
  if (success && itemId.length() > 0)   doc["itemId"]       = itemId;
  if (success && rfidUid.length() > 0)  doc["rfidUid"]      = rfidUid;
  if (!success)                         doc["errorMessage"] = errorMsg;
  String body;
  serializeJson(doc, body);

  HTTPClient http;
  http.setConnectTimeout(5000);
  http.setTimeout(10000);
  http.begin(url);
  http.addHeader("Content-Type", "application/json");

  int code = http.POST(body);
  http.end();

  Serial.printf("Session complete HTTP %d\n", code);

  if (code == 200) {
    if (success)
      Serial.printf("✓ Item ID sent to web — staff can now fill in guest details.\n");
    else
      Serial.printf("✗ Scan failed: %s\n", errorMsg.c_str());
  } else {
    Serial.printf("✗ Failed to complete session (HTTP %d) — retrying in 2s...\n", code);
    delay(2000);

    // Retry once
    HTTPClient http2;
    http2.setConnectTimeout(5000);
    http2.setTimeout(10000);
    http2.begin(url);
    http2.addHeader("Content-Type", "application/json");
    int retryCode = http2.POST(body);
    http2.end();
    Serial.printf("Session complete retry HTTP %d\n", retryCode);
  }
}

// ── Session Reset ─────────────────────────────────────────────────────────────

void resetSession() {
  sessionId    = "";
  lastUid      = "";
  currentState = POLLING;

  Serial.println("\n── Session reset — polling for next request...\n");

  delay(3000);

  // Reconnect WiFi to clear stale socket state
  if (WiFi.status() != WL_CONNECTED) {
    connectWiFi();
  } else {
    WiFi.disconnect(false);
    delay(500);
    WiFi.reconnect();
    unsigned long start = millis();
    while (WiFi.status() != WL_CONNECTED && millis() - start < 5000) { delay(200); }
    Serial.printf("WiFi ready — IP: %s\n", WiFi.localIP().toString().c_str());
  }
}
