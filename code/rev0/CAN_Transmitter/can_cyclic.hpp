#include <Arduino.h>
#include <mcp_can.h>
#include <SPI.h>

#define led_pwr   5
#define led_tx    6

#define pin_baud_select   8

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
    int pin_baud_select_prev; // previous state of pin baud select
  public:
    CAN_message_cyclic(long can_id, int can_dlc, byte *can_data, int can_period);
    void send_CAN(MCP_CAN myCan);
};

void CAN_initialize(MCP_CAN myCan);
