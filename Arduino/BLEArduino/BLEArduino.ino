#include <SPI.h>
#include <EEPROM.h>
#include <boards.h>
#include <RBL_nRF8001.h>

#define DIGITAL_OUT_PIN 2

void setup() {
  // put your setup code here, to run once:
  ble_begin();

  // Enable serial debug
  Serial.begin(57600);
  
  pinMode(DIGITAL_OUT_PIN, OUTPUT); 
}

void loop() 
{
    // put your main code here, to run repeatedly:
    while(ble_available())
    {
      // read out command and data
      byte data0 = ble_read();
        if (data0 == 0x01)
          digitalWrite(DIGITAL_OUT_PIN, HIGH);
        else
          digitalWrite(DIGITAL_OUT_PIN, LOW);
    }
}
