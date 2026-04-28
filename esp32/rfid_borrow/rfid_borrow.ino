/**
 * ESP32 + PN532 Student Borrow Sketch — Web-Triggered Session Mode
 *
 * Flow:
 *   1. Connect to WiFi + sync time
 *   2. Poll GET /api/v1/borrow-sessions/pending every 2s  ← idles here until web triggers
 *   3. Session found → scan student ID card → resolve userId
 *   4. Scan item tag → resolve itemId
 *   5. POST /api/v1/lentItems  { itemId, userId, room, subjectTimeSchedule, status: "Pending" }
 *   6. POST /api/v1/borrow-sessions/{id}/complete  { success, studentName, itemName, lentItemId }
 *   7. Reset → back to polling
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
#include <time.h>

// ── Config ────────────────────────────────────────────────────────────────────
#define WIFI_SSID           "EjGwapo2.4G"
#define WIFI_PASSWORD       "vigheadTHEG0D2.4G"
#define API_BASE_URL        "http://192.168.1.17:5289"

#define NTP_SERVER          "pool.ntp.org"
#define GMT_OFFSET_SEC      28800   // GMT+8 Philippines
#define DAYLIGHT_OFFSET_SEC 0

#define DEFAULT_ROOM        "Technical"
#define SCAN_COOLDOWN_MS    2000
#define POLL_INTERVAL_MS    2000

// ── PN532 ─────────────────────────────────────────────────────────────────────
#define PN532_IRQ   4
#define PN532_RESET 2

Adafruit_PN532 nfc(PN532_IRQ, PN532_RESET);

// ── State machine ─────────────────────────────────────────────────────────────
enum State { POLLING, WAIT_STUDENT, WAIT_ITEM, SUBMITTING };

State  currentState  = POLLING;
String sessionId     = "";
String studentId     = "";
String studentName   = "";
String itemId        = "";
String itemName      = "";
String lastUid       = "";
unsigned long lastScanTime = 0;
unsigned long lastPollTime = 0;

// ─────────────────────────────────────────────────────────────────────────────

void setup() {
  Serial.begin(115200);
  delay(500);

  Serial.println("\n=== RFID Borrow Station (Session Mode) ===");

  nfc.begin();
  uint32_t ver = nfc.getFirmwareVersion();
  if (!ver) {
    Serial.println("ERROR: PN532 not found. Check wiring/I2C switches.");
    while (1);
  }
  Serial.printf("PN532 firmware v%d.%d\n", (ver >> 16) & 0xFF, (ver >> 8) & 0xFF);
  nfc.SAMConfig();

  connectWiFi();
  syncTime();

  Serial.println("\nPolling for borrow sessions from web...");
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

  // ── SUBMITTING: blocked until submitBorrow() finishes ─────────────────────
  if (currentState == SUBMITTING) return;

  // ── WAIT_STUDENT / WAIT_ITEM: read RFID ───────────────────────────────────
  uint8_t uid[7];
  uint8_t uidLength;
  if (!nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A, uid, &uidLength, 1000)) return;

  String uidStr = getUidString(uid, uidLength);
  if (uidStr == lastUid && (now - lastScanTime) < SCAN_COOLDOWN_MS) return;
  lastUid      = uidStr;
  lastScanTime = now;

  Serial.printf("\nScanned tag: %s\n", uidStr.c_str());

  if (currentState == WAIT_STUDENT) {
    if (resolveStudent(uidStr)) {
      currentState = WAIT_ITEM;
      Serial.println("\n[Step 2] Now scan the item tag...");
    }
  } else if (currentState == WAIT_ITEM) {
    if (resolveItem(uidStr)) {
      currentState = SUBMITTING;
      submitBorrow();
      resetSession();
    }
  }
}

// ── WiFi ──────────────────────────────────────────────────────────────────────

void connectWiFi() {
  Serial.printf("Connecting to %s", WIFI_SSID);
  WiFi.begin(WIFI_SSID, WIFI_PASSWORD);
  while (WiFi.status() != WL_CONNECTED) { delay(500); Serial.print("."); }
  Serial.printf("\nConnected — IP: %s\n", WiFi.localIP().toString().c_str());
}

// ── NTP ───────────────────────────────────────────────────────────────────────

void syncTime() {
  Serial.print("Syncing time with NTP...");
  configTime(GMT_OFFSET_SEC, DAYLIGHT_OFFSET_SEC, NTP_SERVER);
  struct tm timeinfo;
  int attempts = 0;
  while (!getLocalTime(&timeinfo) && attempts < 10) { Serial.print("."); delay(1000); attempts++; }
  if (attempts >= 10) {
    Serial.println("\nWarning: NTP sync failed — schedule will use fallback.");
  } else {
    char buf[32];
    strftime(buf, sizeof(buf), "%A %I:%M %p", &timeinfo);
    Serial.printf("\nTime synced: %s\n", buf);
  }
}

String getCurrentSchedule() {
  struct tm timeinfo;
  if (!getLocalTime(&timeinfo)) return "Monday 8:00 AM";
  const char* days[] = { "Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday" };
  int hour = timeinfo.tm_hour, minute = timeinfo.tm_min;
  const char* ampm = (hour >= 12) ? "PM" : "AM";
  int hour12 = hour % 12; if (hour12 == 0) hour12 = 12;
  char buf[32];
  snprintf(buf, sizeof(buf), "%s %d:%02d %s", days[timeinfo.tm_wday], hour12, minute, ampm);
  return String(buf);
}

// ── UID Helper ────────────────────────────────────────────────────────────────

String getUidString(uint8_t* buf, uint8_t len) {
  String s = "";
  for (uint8_t i = 0; i < len; i++) { if (buf[i] < 0x10) s += "0"; s += String(buf[i], HEX); }
  s.toUpperCase();
  return s;
}

// ── API Calls ─────────────────────────────────────────────────────────────────

// Poll GET /api/v1/borrow-sessions/pending — 204 means nothing pending, 200 means go
void checkPendingSession() {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); }

  String url = String(API_BASE_URL) + "/api/v1/borrow-sessions/pending";
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
  Serial.println("✓ Borrow session triggered from web!");
  Serial.printf("  Session ID: %s\n", sessionId.c_str());
  Serial.println("─────────────────────────────────────────");
  Serial.println("\n[Step 1] Scan student ID card...");

  currentState = WAIT_STUDENT;
}

// GET /api/v1/users/students/rfid/{uid}
bool resolveStudent(const String& rfidUid) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); }

  String url = String(API_BASE_URL) + "/api/v1/users/students/rfid/" + rfidUid;
  Serial.printf("GET %s\n", url.c_str());

  HTTPClient http;
  http.setConnectTimeout(5000);
  http.setTimeout(10000);
  http.begin(url);

  int code = http.GET();
  String resp = http.getString();
  http.end();

  Serial.printf("HTTP %d\n", code);

  if (code == 404) { Serial.println("✗ Student not registered."); return false; }
  if (code != 200) { Serial.printf("✗ Unexpected error: %d\n", code); return false; }

  StaticJsonDocument<512> doc;
  deserializeJson(doc, resp);
  studentId   = doc["data"]["id"].as<String>();
  studentName = String(doc["data"]["firstName"].as<String>())
              + " "
              + String(doc["data"]["lastName"].as<String>());

  Serial.printf("✓ Student: %s (ID: %s)\n", studentName.c_str(), studentId.c_str());
  return true;
}

// GET /api/v1/items/rfid/{uid}
bool resolveItem(const String& rfidUid) {
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

  if (code == 404) { Serial.println("✗ Item not registered."); return false; }
  if (code != 200) { Serial.printf("✗ Unexpected error: %d\n", code); return false; }

  StaticJsonDocument<512> doc;
  deserializeJson(doc, resp);
  itemId   = doc["data"]["id"].as<String>();
  itemName = doc["data"]["itemName"].as<String>();

  Serial.printf("✓ Item: %s (ID: %s)\n", itemName.c_str(), itemId.c_str());
  return true;
}

// POST /api/v1/lentItems then POST /api/v1/borrow-sessions/{id}/complete
void submitBorrow() {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); }

  String schedule = getCurrentSchedule();

  // ── Step A: create the lent item ──────────────────────────────────────────
  String lentUrl = String(API_BASE_URL) + "/api/v1/lentItems";
  Serial.printf("POST %s\n", lentUrl.c_str());
  Serial.printf("  Student : %s\n", studentName.c_str());
  Serial.printf("  Item    : %s\n", itemName.c_str());
  Serial.printf("  Room    : %s\n", DEFAULT_ROOM);
  Serial.printf("  Schedule: %s\n", schedule.c_str());

  StaticJsonDocument<256> lentDoc;
  lentDoc["itemId"]              = itemId;
  lentDoc["userId"]              = studentId;
  lentDoc["subjectTimeSchedule"] = schedule;
  lentDoc["status"]              = "Pending";
  if (strlen(DEFAULT_ROOM) > 0) lentDoc["room"] = DEFAULT_ROOM;
  String lentBody;
  serializeJson(lentDoc, lentBody);

  HTTPClient http1;
  http1.setConnectTimeout(5000);
  http1.setTimeout(10000);
  http1.begin(lentUrl);
  http1.addHeader("Content-Type", "application/json");

  int lentCode = http1.POST(lentBody);
  String lentResp = http1.getString();
  http1.end();

  Serial.printf("LentItems HTTP %d\n", lentCode);

  bool    success    = (lentCode == 200 || lentCode == 201);
  String  lentItemId = "";
  String  errorMsg   = "";

  if (success) {
    StaticJsonDocument<512> respDoc;
    deserializeJson(respDoc, lentResp);
    lentItemId = respDoc["data"]["id"].as<String>();
    Serial.println("✓ Borrow request submitted!");
  } else {
    errorMsg = "LentItems error " + String(lentCode);
    Serial.printf("✗ Failed: %s\n", errorMsg.c_str());
    Serial.printf("  Response: %s\n", lentResp.c_str());
  }

  // ── Step B: close the borrow session ──────────────────────────────────────
  String sessUrl = String(API_BASE_URL) + "/api/v1/borrow-sessions/" + sessionId + "/complete";
  Serial.printf("POST %s\n", sessUrl.c_str());

  StaticJsonDocument<256> sessDoc;
  sessDoc["success"]     = success;
  sessDoc["studentName"] = studentName;
  sessDoc["itemName"]    = itemName;
  if (success && lentItemId.length() > 0) sessDoc["lentItemId"]   = lentItemId;
  if (!success)                           sessDoc["errorMessage"] = errorMsg;
  String sessBody;
  serializeJson(sessDoc, sessBody);

  HTTPClient http2;
  http2.setConnectTimeout(5000);
  http2.setTimeout(10000);
  http2.begin(sessUrl);
  http2.addHeader("Content-Type", "application/json");

  int sessCode = http2.POST(sessBody);
  http2.end();

  Serial.printf("Session complete HTTP %d\n", sessCode);

  if (sessCode == 200) {
    Serial.println("✓ Session closed.");
  } else {
    Serial.printf("✗ Failed to complete session (HTTP %d) — retrying in 2s...\n", sessCode);
    delay(2000);

    // Retry once
    HTTPClient http3;
    http3.setConnectTimeout(5000);
    http3.setTimeout(10000);
    http3.begin(sessUrl);
    http3.addHeader("Content-Type", "application/json");
    int retryCode = http3.POST(sessBody);
    http3.end();
    Serial.printf("Session complete retry HTTP %d\n", retryCode);
  }
}

// ── Session Reset ─────────────────────────────────────────────────────────────

void resetSession() {
  sessionId   = "";
  studentId   = "";
  studentName = "";
  itemId      = "";
  itemName    = "";
  lastUid     = "";
  currentState = POLLING;

  Serial.println("\n── Session reset — polling for next request...\n");

  // Give the TCP stack time to fully close all connections before next poll
  delay(3000);

  // Force WiFi reconnect to clear any stale socket state
  if (WiFi.status() != WL_CONNECTED) {
    connectWiFi();
  } else {
    WiFi.disconnect(false);
    delay(500);
    WiFi.reconnect();
    unsigned long start = millis();
    while (WiFi.status() != WL_CONNECTED && millis() - start < 5000) {
      delay(200);
    }
    Serial.printf("WiFi ready — IP: %s\n", WiFi.localIP().toString().c_str());
  }
}
