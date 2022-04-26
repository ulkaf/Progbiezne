using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract int gridWidth { get; }
        public abstract int gridHeight { get; }
        
        public abstract void CreateBallsList(int count,List<Ball>balls);
        public abstract void DeleteBalls();
        public abstract void UpdateBalls();
        public static LogicAbstractApi CreateApi(int width, int height)
        {
            return new LogicApi(width, height);
        }

    }
    internal class LogicApi : LogicAbstractApi
    { 
        public override int gridWidth { get; }
        public override int gridHeight { get; }
       

        public LogicApi(int width, int height)
        {
            this.gridWidth = width;
            this.gridHeight = height;
        }
        public override void CreateBallsList(int count,List<Ball>balls)
        {  
            Random random = new Random();
            for (uint i = 0; i < count; ++i)
            {
                int radius = random.Next(10, 30);
                int x = random.Next(radius, gridWidth - radius);
                int y = random.Next(radius, gridHeight - radius);
                int newX = random.Next(radius);
                int newY = random.Next(radius);
                Ball ball = new Ball(radius,x,y,newX,newY);
                balls.Add(ball);
            }
           
        }
        public override void UpdateBalls()
        {
      //      foreach (Ball ball in ballsList)
       ///     {
       //         ball.newPosition(gridWidth, gridHeight);
      //      }
       }
        public override void DeleteBalls()
        {
            //ballsList.Clear();
        }
    }
}
