using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Ballnfo
    {
        int ID { get; }
        double X { get; }
        double Y { get; }
        double NewX { get; }
        double NewY { get; }
        int WallCollisionCount { get; }
        int BallCollisionCount { get; }

        public Ballnfo(int id, double x, double y, double newX, double newY, int wallCollisionCount, int ballCollisionCount)
        {
            ID = id;
            X = x;
            Y = y;
            NewX = newX;
            NewY = newY;
            WallCollisionCount = wallCollisionCount;
            BallCollisionCount = ballCollisionCount;
        }
    }
}
