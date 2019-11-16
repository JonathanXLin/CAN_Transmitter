#include "can_cyclic.hpp"

void CAN_initialize(MCP_CAN myCan)
{
  pinMode(led_pwr, OUTPUT);
  pinMode(led_tx, OUTPUT);

  digitalWrite(led_pwr, LOW);
  digitalWrite(led_tx, LOW);
  
  Serial.begin(9600);

  // Initialize MCP2515 running at 16MHz with a baudrate of 500kb/s and the masks and filters disabled.
  if(myCan.begin(MCP_ANY, CAN_250KBPS, MCP_16MHZ) == CAN_OK) 
    Serial.println("MCP2515 Initialized Successfully!");
  else 
  {
    while (1)
    {
      Serial.println("Error Initializing MCP2515...");
      
      digitalWrite(led_pwr, HIGH);
      delay(300);
      digitalWrite(led_pwr, LOW);
      delay(300);
    }
  }

  myCan.setMode(MCP_NORMAL);   // Change to normal mode to allow messages to be transmitted

  digitalWrite(led_pwr, HIGH);
}
