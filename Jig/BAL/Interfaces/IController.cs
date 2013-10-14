using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SixtyFourT.BAL
{
  public interface IController
  {
    //Start Controller
    void StartController();

    //Stop Controller
    void StopController();

    //Response to service by sending elevator
    Elevator ServiceRequest(Floor floor);

    //Start floor request Service
    void StartService(Elevator elevator);

    //enable disable elevator by chaing the new status
    //new status Status_Dormant equal to enable.
    //new status Status_Disabled equal to disabled.
    void SetElevatorStatus(Elevator elevator, string newStatus);

    //send particular elevator to floor request
    Elevator ConfigureElevatorForFloor(Elevator elevator, Floor floor);
  }
}
