using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixtyFourT.BAL;
using System.Collections;

namespace UnitTestProject1
{

  [TestClass]
  public class UnitTest1
  {

    Controller _controller = new Controller();
    Elevator elevator1 = new Elevator();
    Elevator elevator2 = new Elevator();
    Hashtable hashtable = new Hashtable();
    Floor floor30 = new Floor(30);
    Floor floor80 = new Floor(80);
    Floor floor100 = new Floor(100);
    Floor floor0 = new Floor(0);

    public UnitTest1()
    {
      _controller.StartController();
    }

    [TestMethod]
    public void FloorRequestReturnElevator()
    {
      //this method checks the request sent from floor and returned elevator
      elevator1 = _controller.ServiceRequest(floor30);
      Assert.AreEqual(elevator1.CureentFloor, 30);
    }


    [TestMethod]
    public void ElevatorCheckMovingUp()
    {
      elevator1 = _controller.ServiceRequest(floor30);
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 2;
      _controller.StartService(elevator1);
      Assert.AreEqual(elevator1.Status, Utility.Status_Moving_Up);
    }

    [TestMethod]
    public void ElevatorCheckMovingDown()
    {
      elevator2 = _controller.ServiceRequest(floor80);
      elevator2.CureentFloor = 80;
      elevator2.Direction = 0;
      elevator2.FloorCount = 5;
      _controller.StartService(elevator2);
      Assert.AreEqual(elevator2.Status, Utility.Status_Moving_Down);
    }

    [TestMethod]
    public void ElevatorCheckDoorOpening()
    {
      elevator1 = _controller.ServiceRequest(floor30);
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 2;
      _controller.StartService(elevator1);
      //2 floors wait for 6 secs
      System.Threading.Thread.Sleep(6500);
      Assert.AreEqual(elevator1.Status, Utility.Status_Door_Opening);
    }

    [TestMethod]
    public void ElevatorCheckDoorClosing()
    {
      elevator1 = _controller.ServiceRequest(floor30);
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 2;
      _controller.StartService(elevator1);
      //2 floors wait for 6 +2 8 secs for door close
      System.Threading.Thread.Sleep(8500);
      Assert.AreEqual(elevator1.Status, Utility.Status_Door_Closing);
    }

    [TestMethod]
    public void ElevatorCheckDormant()
    {
      elevator1 = _controller.ServiceRequest(floor30);
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 2;
      _controller.StartService(elevator1);
      //2 floors wait for 6 +2 + 2 = 10 secs for dormant elevator
      System.Threading.Thread.Sleep(10500);
      Assert.AreEqual(elevator1.Status, Utility.Status_Dormant);
    }

    [TestMethod]
    public void ElevatorCheckMovingUp1Floor()
    {
      elevator1 = _controller.ServiceRequest(floor30);
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 3;
      _controller.StartService(elevator1);
      //1 floor wait for 3 secs for status of next floor
      System.Threading.Thread.Sleep(3500);
      Assert.AreEqual(elevator1.CureentFloor, 31);
    }

    [TestMethod]
    public void ElevatorCheckMovingUp2Floor()
    {
      elevator1 = _controller.ServiceRequest(floor30);
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 3;
      _controller.StartService(elevator1);
      //2 floors wait for 6 secs for status of 2nd floor
      System.Threading.Thread.Sleep(6500);
      Assert.AreEqual(elevator1.CureentFloor, 32);
    }

    [TestMethod]
    public void ElevatorCheckMovingDown1Floor()
    {
      elevator1 = _controller.ServiceRequest(floor30);
      elevator1.CureentFloor = 70;
      elevator1.Direction = 0;
      elevator1.FloorCount = 3;
      _controller.StartService(elevator1);
      //1 floor wait for 3 secs for status of next floor
      System.Threading.Thread.Sleep(3500);
      Assert.AreEqual(elevator1.CureentFloor, 69);
    }

    [TestMethod]
    public void ElevatorCheckMovingDown2Floor()
    {
      elevator1 = _controller.ServiceRequest(floor30);
      elevator1.CureentFloor = 70;
      elevator1.Direction = 0;
      elevator1.FloorCount = 3;
      _controller.StartService(elevator1);
      //2 floors wait for 6 secs for status of 2nd floor
      System.Threading.Thread.Sleep(6500);
      Assert.AreEqual(elevator1.CureentFloor, 68);
    }

    [TestMethod]
    public void TopFloorCheckUpButtonDisabled()
    {
      elevator1 = _controller.ServiceRequest(floor100);
      Assert.AreEqual(floor100.ButtonUp, false);
    }

    [TestMethod]
    public void TopFloorCheckDownButtonEnabled()
    {
      elevator1 = _controller.ServiceRequest(floor100);
      Assert.AreEqual(floor100.ButtonDown, true);
    }

    [TestMethod]
    public void BottomFloorCheckDownButtonDisabled()
    {
      elevator1 = _controller.ServiceRequest(floor0);
      Assert.AreEqual(floor0.ButtonDown, false);
    }

    [TestMethod]
    public void BottomFloorCheckUpButtonEnabled()
    {
      elevator1 = _controller.ServiceRequest(floor0);
      Assert.AreEqual(floor0.ButtonUp, true);
    }

    [TestMethod]
    public void Floor30CheckUpDownButtonEnabled()
    {
      elevator1 = _controller.ServiceRequest(floor30);
      bool upButton = floor30.ButtonUp;
      bool downButton = floor30.ButtonDown ;
      bool expectedresult = true;
      bool finalResult = false;
      if (upButton && downButton)
      {
        finalResult = true;
      }
      Assert.AreEqual(finalResult, expectedresult);
    }
    
    [TestMethod]
    public void ControllerMakeElevatorDisable()
    {
      //enable disable elevator by chaing the new status
      //new status Status_Dormant equal to enable.
      //new status Status_Disabled equal to disabled.
      _controller.SetElevatorStatus(elevator1, Utility.Status_Disabled);
      Assert.AreEqual(elevator1.Status, Utility.Status_Disabled);
    }

    [TestMethod]
    public void OutstandingRequestCheck()
    {

      Elevator elevator3 = new Elevator();
      Elevator elevator4 = new Elevator();
      Floor floor10 = new Floor(10);
      Floor floor70 = new Floor(70);

      elevator1 = _controller.ServiceRequest(floor30);
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 2;
      _controller.StartService(elevator1);

      elevator2 = _controller.ServiceRequest(floor80);
      elevator2.CureentFloor = 30;
      elevator2.Direction = 1;
      elevator2.FloorCount = 2;
      _controller.StartService(elevator2);

      elevator3 = _controller.ServiceRequest(floor100);
      elevator3.CureentFloor = 30;
      elevator3.Direction = 1;
      elevator3.FloorCount = 2;
      _controller.StartService(elevator3);

      elevator4 = _controller.ServiceRequest(floor0);
      elevator4.CureentFloor = 30;
      elevator4.Direction = 1;
      elevator4.FloorCount = 2;
      _controller.StartService(elevator4);

      //Fifth outstanding request 
      elevator1 = _controller.ServiceRequest(floor70);

      //total number of outstanding is one
      int pendReqCount = elevator1.PendingRequests.Count;

      Assert.AreEqual(1, pendReqCount);
    }

    [TestMethod]
    public void OutstandingRequestCheckFloorNo()
    {

      Elevator elevator3 = new Elevator();
      Elevator elevator4 = new Elevator();
      Floor floor10 = new Floor(10);
      Floor floor70 = new Floor(70);

      elevator1 = _controller.ServiceRequest(floor30);
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 2;
      _controller.StartService(elevator1);

      elevator2 = _controller.ServiceRequest(floor80);
      elevator2.CureentFloor = 30;
      elevator2.Direction = 1;
      elevator2.FloorCount = 2;
      _controller.StartService(elevator2);

      elevator3 = _controller.ServiceRequest(floor100);
      elevator3.CureentFloor = 30;
      elevator3.Direction = 1;
      elevator3.FloorCount = 2;
      _controller.StartService(elevator3);

      elevator4 = _controller.ServiceRequest(floor0);
      elevator4.CureentFloor = 30;
      elevator4.Direction = 1;
      elevator4.FloorCount = 2;
      _controller.StartService(elevator4);

      //Fifth request outstanding
      elevator1 = _controller.ServiceRequest(floor70);

      string floorNumber = "";
      string direction;
      //loop through all pending request 
      foreach (DictionaryEntry entry in elevator1.PendingRequests)
      {
        floorNumber = entry.Key.ToString();
        direction = entry.Value.ToString();
        if (direction == "1")
        {
          direction = Utility.Status_Moving_Up;
        }
        else
        {
          direction = Utility.Status_Moving_Down;
        }
      }
      Assert.AreEqual(floorNumber, "70");
    }

    [TestMethod]
    public void OutstandingRequestCheckDirection()
    {

      Elevator elevator3 = new Elevator();
      Elevator elevator4 = new Elevator();
      Floor floor10 = new Floor(10);
      Floor floor70 = new Floor(70);

      elevator1 = _controller.ServiceRequest(floor30);
      elevator1.CureentFloor = 30;
      elevator1.Direction = 1;
      elevator1.FloorCount = 2;
      _controller.StartService(elevator1);

      elevator2 = _controller.ServiceRequest(floor80);
      elevator2.CureentFloor = 30;
      elevator2.Direction = 1;
      elevator2.FloorCount = 2;
      _controller.StartService(elevator2);

      elevator3 = _controller.ServiceRequest(floor100);
      elevator3.CureentFloor = 30;
      elevator3.Direction = 1;
      elevator3.FloorCount = 2;
      _controller.StartService(elevator3);

      elevator4 = _controller.ServiceRequest(floor0);
      elevator4.CureentFloor = 30;
      elevator4.Direction = 1;
      elevator4.FloorCount = 2;
      _controller.StartService(elevator4);

      //Fifth outstanding request 
      elevator1 = _controller.ServiceRequest(floor70);

      string floorNumber = "";
      string direction = "";
      //loop through all pending request 
      foreach (DictionaryEntry entry in elevator1.PendingRequests)
      {
        floorNumber = entry.Key.ToString();
        direction = entry.Value.ToString();
        if (direction == "1")
        {
          direction = Utility.Status_Moving_Up;
        }
        else
        {
          direction = Utility.Status_Moving_Down;
        }
      }
      Assert.AreEqual(direction, Utility.Status_Moving_Down);
    }
    
    [TestMethod]
    public void ControllerConfigureElevatorFloor()
    {
      Floor floor50 = new Floor(50);
      //Get first elevator
      elevator1 = _controller.elevators[0];
      elevator1.CureentFloor = 48;
      floor50.CureentFloor = 50;
      _controller.ConfigureElevatorForFloor(elevator1, floor50);
      //wait for 10 secs = 2 floors 6 seconds + 4 for door open and close
      System.Threading.Thread.Sleep(10500);
      Assert.AreEqual(elevator1.CureentFloor, 50);
    }

    //For status of all elevators
    private void ControllerGetAllElevatorsStatus()
    {
      //by looping though the all elevatros 
      foreach (Elevator elevator in _controller.elevators)
      {
        //get the status of each elever
        //elevator.Direction
        //elevator.CureentFloor
       //etc...
      }
    }
  }
}
