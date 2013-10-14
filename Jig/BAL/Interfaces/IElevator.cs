using System;
using System.Collections.Generic;
using System.Collections;

namespace SixtyFourT.BAL
{
  public interface IElevator
  {
    //status of service
    string Status { get; set; }

    //current floor number
    Int32 CureentFloor { get; set; }

    //Current Service Direction
    Int32 Direction { get; set; }

    //Start floor number
    Int32 StartFloor { get; set; }

    //Destination floor number
    Int32 DestinationFloor { get; set; }

    //service number floors to travel
    Int32 FloorCount { get; set; }

    //service is busy and running
    Int32 IsRunning { get; set; }

    //service start time
    DateTime StartTime { get; set; }

    //expected service end time
    DateTime EndTime { get; set; }

    //Door open time
    Int32 DoorOpenTime { get; }

    //Door close time
    Int32 DoorCloseTime { get; }

    //store Speed betwwen the floors
    Int32 SpeedBetweenFloors { get; }

  }
}
