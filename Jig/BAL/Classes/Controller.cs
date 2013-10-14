using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace SixtyFourT.BAL
{
  public class Controller : IController, IDisposable
  {
    #region Private Variables
    private Timer _timer;
    private bool _timerStatus = false;
    private bool _disposed = false;
    private string _errorMessage;
    #endregion

    #region Public Variables
    public string GetDate;
    public Elevator[] elevators = new Elevator[4];
    #endregion

    #region Public functions

    //start timer and initialise elevator
    public void StartController()
    {
      elevators[0] = new Elevator();
      elevators[1] = new Elevator();
      elevators[2] = new Elevator();
      elevators[3] = new Elevator();

      if (_timerStatus == false)
      {
        _timer = new Timer(1000);
        _timer.Interval = 1000;
        _timer.Enabled = true;
        _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        _timerStatus = true;

        foreach (Elevator elevator in elevators)
        {
          elevator.Status = Utility.Status_Dormant;
          elevator.IsRunning = 0;
        }
      }
    }

    //stop timer
    public void StopController()
    {
      _timer.Enabled = false;
    }
  
    //Response to service by sending elevator
    public Elevator ServiceRequest(Floor floor)
    {
      Elevator returnElevator = new Elevator();

      foreach (Elevator elevator in elevators)
      {
        if (elevator.Status == Utility.Status_Dormant)
        {
          elevator.CureentFloor = floor.CureentFloor;
          returnElevator = elevator;
          return returnElevator;
        }
      }
      //If all above elevators are busy then add in pending
      foreach (Elevator elevator in elevators)
      {
        if (elevator.Status != Utility.Status_Disabled)
        {
          //all elevators are busy store in pending request;
          if (!returnElevator.PendingRequests.ContainsKey(floor.CureentFloor))
          {
            returnElevator = elevator;
            //add pending request
            returnElevator.PendingRequests.Add(floor.CureentFloor, floor.Direction);
            return returnElevator;
          }
        }
      }

      return returnElevator;
    }

    //Start floor request Service
    public void StartService(Elevator elevator)
    {
      try
      {
        if (elevator.Direction == 1)
        {
          elevator.DestinationFloor = elevator.CureentFloor + elevator.FloorCount;
        }
        else
        {
          elevator.DestinationFloor = elevator.CureentFloor - elevator.FloorCount;
        }
        elevator.StartFloor = elevator.CureentFloor;
        elevator.FloorCount = elevator.FloorCount;
        elevator.StartTime = DateTime.Now;
        elevator.EndTime = getEndTime(elevator.FloorCount, elevator);
        elevator.IsRunning = 1;
        setStatus(elevator);
      }
      catch (Exception ex)
      {
        _errorMessage = ex.Message;
      }
    }

    //enable disable elevator by chaing the new status
    //new status Status_Dormant equal to enable.
    //new status Status_Disabled equal to disabled.
    public void SetElevatorStatus(Elevator elevator, string newStatus)
    {
      elevator.Status = newStatus;
    }

    //send particular elevator to floor request
    public Elevator ConfigureElevatorForFloor(Elevator elevator, Floor floor)
    {
      elevator.CureentFloor = floor.CureentFloor;
      return elevator;
    }

    #endregion

    #region Private Functions

    //Process request for each timer tick
    private void ProcessRequest(Elevator elevator)
    {
      if (elevator.EndTime > DateTime.Now && elevator.IsRunning == 1)
      {
        setStatus(elevator);
      }
      else
      {
        elevator.Status = Utility.Status_Dormant;
        elevator.CureentFloor = elevator.DestinationFloor;
        elevator.IsRunning = 0;
      }
    }

    //time tick event
    private void timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      GetDate = DateTime.Now.ToString();
      foreach (Elevator elevator in elevators)
      {
        if (elevator.Status != Utility.Status_Dormant)
        {
          ProcessRequest(elevator);
        }
      }
    }

    //get expected end time based on speed between the floors,  door open time and door close time
    private DateTime getEndTime(Int32 floorcount, Elevator elevator)
    {
      DateTime returnDatetime;
      returnDatetime = DateTime.Now.AddSeconds((floorcount * elevator.SpeedBetweenFloors) + elevator.DoorOpenTime + elevator.DoorCloseTime);
      return returnDatetime;
    }

    //Set status of elevator
    private void setStatus(Elevator elevator)
    {
      //check time and set floor number
      TimeSpan span = DateTime.Now.Subtract(elevator.StartTime);
      Int32 spanDoors = DateTime.Now.Subtract(elevator.EndTime).Seconds * -1;
      double noOfSeconds = Math.Round(span.Seconds / (double)elevator.SpeedBetweenFloors, 2);
      int FloorCount = (int)noOfSeconds;

      if (elevator.Direction == 1)
      {
        elevator.Status = Utility.Status_Moving_Up;
        elevator.CureentFloor = elevator.StartFloor + FloorCount;
      }
      else
      {
        elevator.Status = Utility.Status_Moving_Down;
        elevator.CureentFloor = elevator.StartFloor - FloorCount;
      }
      if (spanDoors <= 4 && spanDoors > 2)
      {
        elevator.Status = Utility.Status_Door_Opening;
        elevator.CureentFloor = elevator.DestinationFloor;
      }
      else if (spanDoors <= 2)
      {
        elevator.Status = Utility.Status_Door_Closing;
        elevator.CureentFloor = elevator.DestinationFloor;
      }
    }

    #endregion

    #region Dispose
    ~Controller()
        {
            Dispose(false);
        }

    public void Dispose()
    {
      Dispose(true);
    }

    protected virtual void Dispose(bool disposeManagedResources)
    {
      //// process only if mananged and unmanaged resources have
      //// not been disposed of.
      if (!this._disposed)
      {
        if (disposeManagedResources)
        {
          ////
          GC.SuppressFinalize(this);
        }
        //// dispose unmanaged resources
        this._disposed = true;
      }
    }

    #endregion Dispose


  }
}
