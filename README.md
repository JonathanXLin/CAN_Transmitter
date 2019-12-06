# CAN_Transmitter
Please read the User Guide for more information.

This CAN transmitter sends cyclic CAN messages for automotive products undergoing long duration testing. This is intended to be easy to create, program, and configure. The following are user configurable:
<br><br>•	Baud rate
<br>•	CAN ID type (11/29 bit)
<br>•	Number of CAN messages
  <br>&nbsp;&nbsp;&nbsp;o	CAN message IDs
  <br>&nbsp;&nbsp;&nbsp;o	CAN message DLCs
  <br>&nbsp;&nbsp;&nbsp;o	CAN message data
  <br>&nbsp;&nbsp;&nbsp;o	CAN message period
  
In addition, rev2 code adds the capability of data logging. Incoming CAN frames are stored in a plain ASCII file (.txt). Due to hardware limitations, if the incoming message frequency exceeds 30Hz, bad things will happen (CAN buffer overflow and log file becomes inaccurate, IRQ handler hogs processor time and transmit messages don't get transmitted).
