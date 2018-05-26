constexpr int delay_ms = 500;
constexpr bool use_serial = true;


void setup() {
  if (use_serial) {
    Serial.begin(9600);
    while (!Serial);
  }
  pinMode(LED_BUILTIN, OUTPUT);
}


void loop() {
  digitalWrite(LED_BUILTIN, HIGH);
  if (use_serial) Serial.println("ON");
  delay(delay_ms);
  digitalWrite(LED_BUILTIN, LOW);
  if (use_serial) Serial.println("OFF");
  delay(delay_ms);
}

