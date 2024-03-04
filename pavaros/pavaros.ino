#include <SimulatorProgram.h>
const int myg1=3;
const int myg2=4;
int pavara;
bool pasikeite1 = false;
bool pasikeite2 = false;
int pmyg1 = 0;
int pmyg2 = 0;
void setup() {
  // put your setup code here, to run once:
pinMode(myg1, INPUT);
pinMode(myg2, INPUT);
Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  pmyg1 = digitalRead(myg1);
  pmyg2 = digitalRead(myg2);
  pavara = 0;
  if(pmyg1==LOW){
    pasikeite1=false;
  }
  if(pmyg2==LOW){
    pasikeite2=false;
  }
  if(pmyg1 == HIGH && pasikeite1 == false){
    pavara=1;
    pasikeite1=true;
  }
  if(pmyg2 == HIGH && pasikeite2 == false){
    pavara=2;
    pasikeite2=true;
  }
  //Serial.println(pavara);
  CheckVar(1,pavara);
  delay(500);
}
