#include <Wire.h>
#include <Adafruit_PN532.h>
#include <WiFi.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>

// PN532 pins
#define PN532_IRQ   4
#define PN532_RESET 2

Adafruit_PN532 nfc(PN532_IRQ, PN532_RESET);

// ── Config ────────────────────────────────────────────────────────────────────
#define WIFI_SSID      "EjGwapo2.4G"
#define WIFI_PASSWORD  "vigheadTHEG0D2.4G"
#define API_BASE_URL   "http://192.168.1.4:5289"

#define API_IDENTIFIER "christian"
#define API_PASSWORD   "@Password123"

// JWT Token (obtained after login)
String jwtToken = "";

// Registration Mode
enum RegistrationMode {
  MODE_MANUAL,      // Admin enters Item ID via Serial
  MODE_SEQUENTIAL   // Auto-fetch next item from backend
};

RegistrationMode currentMode = MODE_MANUAL;

void setup() {
  Serial.begin(115200);
  delay(500);

  Serial.println("\n=== RFID Item Registration Mode ===");

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

  Serial.println("\n[Manual Mode] Type 'M' to start registration");
  Serial.println("Instructions:");
  Serial.println("1. Type 'M' and press Enter");
  Serial.println("2. Scan the RFID tag on the item");
  Serial.println("3. Enter the Item ID (GUID)");
  Serial.println("4. Press Enter to register\n");
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

// Register RFID to item
bool registerRfidToItem(String itemId, String rfidUid) {
  if (WiFi.status() != WL_CONNECTED) { connectWiFi(); login(); }

  String url = String(API_BASE_URL) + "/api/v1/items/" + itemId + "/register-rfid";
  Serial.printf("POST %s\n", url.c_str());

  StaticJsonDocument<200> doc;
  doc["rfidUid"] = rfidUid;
  String body;
  serializeJson(doc, body);

  HTTPClient http;
  http.setConnectTimeout(5000);
  http.setTimeout(10000);
  http.begin(url);
  http.addHeader("Content-Type", "application/json");
  http.addHeader("Authorization", "Bearer " + jwtToken);

  int code = http.POST(body);
  String resp = http.getString();
  http.end();

  Serial.printf("HTTP %d — %s\n", code, resp.c_str());

  if (code == 200) {
    Serial.println("✓ RFID registered to item successfully!");
    return true;
  } else if (code == 401) {
    Serial.println("✗ Unauthorized. Re-logging in...");
    login();
    return false;
  } else if (code == 404) {
    Serial.println("✗ Item not found. Check the Item ID.");
    return false;
  } else if (code == 409) {
    Serial.println("✗ RFID already assigned to another item.");
    return false;
  } else {
    Serial.printf("✗ Unexpected error: %d\n", code);
    return false;
  }
}

void handleManualMode() {
  Serial.println("\n─────────────────────────────");
  Serial.println("Waiting for RFID tag...\n");

  uint8_t uid[7];
  uint8_t uidLength;

  // Wait for RFID tag
  while (!nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A, uid, &uidLength, 1000)) {
    // Check for mode change
    if (Serial.available()) {
      String input = Serial.readStringUntil('\n');
      input.trim();
      input.toUpperCase();
      if (input == "M") {
        Serial.println("Already in MANUAL mode\n");
      }
      return;
    }
  }

  String uidStr = getUidString(uid, uidLength);

  Serial.println("Found RFID tag!");
  Serial.printf("UID: %s\n", uidStr.c_str());

  // Wait for Item ID input
  Serial.println("\nEnter Item ID (GUID):");
  Serial.print("> ");

  // Wait for serial input with timeout
  unsigned long startTime = millis();
  String itemId = "";

  while (millis() - startTime < 60000) { // 60 second timeout
    if (Serial.available()) {
      itemId = Serial.readStringUntil('\n');
      itemId.trim();
      break;
    }
    delay(100);
  }

  if (itemId.length() == 0) {
    Serial.println("\nTimeout! No Item ID entered.\n");
    delay(2000);
    return;
  }

  Serial.printf("\nRegistering RFID '%s' to Item '%s'...\n", uidStr.c_str(), itemId.c_str());

  bool success = registerRfidToItem(itemId, uidStr);

  if (success) {
    Serial.println("\n✓ Registration Complete!\n");
  }

  Serial.println("Waiting 3 seconds before next scan...\n");
  delay(3000);
}

void loop() {
  // Check for mode change command
  if (Serial.available()) {
    String input = Serial.readStringUntil('\n');
    input.trim();
    input.toUpperCase();
    
    if (input == "M") {
      currentMode = MODE_MANUAL;
      Serial.println("\nManual mode activated. Scan RFID tag...\n");
    }
  }
  
  // Execute based on current mode
  if (currentMode == MODE_MANUAL) {
    handleManualMode();
  }
}
