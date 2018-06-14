/*
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
#define RST_PIN   9     // SPI Reset Pin
#define SS_PIN    10    // SPI Slave Select Pin
MFRC522 mfrc522(SS_PIN, RST_PIN);   // Instanz des MFRC522 erzeugen

bool previousNotNull = false;
bool initialized = false;

void setup() {
  // Diese Funktion wird einmalig beim Start ausgeführt
  pinMode(SS_PIN,OUTPUT);
  Serial.begin(9600);  // Serielle Kommunikation mit dem PC initialisieren
  SPI.begin();         // Initialisiere SPI Kommunikation
  mfrc522.PCD_Init();  // Initialisiere MFRC522 Lesemodul
  mfrc522.PCD_DumpVersionToSerial(); //Zeigt Details des MFRC522 Card Readers
}

void loop() {
    bool notNull = false;

    mfrc522.PICC_IsNewCardPresent(); 
    mfrc522.PICC_ReadCardSerial();

    if(mfrc522.uid.size < 1) {
      if(initialized == true){
        if(previousNotNull != notNull){
          if(notNull)
            Serial.println(notNull);
          previousNotNull = notNull;
        }        
      } else {
        if(notNull)
          Serial.println(notNull);
        initialized = true;     
        previousNotNull = notNull;
      }
      return;
    }

    byte uidSize = mfrc522.uid.size;
    byte uid[mfrc522.uid.size];
    
    for (byte i = 0; i < uidSize; i++) {
      uid[i] = mfrc522.uid.uidByte[i];
      mfrc522.uid.uidByte[i] = 0;
    }

    for (byte i = 0; i < uidSize; i++) {
      if(uid[i] != 0) {
        notNull = true;
      }
    }
    
    if(initialized == true){
      if(previousNotNull != notNull){
        if(notNull)
          Serial.println(notNull);
        previousNotNull = notNull;
      }        
    } else {       
      initialized = true;   
      previousNotNull = notNull;
    }
    
    // Versetzt die gelesene Karte in einen Ruhemodus, um nach anderen Karten suchen zu können.
    mfrc522.PICC_HaltA();
    //mfrc522.PCD_Init();
}
