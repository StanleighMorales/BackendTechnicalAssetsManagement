/**
 * ESP32 + PN532 Student Borrow Sketch
 *
 * Flow:
 *   1. Connect to WiFi
 *   2. Login via POST /api/v1/auth/login-mobile → get JWT
 *   3. Scan student ID card → GET /api/v1/users/students/rfid/{uid} → resolve userId
 *   4. Scan item tag     → GET /api/v1/items/rfid/{uid}             → resolve itemId
 *   5. POST /api/v1/lentItems with { itemId, userId, status: "Pending", room, subjectTimeSchedule }
 *   6. Reset and wait for next borrow session
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

// Default borrow metadata — adjust or make dynamic as needed
#define DEFAULT_ROOM     "Room 101"
#define DEFAULT_SCHEDULE "MWF 8:00-9:30"

// ── PN532 ─────────────────────────────────────────────────────────────────────
#define PN532_IRQ   4
#define PN532_RESET 2

Adafruit_PN532 nfc(PN532_IRQ, PN532_RESET);

// ── State machine ─────────────────────────────────────────────────────────────
enum State { WAIT_STUDENT, WAIT_ITEM, SUBMITTING };

State  currentState = WAIT_STUDENT;
String jwtToken     = "";
String studentId    = "";   // GUID resolved from student RFID
String itemId       = "";   // GUID resolved from item RFID
String lastUid      = "";
unsigned long lastScanTime = 0;

#define SCAN_COOLDOWN_MS 2000

// ─────────────────────────────────────────────────────────────────────────────

void setup() {
  Serial.begin(115200);
  delay(500);

  Serial.println("\n=== RFID Borrow Mode ===");

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

  Serial.println("\n[Step 1] Scan your student ID card...");
}

void loop() {
  if (currentState == SUBMITTING) return;

  uint8_t uid[7];
  uint8_t uidLength;

  if (!nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A, uid, &uidLength, 1000)) return;

  String uidStr = getUidString(uid, uidLength);
  unsigned long now = millis();

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

// Calls GET /api/v1/users/students/rfid/{uid} → stores studentId GUID
bool resolveStudent(const String& rfidUid) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); login(); }

  String url = String(API_BASE_URL) + "/api/v1/users/students/rfid/" + rfidUid;
  Serial.printf("GET %s\n", url.c_str());

  HTTPClient http;
  http.begin(url);
  http.addHeader("Authorization", "Bearer " + jwtToken);

  int code = http.GET();
  String resp = http.getString();
  http.end();

  Serial.printf("HTTP %d\n", code);

  if (code == 401) { Serial.println("Token expired, re-logging in..."); login(); return false; }
  if (code == 404) { Serial.println("✗ Student not registered. Ask admin to register your ID card first."); return false; }
  if (code != 200) { Serial.printf("✗ Unexpected error: %d\n", code); return false; }

  StaticJsonDocument<512> doc;
  deserializeJson(doc, resp);
  studentId = doc["data"]["id"].as<String>();

  String name = String(doc["data"]["firstName"].as<String>()) + " " + String(doc["data"]["lastName"].as<String>());
  Serial.printf("✓ Student: %s (ID: %s)\n", name.c_str(), studentId.c_str());
  return true;
}

// Calls GET /api/v1/items/rfid/{uid} → stores itemId GUID
bool resolveItem(const String& rfidUid) {
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

  if (code == 401) { Serial.println("Token expired, re-logging in..."); login(); return false; }
  if (code == 404) { Serial.println("✗ Item not registered. Ask admin to register this tag first."); return false; }
  if (code != 200) { Serial.printf("✗ Unexpected error: %d\n", code); return false; }

  StaticJsonDocument<512> doc;
  deserializeJson(doc, resp);
  itemId = doc["data"]["id"].as<String>();

  String itemName = doc["data"]["itemName"].as<String>();
  Serial.printf("✓ Item: %s (ID: %s)\n", itemName.c_str(), itemId.c_str());
  return true;
}

// Calls POST /api/v1/lentItems to create the borrow record
void submitBorrow() {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); login(); }

  String url = String(API_BASE_URL) + "/api/v1/lentItems";
  Serial.printf("POST %s\n", url.c_str());

  StaticJsonDocument<256> doc;
  doc["itemId"]               = itemId;
  doc["userId"]               = studentId;
  doc["room"]                 = DEFAULT_ROOM;
  doc["subjectTimeSchedule"]  = DEFAULT_SCHEDULE;
  doc["status"]               = "Pending";
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

  if (code == 201 || code == 200)
    Serial.println("✓ Borrow request submitted! Waiting for admin approval.");
  else if (code == 400)
    Serial.println("✗ Request rejected — item may already be borrowed or your profile is incomplete.");
  else if (code == 401)
    Serial.println("✗ Unauthorized.");
  else
    Serial.printf("✗ Unexpected: %d\n", code);
}

void resetSession() {
  studentId    = "";
  itemId       = "";
  lastUid      = "";
  currentState = WAIT_STUDENT;
  Serial.println("\n── Session reset ──");
  Serial.println("[Step 1] Scan your student ID card...");
}
