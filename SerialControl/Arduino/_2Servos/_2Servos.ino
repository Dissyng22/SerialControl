// zoomkat 11-22-10 serial servo (2) test
// for writeMicroseconds, use a value like 1500
// for IDE 0019 and later
// Powering a servo from the arduino usually DOES NOT WORK.
// two servo setup with two servo commands
// send eight character string like 15001500 or 14501550

#include <Servo.h> 
String readString, servo, stangle;
int s1angle = 20;
int s2angle = 0;
Servo myservo1;  // create servo object to control a servo 
Servo myservo2;
const int releasepin = 13;
const int servo1pin = 9;
const int servo2pin = 8;
const int servo1min = 024;
const int servo1max = 170;
const int servo2min = 000;
const int servo2max = 180;

void setup() {
  Serial.begin(9600);
  pinMode(releasepin, OUTPUT);
  digitalWrite(releasepin, HIGH);
  myservo1.attach(servo1pin);  //the pin for the servo control 
  myservo2.attach(servo2pin);
  myservo1.write(s1angle);
  myservo2.write(s2angle);
  Serial.println("servo-test-21"); // so I can keep track of what is loaded
}

void loop() {

  while (Serial.available()) {
    delay(10);  
    if (Serial.available() >0) {
      char c = Serial.read();  //gets one byte from serial buffer
      readString += c; //makes the string readString
    } 
  }

  if (readString.length() >0) {
      Serial.println("Input:");
      Serial.println(readString); //see what was received
      
      // expect a string like 1150 containing the servo number and angle      
      servo = readString.substring(0, 1);
      stangle = readString.substring(1, 4);

      Serial.println("Servo number:");
      Serial.println(servo);
      Serial.println("Current angle servo 1:");
      Serial.println(s1angle);
      Serial.println("Current angle servo 2:");
      Serial.println(s2angle);
      

      if(servo == "1")
      {
      char carray3[6]; //magic needed to convert string to a number 
      stangle.toCharArray(carray3, sizeof(carray3));
        s1angle = atoi(carray3);
        Serial.println("New requested angle for servo 1:");
        Serial.println(s1angle);
      }
      else if(servo == "2")
      {
      char carray3[6]; //magic needed to convert string to a number 
      stangle.toCharArray(carray3, sizeof(carray3));
        s2angle = atoi(carray3);
        Serial.println("New requested angle for servo 2:");
        Serial.println(s2angle);
      }

      int servonum; 
      char carray4[6]; //magic needed to convert string to a number 
      servo.toCharArray(carray4, sizeof(carray4));
      servonum = atoi(carray4); 
 
      switch (servonum) 
      {
        case 1:
          if(s1angle < servo1min)
            s1angle = servo1min; 
          else if(s1angle > servo1max)
            s1angle = servo1max;
            Serial.println("Setting new angle:");
            Serial.println(s1angle);
            myservo1.write(s1angle);
          break;
        case 2:
          if(s2angle < servo2min)
            s2angle = servo2min; 
          else if(s2angle > servo2max)
            s2angle = servo2max;
            Serial.println("Setting new angle:");
            Serial.println(s2angle);
          myservo2.write(s2angle);
          break;
         case 3:
          digitalWrite(releasepin, LOW);
         break;
          
      }
      
    readString="";
  } 
}


