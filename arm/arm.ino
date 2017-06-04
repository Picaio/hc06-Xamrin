#include <Servo.h> 
int led13=13;
String estado;
int numero=0;

Servo DOF1;
Servo DOF2;
Servo DOF3;
Servo DOF4;
Servo DOF5;
Servo DOF6;

void setup(){
  Serial.begin(9600);
   pinMode(led13,OUTPUT);
   DOF1.attach(6);
   DOF2.attach(7);
   DOF3.attach(8);
   DOF4.attach(9);
   DOF5.attach(10);
   DOF6.attach(11);
}

void loop(){
 if(Serial.available()>0){
 estado = Serial.readString();
 Serial.println(estado);

if(estado.startsWith("a"))
{
  estado.remove(0,1);
  Serial.println(estado);
  numero = estado.toInt();
  numero = map(numero, 0, 100, 0, 180);     // scale it to use it with the servo (value between 0 and 180) 
  DOF1.write(numero);                  // sets the servo position according to the scaled value 
  delay(15);            
  
}
if(estado.startsWith("b"))
{
  estado.remove(0,1);
  Serial.println(estado);
  numero = estado.toInt();
  numero = map(numero, 0, 100, 0, 180);     // scale it to use it with the servo (value between 0 and 180) 
  DOF2.write(numero);                  // sets the servo position according to the scaled value 
  delay(15);            
  
}
if(estado.startsWith("c"))
{
  estado.remove(0,1);
  Serial.println(estado);
  numero = estado.toInt();
  numero = map(numero, 0, 100, 0, 180);     // scale it to use it with the servo (value between 0 and 180) 
  DOF3.write(numero);                  // sets the servo position according to the scaled value 
  delay(15);            
  
}
if(estado.startsWith("d"))
{
  estado.remove(0,1);
  Serial.println(estado);
  numero = estado.toInt();
  numero = map(numero, 0, 100, 0, 180);     // scale it to use it with the servo (value between 0 and 180) 
  DOF4.write(numero);                  // sets the servo position according to the scaled value 
  delay(15);            
  
}
if(estado.startsWith("e"))
{
  estado.remove(0,1);
  Serial.println(estado);
  numero = estado.toInt();
  numero = map(numero, 0, 100, 0, 180);     // scale it to use it with the servo (value between 0 and 180) 
  DOF5.write(numero);                  // sets the servo position according to the scaled value 
  delay(15);            
  
}
if(estado.startsWith("f"))
{
  estado.remove(0,1);
  Serial.println(estado);
  numero = estado.toInt();
  numero = map(numero, 0, 100, 0, 180);     // scale it to use it with the servo (value between 0 and 180) 
  DOF6.write(numero);                  // sets the servo position according to the scaled value 
  delay(15);            
  
}

 
 }

}  
