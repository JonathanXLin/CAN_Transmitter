#include "can_cyclic.hpp"

void CAN_initialize(MCP_CAN myCan_250KBPS, MCP_CAN myCan_500KBPS)
{
  pinMode(led_pwr, OUTPUT);
  pinMode(led_tx, OUTPUT);

  digitalWrite(led_pwr, LOW);
  digitalWrite(led_tx, LOW);
  
  Serial.begin(9600);

  // Initialize MCP2515 running at 16MHz with a baudrate of 500kb/s and the masks and filters disabled.
  if(myCan_250KBPS.begin(MCP_ANY, CAN_250KBPS, MCP_16MHZ) == CAN_OK) 
    Serial.println("MCP2515 250KBPS Initialized Successfully!");
  else 
  {
    while (1)
    {
      Serial.println("Error Initializing MCP2515 250KBPS...");
      
      digitalWrite(led_pwr, HIGH);
      delay(300);
      digitalWrite(led_pwr, LOW);
      delay(300);
    }
  }
  
  if(myCan_500KBPS.begin(MCP_ANY, CAN_500KBPS, MCP_16MHZ) == CAN_OK) 
    Serial.println("MCP2515 500KBPS Initialized Successfully!");
  else 
  {
    while (1)
    {
      Serial.println("Error Initializing MCP2515 500KBPS...");
      
      digitalWrite(led_pwr, HIGH);
      delay(300);
      digitalWrite(led_pwr, LOW);
      delay(300);
    }
  }

  myCan_250KBPS.setMode(MCP_NORMAL);   // Change to normal mode to allow messages to be transmitted
  myCan_500KBPS.setMode(MCP_NORMAL);

  digitalWrite(led_pwr, HIGH);
}
