/*
   MFRC522 - Library to use ARDUINO RFID MODULE KIT 13.56 MHZ WITH TAGS SPI W AND R BY COOQROBOT.
   The library file MFRC522.h has a wealth of useful info. Please read it.
   The functions are documented in MFRC522.cpp.

   Typical pin layout used:
   -----------------------------------------------------------------------------------------
               MFRC522      Arduino       Arduino   Arduino    Arduino          Arduino
               Reader/PCD   Uno/101       Mega      Nano v3    Leonardo/Micro   Pro Micro
   Signal      Pin          Pin           Pin       Pin        Pin              Pin
   -----------------------------------------------------------------------------------------
   RST/Reset   RST          9             5         D9         RESET/ICSP-5     RST
   SPI SS      SDA(SS)      10            53        D10        10               10
   SPI MOSI    MOSI         11 / ICSP-4   51        D11        ICSP-4           16
   SPI MISO    MISO         12 / ICSP-1   50        D12        ICSP-1           14
   SPI SCK     SCK          13 / ICSP-3   52        D13        ICSP-3           15

*/

#include <SPI.h>
#include <MFRC522.h>


static constexpr uint8_t SS_PIN = 10;
static constexpr uint8_t RST_PIN = 9;

MFRC522 mfrc522(SS_PIN, RST_PIN);


void initSerial(int baudrate = 9600) {
  Serial.begin(baudrate);
  while (!Serial);
  //Serial.println("Serial initialized");
}


void initSPI() {
  SPI.begin();
  //Serial.println("SPI initialized");
}


void initMFRC522(const MFRC522& board) {
  board.PCD_Init();
  //Serial.println("MFRC522 initialized");
}


uint32_t convertUID(byte* id) {
  uint32_t num = 0;  
  for (byte i = 0; i < 4; i++) {
    num = (num << 8) | id[i];
  }
  return num;
}


void setup() {
  initSerial();
  initSPI();
  initMFRC522(mfrc522);
}


void loop() {
  if (!mfrc522.PICC_IsNewCardPresent())
    return;
  if (!mfrc522.PICC_ReadCardSerial())
    return;

  auto* uidData = mfrc522.uid.uidByte;
  auto uidNum = convertUID(uidData);
  //Serial.println(uidNum);
  Serial.write(uidData, 4);
  Serial.flush();
  //Serial.println();
  mfrc522.PICC_HaltA();
  //mfrc522.PICC_DumpToSerial(&(mfrc522.uid));
}


