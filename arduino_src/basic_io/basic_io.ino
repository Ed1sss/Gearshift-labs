#include <SimulatorProgram.h>

String p1=",";
String p2= "VALUES: ";
int testValue1 =0;
int testValue2=A0;
int testValue3 = 0;
void setup() {
  Serial.begin(9600);                           
  inString.reserve(10);
  RealIO_Connect();
}

void loop() {
    testValue1 = (testValue1 + 1) % 360;
    testValue2 = (testValue2 + 1) % 100;
    testValue3= 1;
    Serial.println(p2 + testValue1+p1+testValue2+p1+testValue3);
}