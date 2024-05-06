void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(9600);
}

// the loop routine runs over and over again forever:
void loop() {
  // read the input on analog pin 0:
  int sensorValue4 = analogRead(A6);
  // print out the value you read:
  Serial.print("1: ");
  Serial.println(sensorValue4);
  delay(500);  // delay in between reads for stability
}
