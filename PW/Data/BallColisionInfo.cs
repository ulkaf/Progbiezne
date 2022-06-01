﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class BallColisionInfo
    {
        public BallColisionInfo(IBall ball, double oldNewX1, double oldNewY1, IBall secondBall, double oldNewX2, double oldNewY2)
        {
          Ball = ball;
          OldNewX1 = oldNewX1;
          OldNewY1 = oldNewY1;
          OldNewX2 = oldNewX2;
          OldNewY2 = oldNewY2;
          SecondBall = secondBall;
        }

      


        public IBall Ball { get; }
        public IBall SecondBall { get; }

        public double OldNewX1 { get; }
        public double OldNewY1 { get; }

        public double OldNewX2 { get; }

        public double OldNewY2 { get; }

    }

}