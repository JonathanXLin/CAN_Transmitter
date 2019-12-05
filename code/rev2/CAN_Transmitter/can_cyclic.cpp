#include "can_cyclic.hpp"

MCP_CAN myCan(9);     // Set CS to pin 9

unsigned char flagRecv = 0;
unsigned char len = 0;
unsigned char buf[8];
char str[20];

void CAN_initialize(){
  pinMode(led_pwr, OUTPUT);
  pinMode(led_tx, OUTPUT);
  pinMode(pin_baud_select, INPUT);

  digitalWrite(led_pwr, LOW);
  digitalWrite(led_tx, LOW);
  
  Serial.begin(9600);

  if (digitalRead(pin_baud_select))
  {
    // Initialize MCP2515 running at 16MHz with a baudrate of 250kb/s and the masks and filters disabled.
    if(myCan.begin(CAN_250KBPS) == CAN_OK) 
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
  }
  else
  {
    // Initialize MCP2515 running at 16MHz with a baudrate of 500kb/s and the masks and filters disabled.
    if(myCan.begin(CAN_500KBPS) == CAN_OK) 
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
  }

  attachInterrupt(0, MCP2515_ISR, FALLING); // start interrupt

  digitalWrite(led_pwr, HIGH);
}

CAN_message_cyclic::CAN_message_cyclic(long can_id, int can_dlc, byte *can_data, int can_period){
  id = can_id;
  dlc = can_dlc;

  for (int i=0; i<8; i++)
  {
    data[i] = can_data[i];
  }

  period = can_period;
  millis_next = millis() + period;

  led_millis_next = 999999999999; // Initialize to large so LED functions will not run when no CAN message has transmitted
  led_flag = false;

  pin_baud_select_prev = digitalRead(pin_baud_select);
}

void CAN_message_cyclic::send_CAN(){
  if (millis() > millis_next)
  {
    // send data:  ID = 0x100, Standard CAN Frame, Data length = 8 bytes, 'data' = array of data bytes to send
    byte sndStat = 0;

    int pin_baud_select_curr = digitalRead(pin_baud_select);
    if (pin_baud_select_curr != pin_baud_select_prev)
    {
      if (pin_baud_select_curr)
      {
        // Initialize MCP2515 running at 16MHz with a baudrate of 250kb/s and the masks and filters disabled.
        if(myCan.begin(CAN_250KBPS) == CAN_OK) 
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

        attachInterrupt(0, MCP2515_ISR, FALLING); // start interrupt
      }
      else
      {
        // Initialize MCP2515 running at 16MHz with a baudrate of 500kb/s and the masks and filters disabled.
        if(myCan.begin(CAN_500KBPS) == CAN_OK) 
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

        attachInterrupt(0, MCP2515_ISR, FALLING); // start interrupt
      }
    }
    pin_baud_select_prev = pin_baud_select_curr;
    
    sndStat = myCan.sendMsgBuf(id, CAN_ID_29_BIT, dlc, data);;
    
    if(sndStat == CAN_OK)
    {
      //Serial.print("SENT AND RECEIVED, message id: ");
      //Serial.print(id, HEX);
      //Serial.println();
    } 
    else 
    {
      //Serial.print("ERROR SENDING MESSAGE, message id: ");
      //Serial.print(id, HEX);
      //Serial.println();
    }

    millis_next = millis() + period; // Update millis_next

    if (!led_flag)
    {
      digitalWrite(led_tx, HIGH);
      led_flag = true;
    }

    led_millis_next = millis() + 50; // Update led_millis_next
  }

  if (millis() > led_millis_next && led_flag)
  {
    digitalWrite(led_tx, LOW);
    led_flag = false;
  }
}

void MCP2515_ISR(){
    flagRecv = 1;
}

void print_can_receive(){
  if(flagRecv) 
  {                                   // check if get data
    flagRecv = 0;                   // clear flag
    unsigned long id = 0;

    // iterate over all pending messages
    // If either the bus is saturated or the MCU is busy,
    // both RX buffers may be in use and reading a single
    // message does not clear the IRQ conditon.
    while (myCan.checkReceive() == CAN_MSGAVAIL) 
    {
      // read data,  len: data length, buf: data buf
      myCan.readMsgBufID(&id, &len, buf);

      Serial.print(id);
      Serial.print(",");
      
      for(int i = 0; i<len; i++)
      {
        Serial.print(buf[i]);
        Serial.print(",");
      }
      Serial.println();
    }
  }
}
