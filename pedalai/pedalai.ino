void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(9600);
}

// the loop routine runs over and over again forever:
void loop() {
  // read the input on analog pin 0:
  int sensorValue = analogRead(A0);
  int sensorValue2 = analogRead(A2);
  int sensorValue3 = analogRead(A4);
  // print out the value you read:
  Serial.print("1: ");
  Serial.println(sensorValue);
  Serial.print("2: ");
  Serial.println(sensorValue2);
  //Serial.print("3: ");
  //Serial.println(sensorValue3);
  delay(500);  // delay in between reads for stability
}
