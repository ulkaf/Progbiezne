using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
   public interface ICollisionInfo
    {
         IBall Ball { get; }
         IBall SecondBall { get; }

        double OldNewX1 { get; }
        double OldNewY1 { get; }

        double OldNewX2 { get; }

         double OldNewY2 { get; }
    }
}
