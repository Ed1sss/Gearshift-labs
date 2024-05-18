#include <SimulatorProgram.h>

String p1=",";
String p2= "VALUES: ";
int accelerator =0;
int brake=0;
int clutch = 0;
int shift_down = 0;
int shift_up = 0;
int wheel = 0;

int accelerator_pin = A0;
int brake_pin =A1;
int shift_up_pin = 4;
int shift_down_pin = 3;
int wheel_pin = A3;


void setup() {
  Serial.begin(9600);
  pinMode(shift_down_pin, INPUT);
  pinMode(shift_up_pin, INPUT);                      
  inString.reserve(10);
  RealIO_Connect();
}

void loop() {
  int accelerator = analogRead(accelerator_pin);
  int brake=analogRead(brake_pin);
  int shift_down = digitalRead(shift_down_pin);
  int shift_up = digitalRead(shift_up_pin);
  int wheel = analogRead(wheel_pin);


  Serial.println(accelerator + p1 + brake + p1 + wheel + p1 + shift_down + p1 + shift_up);
}
 