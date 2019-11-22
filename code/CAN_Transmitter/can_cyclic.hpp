#include <Arduino.h>
#include <mcp_can.h>
#include <SPI.h>

#define led_pwr   5
#define led_tx    6

#define pin_250KBPS   8
#define pin_500KBPS   9

enum CAN_id_length {CAN_ID_11_BIT, CAN_ID_29_BIT};

class CAN_message_cyclic
{
  private:
    unsigned long id;
    unsigned int dlc;
    byte data[8];
    unsigned int period;
    unsigned long millis_next; // when millis exceeds this variable, send message again and increment it by the period
    unsigned long led_millis_next; // when LED turns on, this variable is made to be 10ms greater than millis(), and turns off when millis() exceeds it
    bool led_flag; // set to true when LED is on, reset when LED is off
  public:
    CAN_message_cyclic(long can_id, int can_dlc, byte *can_data, int can_period)
    {
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
    }

    void send_CAN(MCP_CAN myCan_250KBPS, MCP_CAN myCan_500KBPS)
    {
      if (millis() > millis_next)
      {
        // send data:  ID = 0x100, Standard CAN Frame, Data length = 8 bytes, 'data' = array of data bytes to send
        byte sndStat = 0;
        
        if (digitalRead(pin_250KBPS) == HIGH)
          sndStat = myCan_250KBPS.sendMsgBuf(id, CAN_ID_29_BIT, dlc, data);
        else (digitalRead(pin_500KBPS) == HIGH)
          sndStat = myCan_500KBPS.sendMsgBuf(id, CAN_ID_29_BIT, dlc, data);
        
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
          Serial.println("1");
        }

        led_millis_next = millis() + 50; // Update led_millis_next
      }

      if (millis() > led_millis_next && led_flag)
      {
        digitalWrite(led_tx, LOW);
        led_flag = false;
        Serial.println("0");
      }
    }
};

void CAN_initialize(MCP_CAN myCan_250KBPS, MCP_CAN myCan_500KBPS);
