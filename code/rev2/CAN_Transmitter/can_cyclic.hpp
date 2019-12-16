#ifndef CAN_CYCLIC_HPP
#define CAN_CYCLIC_HPP

#include "mcp_can.h"
#include <SPI.h>
#include <SD.h>

#define led_pwr   5
#define led_tx    6

#define pin_baud_select   8

#define num_bytes_serial 12 //the number of bytes to send the 32 bit id and 8 bytes of message
#define num_bytes_data 8 //number of bytes in the data frame

/*SAMD core*/
#ifdef ARDUINO_SAMD_VARIANT_COMPLIANCE
  #define SERIAL SerialUSB
#else
  #define SERIAL Serial
#endif

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
    void send_CAN();
};

void CAN_initialize();

void print_can_receive();

void MCP2515_ISR();

void serial_to_app(unsigned long id, unsigned char buf[num_bytes_data]);

#endif
