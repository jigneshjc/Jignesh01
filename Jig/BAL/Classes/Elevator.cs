using System;
using System.Collections.Generic;
using System.Collections;

namespace SixtyFourT.BAL
{
  
  public class Elevator : IElevator
  {
    #region private Variables
    private Int32 _DoorOpenTime;
    private Int32 _DoorCloseTime;
    private Int32 _SpeedBetweenFloors;
    private bool _ButtonUp;
    private bool _ButtonDown;
    #endregion private Variables

    #region public Variables
    public string Status { get; set; }
    public Int32 CureentFloor { get; set; }
    public Int32 Direction { get; set; }
    public Int32 StartFloor { get; set; }
    public Int32 DestinationFloor { get; set; }
    public Int32 FloorCount { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Int32 IsRunning { get; set; }
    #endregion public Variables

    #region Properties
    public Int32 DoorOpenTime
    {
      get
      {
        return this._DoorOpenTime; 
      }
    }
    public Int32 DoorCloseTime
    {
      get
      {
        return this._DoorCloseTime;
      }
    }
    public Int32 SpeedBetweenFloors
    {
      get
      {
        return this._SpeedBetweenFloors;
      }
    }

    public bool ButtonUp
    {
      get { return this._ButtonUp; }
      set { _ButtonUp = value; }
    }

    public bool ButtonDown
    {
      get { return this._ButtonDown; }
      set { _ButtonDown = value; }
    }

    //store pending requests for service floor and direction
    public Hashtable PendingRequests = new Hashtable();
    #endregion Properties

    #region Constructor
    public Elevator()
    {
      _DoorOpenTime = Utility.DoorOpenTime;
      _DoorCloseTime = Utility.DoorCloseTime;
      _SpeedBetweenFloors = Utility.SpeedBetweenFloors;
    }
    #endregion Constructor
  }
}
