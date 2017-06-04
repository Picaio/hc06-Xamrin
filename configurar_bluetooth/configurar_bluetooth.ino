#include <SoftwareSerial.h>

/* Programa el modulo bluetooth HC-06 con un nuevo: 
  NOMBRE  (Nombre de 20 caracteres)
  PIN     (Clave de cuatro numeros)
  BPS     (Velocidad de conexion en baudios)

  
  CONEXIONES:
  ARDUINO   BLUETOOTH
  5V        VCC
  GND       GND
  PIN 2     TX
  PIN 3     RX
  
 */

SoftwareSerial blue(2, 3);   //Crea conexion al bluetooth - PIN 2 a TX y PIN 3 a RX

char NOMBRE[21]  = "HC-06 Picaio"; // Nombre de 20 caracteres maximo
char BPS         = '4';     // 1=1200 , 2=2400, 3=4800, 4=9600, 5=19200, 6=38400, 7=57600, 8=115200
char PASS[5]    = "1234";   // PIN O CLAVE de 4 caracteres numericos     
 
void setup()
{
    blue.begin(9600); // inicialmente la comunicacion serial a 9600 Baudios (velocidad de fabrica)
    
    pinMode(13,OUTPUT);
    digitalWrite(13,HIGH); // Enciende el LED 13 durante 4s antes de configurar el Bluetooth
    delay(4000);
    
    digitalWrite(13,LOW); // Apaga el LED 13 para iniciar la programacion
    
    blue.print("AT");  // Inicializa comando AT
    delay(1000);
 
    blue.print("AT+NAME"); // Configura el nuevo nombre 
    blue.print(NOMBRE);
    delay(1000);                  // espera 1 segundo
 
    blue.print("AT+BAUD");  // Configura la nueva velocidad 
    blue.print(BPS); 
    delay(1000);
 
    blue.print("AT+PIN");   // Configura el nuevo PIN
    blue.print(PASS); 
    delay(1000);    
}
 
void loop()
{
    digitalWrite(13, !digitalRead(13)); // cuando termina de configurar el Bluetooth queda el LED 13 parpadeando
    delay(300);
}
