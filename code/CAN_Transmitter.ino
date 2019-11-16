#include <mcp_can.h>
#include <SPI.h>

const int led_pwr = 5;
const int led_tx = 6;

MCP_CAN CAN0(10);     // Set CS to pin 10

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

      led_millis_next = 999999999999; // Initialize to max possible value so LED functions will not run when no CAN message has transmitted
      led_flag = false;
    }

    send_CAN()
    {
      if (millis() > millis_next)
      {
        // send data:  ID = 0x100, Standard CAN Frame, Data length = 8 bytes, 'data' = array of data bytes to send
        byte sndStat = CAN0.sendMsgBuf(id, CAN_ID_29_BIT, dlc, data);
        
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

void CAN_initialize();

byte CCVS1_data[8] = {0x37, 0x00, 0x00, 0xCF, 0x3F, 0xFF, 0xFF, 0xFF};
CAN_message_cyclic CCVS1(0x18FEF100, 8, CCVS1_data, 100);

byte EBC2_data[8] = {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
CAN_message_cyclic EBC2(0x18FEBF0B, 8, EBC2_data, 100);

void setup()
{
  pinMode(led_pwr, OUTPUT);
  pinMode(led_tx, OUTPUT);

  digitalWrite(led_pwr, LOW);
  digitalWrite(led_tx, LOW);
  
  CAN_initialize();

  digitalWrite(led_pwr, HIGH);
}

void loop()
{
  CCVS1.send_CAN();
  EBC2.send_CAN();
}

void CAN_initialize()
{
  Serial.begin(9600);

  // Initialize MCP2515 running at 16MHz with a baudrate of 500kb/s and the masks and filters disabled.
  if(CAN0.begin(MCP_ANY, CAN_250KBPS, MCP_16MHZ) == CAN_OK) 
    Serial.println("MCP2515 Initialized Successfully!");
  else 
    Serial.println("Error Initializing MCP2515...");

  CAN0.setMode(MCP_NORMAL);   // Change to normal mode to allow messages to be transmitted
}
