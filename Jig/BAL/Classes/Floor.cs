using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SixtyFourT.BAL
{
  public class Floor : IFloor
  {
    #region Public Variables
    private bool _ButtonUp;
    private bool _ButtonDown;
    private Int32 _CureentFloor;
    private Int32 _FloorCount;
    #endregion

    #region Properties
    public Int32 CureentFloor
    {
      get { return this._CureentFloor; }
      set { this._CureentFloor = value; }
    }
    
    public bool ButtonUp
    {
      get { return this._ButtonUp; }
    }

    public bool ButtonDown
    {
      get { return this._ButtonDown; }
    }

    public Int32 FloorCount
    {
      get { return this._FloorCount; }
      set { this._FloorCount = value; }
    }

    public Int32 Direction { get; set; }
    #endregion Properties

    #region Constructors
    public Floor(Int32 currentFloor)
    {
      _CureentFloor = currentFloor;
      UpdateButton();
    }
    #endregion Constructors

    #region Public methods
    public void  UpdateButton()
    {
      if (_CureentFloor >= Utility.Top_Floor_No)
      {
        _ButtonUp = false;
        _ButtonDown = true;
        _CureentFloor = Utility.Top_Floor_No;
      }
      else if (_CureentFloor <= Utility.Bottom_Floor_No)
      {
        _ButtonUp = true;
        _ButtonDown = false ;
        _CureentFloor = Utility.Bottom_Floor_No;
      }
      else if (_CureentFloor >= Utility.Bottom_Floor_No)
      {
        _ButtonUp = true;
        _ButtonDown = true;
      }
      CureentFloor = _CureentFloor;
    }
    #endregion Public methods

  }
}
