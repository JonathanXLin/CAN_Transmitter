#include "can_cyclic.hpp"

MCP_CAN myCan_250KBPS(10);     // Set CS to pin 10
MCP_CAN myCan_500KBPS(10);     // Set CS to pin 10

// ============== ADD AND EDIT CAN MESSAGE PARAMETERS ACCORDING TO FORMAT BELOW ======================
// byte your_data_name[8] = {your_data};
// CAN_message_cyclic CCVS1(your_address, your_dlc, your_data_name, your_period);

byte CCVS1_data[8] = {0x37, 0x00, 0x00, 0xCF, 0x3F, 0xFF, 0xFF, 0xFF};
CAN_message_cyclic CCVS1(0x18FEF100, 8, CCVS1_data, 100);

byte EBC2_data[8] = {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
CAN_message_cyclic EBC2(0x18FEBF0B, 8, EBC2_data, 100);

// ===================================================================================================

void setup()
{
  // Initializes CAN and LEDs
  CAN_initialize(myCan_250KBPS, myCan_500KBPS);
}

void loop()
{
  // One line for each CAN message
  CCVS1.send_CAN(myCan_250KBPS, myCan_500KBPS);
  EBC2.send_CAN(myCan_250KBPS, myCan_500KBPS);
}
