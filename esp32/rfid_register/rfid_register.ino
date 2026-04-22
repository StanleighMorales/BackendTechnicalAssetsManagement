#include <Wire.h>
#include <Adafruit_PN532.h>
#include <WiFi.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>

// PN532 pins
#define PN532_IRQ   4
#define PN532_RESET 2

Adafruit_PN532 nfc(PN532_IRQ, PN532_RESET);

// Network Credentials
const char* ssid = "EjGwapo2.4G";
const char* password = "vigheadTHEG0D2.4G";

// Backend API Configuration
const char* apiBaseUrl = "http://192.168.1.4:5289";

// Admin credentials for authentication
const char* adminIdentifier = "christian";
const char* adminPassword = "@Password123";

// JWT Token
String jwtToken = "";

void setup() {
  Serial.begin(115200);
  delay(500);
  
  Serial.println("\n=== RFID Card Registration Station ===");
  
  // Initialize PN532
  nfc.begin();
  uint32_t ver = nfc.getFirmwareVersion();
  if (!ver) {
    Serial.println("ERROR: PN532 not found. Check wiring/I2C switches.");
    while (1);
  }
  Serial.printf("PN532 firmware v%d.%d\n", (ver >> 16) & 0xFF, (ver >> 8) & 0xFF);
  nfc.SAMConfig();
  
  // Connect to WiFi
  Serial.printf("Connecting to %s", ssid);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.printf("\nConnected — IP: %s\n", WiFi.localIP().toString().c_str());
  
  // Login to get JWT token
  login();
  
  Serial.println("\nWaiting for RFID cards to register...");
}

void login() {
  String url = String(apiBaseUrl) + "/api/v1/auth/login-mobile";
  Serial.printf("Logging in as %s...\n", adminIdentifier);

  StaticJsonDocument<128> doc;
  doc["identifier"] = adminIdentifier;
  doc["password"]   = adminPassword;
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

// Function to generate 8-character random Alphanumeric code
String generateRegCode() {
  const char vars[] = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
  String code = "";
  for (int i = 0; i < 8; i++) {
    code += vars[esp_random() % (sizeof(vars) - 1)];
  }
  return code;
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

bool registerRfidCard(String uid, String regCode) {
  if (WiFi.status() != WL_CONNECTED) {
    Serial.println("WiFi disconnected, reconnecting...");
    WiFi.begin(ssid, password);
    int attempts = 0;
    while (WiFi.status() != WL_CONNECTED && attempts < 10) {
      delay(500);
      Serial.print(".");
      attempts++;
    }
    if (WiFi.status() != WL_CONNECTED) {
      Serial.println("\nWiFi reconnection failed!");
      return false;
    }
    Serial.println("\nReconnected!");
    login(); // Re-login after reconnection
  }
  
  String url = String(apiBaseUrl) + "/api/v1/rfids";
  
  HTTPClient http;
  http.setConnectTimeout(5000);
  http.setTimeout(10000);
  http.begin(url);
  http.addHeader("Content-Type", "application/json");
  http.addHeader("Authorization", "Bearer " + jwtToken);
  
  // Create JSON payload
  StaticJsonDocument<200> doc;
  doc["rfidUid"] = uid;
  doc["rfidCode"] = regCode;
  
  String jsonPayload;
  serializeJson(doc, jsonPayload);
  
  Serial.printf("POST %s\n", url.c_str());
  Serial.printf("Payload: %s\n", jsonPayload.c_str());
  
  int httpResponseCode = http.POST(jsonPayload);
  String response = http.getString();
  http.end();
  
  Serial.printf("HTTP %d\n", httpResponseCode);
  
  if (httpResponseCode == 200) {
    Serial.println("✓ RFID card registered successfully!");
    
    // Parse response message
    StaticJsonDocument<512> responseDoc;
    DeserializationError error = deserializeJson(responseDoc, response);
    if (!error) {
      const char* message = responseDoc["message"];
      if (message) {
        Serial.printf("   %s\n", message);
      }
    }
    return true;
  } else if (httpResponseCode == 401) {
    Serial.println("✗ Unauthorized! Token expired, re-logging in...");
    login();
    return false;
  } else if (httpResponseCode == 409) {
    Serial.println("✗ RFID already registered!");
    Serial.println("   This card is already in the system.");
    return false;
  } else if (httpResponseCode == -1) {
    Serial.println("✗ Connection error!");
    Serial.println("   Cannot reach backend server.");
    return false;
  } else {
    Serial.printf("✗ Unexpected error: %d\n", httpResponseCode);
    Serial.printf("   Response: %s\n", response.c_str());
    return false;
  }
}

void loop() {
  uint8_t uid[7];
  uint8_t uidLength;
  
  // Wait for a card (1 second timeout)
  if (!nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A, uid, &uidLength, 1000)) {
    return;
  }
  
  String uidStr = getUidString(uid, uidLength);
  
  Serial.println("\n─────────────────────────────");
  Serial.println("Found RFID card!");
  Serial.printf("UID: %s (%d bytes)\n", uidStr.c_str(), uidLength);
  
  // Generate random 8-character registration code
  String regCode = generateRegCode();
  Serial.printf("Generated Code: %s\n", regCode.c_str());
  
  // Register to backend
  bool success = registerRfidCard(uidStr, regCode);
  
  if (success) {
    Serial.println("\n✓ Registration Complete!");
    Serial.println("   Students can now use this code to register.");
  }
  
  // Wait 5 seconds before next scan to prevent duplicates
  Serial.println("\nWaiting 5 seconds before next scan...");
  Serial.println("─────────────────────────────\n");
  delay(5000);
}
