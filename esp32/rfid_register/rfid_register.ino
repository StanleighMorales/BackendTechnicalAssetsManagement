#include <Wire.h>
#include <Adafruit_PN532.h>
#include <WiFi.h>
#include <HTTPClient.h>

// PN532 pins (same as your working code)
#define PN532_IRQ   4
#define PN532_RESET 2

Adafruit_PN532 nfc(PN532_IRQ, PN532_RESET);

// Network Credentials
// const char* ssid = "";
// const char* password = "";

// Supabase Configuration
// const char* serverName = "";
// const char* apiKey = "";

void setup() {
  Serial.begin(115200);
  delay(500);
  
  Serial.println("\n=== RFID Registration Mode ===");
  
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
  Serial.println("\nWaiting for RFID tags...");
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

void sendData(String uid, String regCode) {
  if (WiFi.status() != WL_CONNECTED) {
    Serial.println("WiFi disconnected, reconnecting...");
    WiFi.begin(ssid, password);
    while (WiFi.status() != WL_CONNECTED) {
      delay(500);
      Serial.print(".");
    }
    Serial.println("\nReconnected!");
  }
  
  HTTPClient http;
  http.begin(serverName);
  
  // Standard Supabase Headers
  http.addHeader("Content-Type", "application/json");
  http.addHeader("apikey", apiKey);
  http.addHeader("Authorization", "Bearer " + String(apiKey));
  
  // JSON Payload with correct column names: RfidUid and RfidCode
  String jsonPayload = "{\"RfidUid\": \"" + uid + "\", \"RfidCode\": \"" + regCode + "\"}";
  
  Serial.printf("Sending to Supabase: %s\n", jsonPayload.c_str());
  
  int httpResponseCode = http.POST(jsonPayload);
  String response = http.getString();
  
  Serial.printf("HTTP Response code: %d\n", httpResponseCode);
  
  if (httpResponseCode > 0) {
    if (httpResponseCode == 201 || httpResponseCode == 200) {
      Serial.println("✓ Registration Successful!");
      Serial.printf("Response: %s\n", response.c_str());
    } else {
      Serial.printf("✗ Unexpected response: %s\n", response.c_str());
    }
  } else {
    Serial.printf("✗ Error code: %d\n", httpResponseCode);
  }
  http.end();
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
  Serial.println("Found an RFID tag!");
  Serial.printf("UID Length: %d bytes\n", uidLength);
  Serial.printf("UID Value: %s\n", uidStr.c_str());
  
  // Generate random 8-character registration code
  String regCode = generateRegCode();
  Serial.printf("Generated Registration Code: %s\n", regCode.c_str());
  
  // Send data to Supabase
  sendData(uidStr, regCode);
  
  // Wait 5 seconds before next scan to prevent duplicates
  Serial.println("\nWaiting 5 seconds before next scan...");
  Serial.println("─────────────────────────────\n");
  delay(5000);
}
