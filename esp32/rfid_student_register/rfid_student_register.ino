/**
 * ESP32 + PN532 Student RFID Registration Sketch
 *
 * Flow:
 *   1. Set TARGET_STUDENT_ID to the student's ID number below
 *   2. Flash to ESP32
 *   3. Connect to WiFi & login → get JWT
 *   4. Backend resolves the student GUID from the ID number
 *   5. Scan an unregistered RFID card → UID is POSTed to that student
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

// ── Target Student ────────────────────────────────────────────────────────────
#define TARGET_STUDENT_ID "2023-0001"   // ← change this before flashing

// ── PN532 ─────────────────────────────────────────────────────────────────────
#define PN532_IRQ   4
#define PN532_RESET 2

Adafruit_PN532 nfc(PN532_IRQ, PN532_RESET);

// ── State ─────────────────────────────────────────────────────────────────────
String jwtToken    = "";
String studentGuid = "";
bool   registered  = false;

// ─────────────────────────────────────────────────────────────────────────────

void setup() {
  Serial.begin(115200);
  delay(500);

  Serial.println("\n=== Student RFID Registration Mode ===");

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

  Serial.printf("Resolving student: %s\n", TARGET_STUDENT_ID);
  if (!resolveStudentGuid(TARGET_STUDENT_ID)) {
    Serial.println("Could not resolve student — halting.");
    while (1);
  }

  Serial.println("\nReady — scan a card to register it to this student...");
}

void loop() {
  if (registered) { delay(1000); return; }

  uint8_t uid[7];
  uint8_t uidLength;

  if (!nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A, uid, &uidLength, 1000)) return;

  String uidStr = getUidString(uid, uidLength);
  Serial.printf("\nCard scanned: %s\n", uidStr.c_str());

  bool ok = registerStudentRfid(studentGuid, uidStr);
  if (ok) {
    Serial.println("✓ RFID registered successfully.");
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
    Serial.printf("Login failed (HTTP %d) — halting.\n", code);
    while (1);
  }

  StaticJsonDocument<1024> respDoc;
  deserializeJson(respDoc, resp);
  jwtToken = respDoc["data"]["accessToken"].as<String>();
  Serial.println("Login successful.\n");
}

// Calls GET /api/v1/users/students/by-id-number/{idNumber}
// Stores the student's GUID in studentGuid on success
bool resolveStudentGuid(const String& idNumber) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); login(); }

  String url = String(API_BASE_URL) + "/api/v1/users/students/by-id-number/" + idNumber;
  Serial.printf("GET %s\n", url.c_str());

  HTTPClient http;
  http.begin(url);
  http.addHeader("Authorization", "Bearer " + jwtToken);

  int code = http.GET();
  String resp = http.getString();
  http.end();

  Serial.printf("HTTP %d\n", code);

  if (code == 401) { Serial.println("Token expired, re-logging in..."); login(); return false; }
  if (code == 404) { Serial.println("✗ Student not found. Check the ID number."); return false; }
  if (code != 200) { Serial.printf("✗ Unexpected error (HTTP %d).\n", code); return false; }

  StaticJsonDocument<1024> doc;
  if (deserializeJson(doc, resp) != DeserializationError::Ok) {
    Serial.println("✗ Failed to parse response.");
    return false;
  }

  studentGuid = doc["data"]["id"].as<String>();
  String name = String(doc["data"]["firstName"].as<const char*>()) + " " +
                String(doc["data"]["lastName"].as<const char*>());
  Serial.printf("✓ Found: %s (GUID: %s)\n", name.c_str(), studentGuid.c_str());
  return studentGuid.length() > 0;
}

// Calls POST /api/v1/users/students/{guid}/register-rfid
bool registerStudentRfid(const String& guid, const String& rfidUid) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); login(); }

  String url = String(API_BASE_URL) + "/api/v1/users/students/" + guid + "/register-rfid";
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
  if (code == 409) { Serial.println("✗ This RFID is already registered to another student."); return false; }
  if (code == 404) { Serial.println("✗ Student not found."); return false; }
  return code == 200;
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
