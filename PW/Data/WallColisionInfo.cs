using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class WallColisionInfo
    {
        public WallColisionInfo(IBall ball, double oldNewX, double oldNewY)
        {
            Ball = ball;
            OldNewX = oldNewX;
            OldNewY = oldNewY;
        }

        public IBall Ball { get; }

        public double OldNewX { get; }
        public double OldNewY { get; }

    }
}
