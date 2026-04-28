/**
 * ESP32 + PN532 Student RFID Registration Station
 *
 * Flow:
 *   1. Connect to WiFi
 *   2. Poll GET /api/v1/rfid-sessions/pending/student → check for pending student registration
 *   3. If session found, scan student's RFID card
 *   4. POST /api/v1/rfid-sessions/{sessionId}/complete/student with scanned RFID UID
 *   5. Backend links RFID to student and marks session complete
 *   6. Return to polling
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
#define API_BASE_URL   "http://192.168.1.17:5289"

// ── PN532 ─────────────────────────────────────────────────────────────────────
#define PN532_IRQ   4
#define PN532_RESET 2

Adafruit_PN532 nfc(PN532_IRQ, PN532_RESET);

// ── State machine ─────────────────────────────────────────────────────────────
enum State { POLLING, WAITING_FOR_SCAN, SUBMITTING };

State  currentState = POLLING;
String sessionId    = "";
String studentId    = "";
String studentName  = "";
String lastUid      = "";
unsigned long lastScanTime = 0;
unsigned long lastPollTime = 0;

#define SCAN_COOLDOWN_MS 2000
#define POLL_INTERVAL_MS 2000  // Poll every 2 seconds

// ─────────────────────────────────────────────────────────────────────────────

void setup() {
  Serial.begin(115200);
  delay(500);

  Serial.println("\n=== Student RFID Registration Station ===");

  nfc.begin();
  uint32_t ver = nfc.getFirmwareVersion();
  if (!ver) {
    Serial.println("ERROR: PN532 not found. Check wiring/I2C switches.");
    while (1);
  }
  Serial.printf("PN532 firmware v%d.%d\n", (ver >> 16) & 0xFF, (ver >> 8) & 0xFF);
  nfc.SAMConfig();

  connectWiFi();

  Serial.println("\nPolling for student registration requests...");
}

void loop() {
  unsigned long now = millis();

  // State: POLLING - Check for pending registration sessions
  if (currentState == POLLING) {
    if (now - lastPollTime >= POLL_INTERVAL_MS) {
      lastPollTime = now;
      checkPendingSession();
    }
    return;
  }

  // State: WAITING_FOR_SCAN - Wait for RFID card scan
  if (currentState == WAITING_FOR_SCAN) {
    uint8_t uid[7];
    uint8_t uidLength;

    if (!nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A, uid, &uidLength, 1000)) return;

    String uidStr = getUidString(uid, uidLength);

    // Cooldown to prevent duplicate scans
    if (uidStr == lastUid && (now - lastScanTime) < SCAN_COOLDOWN_MS) return;
    lastUid      = uidStr;
    lastScanTime = now;

    Serial.printf("\n✓ Scanned RFID: %s\n", uidStr.c_str());

    currentState = SUBMITTING;
    completeSession(uidStr);
    resetSession();
  }
}

// ── Helpers ───────────────────────────────────────────────────────────────────

void connectWiFi() {
  Serial.printf("Connecting to %s", WIFI_SSID);
  WiFi.begin(WIFI_SSID, WIFI_PASSWORD);
  while (WiFi.status() != WL_CONNECTED) { delay(500); Serial.print("."); }
  Serial.printf("\nConnected — IP: %s\n", WiFi.localIP().toString().c_str());
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

// Poll for pending student registration sessions
void checkPendingSession() {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); }

  String url = String(API_BASE_URL) + "/api/v1/rfid-sessions/pending/student";

  HTTPClient http;
  http.setConnectTimeout(5000);
  http.setTimeout(10000);
  http.begin(url);

  int code = http.GET();
  String resp = http.getString();
  http.end();

  // 204 No Content = no pending sessions
  if (code == 204) {
    return;
  }

  if (code != 200) {
    Serial.printf("✗ Error polling sessions: %d\n", code);
    return;
  }

  // Parse session data
  StaticJsonDocument<1024> doc;
  DeserializationError error = deserializeJson(doc, resp);
  if (error) {
    Serial.printf("✗ JSON parse error: %s\n", error.c_str());
    return;
  }

  sessionId   = doc["data"]["id"].as<String>();
  studentId   = doc["data"]["studentId"].as<String>();
  studentName = doc["data"]["studentName"].as<String>();

  Serial.println("\n─────────────────────────────────────────");
  Serial.println("✓ New registration request!");
  Serial.printf("  Student: %s\n", studentName.c_str());
  Serial.printf("  Student ID: %s\n", studentId.c_str());
  Serial.printf("  Session ID: %s\n", sessionId.c_str());
  Serial.println("─────────────────────────────────────────");
  Serial.println("\n[Action Required] Place student's RFID card on scanner...");

  currentState = WAITING_FOR_SCAN;
}

// Submit scanned RFID to complete the session
void completeSession(const String& rfidUid) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); }

  String url = String(API_BASE_URL) + "/api/v1/rfid-sessions/" + sessionId + "/complete/student";
  Serial.printf("POST %s\n", url.c_str());

  StaticJsonDocument<128> doc;
  doc["rfidUid"] = rfidUid;
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

  Serial.printf("HTTP %d\n", code);

  if (code == 200) {
    Serial.println("\n✓✓✓ SUCCESS ✓✓✓");
    Serial.printf("RFID '%s' registered to %s\n", rfidUid.c_str(), studentName.c_str());
    Serial.println("Student can now use their card for borrowing items.");
  } else if (code == 404) {
    Serial.println("✗ Session not found or expired.");
  } else if (code == 409) {
    Serial.println("✗ RFID already registered to another student.");
    Serial.println("  This card is already in use.");
  } else {
    Serial.printf("✗ Unexpected error: %d\n", code);
    Serial.printf("  Response: %s\n", resp.c_str());
  }
}

void resetSession() {
  sessionId    = "";
  studentId    = "";
  studentName  = "";
  lastUid      = "";
  currentState = POLLING;
  
  Serial.println("\n── Session reset ──");
  Serial.println("Polling for next registration request...\n");
  
  delay(3000);  // 3-second cooldown before next poll
}
