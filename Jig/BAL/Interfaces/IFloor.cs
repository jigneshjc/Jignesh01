using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtyFourT.BAL
{
  public interface IFloor
  {
    // current floor number
    Int32 CureentFloor { get; set; }
    
    //Direction to Travel
    Int32 Direction { get; set; }

    //Floor Button Up
    bool ButtonUp { get; }

    //Floor Button Down
    bool ButtonDown { get; }

    //Method for update of button when the floor is top or bottom
    void UpdateButton();
  }
}
