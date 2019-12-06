/*=============================================================================
The MIT License (MIT)

Copyright (c) 2019 Jonathan Lin
Copyright (c) 2019 Thomas Paraschuk
Copyright (c) 2013 Seeed Technology Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

The repository for this project, including documentation, can be found at:

  https://github.com/JonathanXLin/CAN_Transmitter
  
=============================================================================*/

#include "can_cyclic.hpp"

byte CCVS1_data[8] = {0x37, 0x00, 0x00, 0xCF, 0x3F, 0xFF, 0xFF, 0xFF};
CAN_message_cyclic CCVS1(0x18FEF100, 8, CCVS1_data, 100);

byte EBC2_data[8] = {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
CAN_message_cyclic EBC2(0x18FEBF0B, 8, EBC2_data, 100);

void setup(){
  // Initializes CAN and LEDs
  CAN_initialize();
}

void loop(){
  // One line for each CAN message
  CCVS1.send_CAN();
  EBC2.send_CAN();
}
