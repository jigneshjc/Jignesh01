using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SixtyFourT.BAL
{
  static public class Utility
  {
    static public Int32 Top_Floor_No = 100;
    static public Int32 Bottom_Floor_No = 0;
    static public Int32 DoorOpenTime = 2;
    static public Int32 DoorCloseTime = 2;
    static public Int32 SpeedBetweenFloors = 3;

    static public string Status_Dormant = "Dormant";
    static public string Status_Moving_Up = "Moving Up";
    static public string Status_Moving_Down = "Moving Down";
    static public string Status_Door_Opening = "Door Opening";
    static public string Status_Door_Closing = "Door Closing";
    static public string Status_Disabled = "Disabled";
  }
}
