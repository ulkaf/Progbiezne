using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract int Width { get;}
        public abstract int Height { get;}
        public abstract List<Ball> balls { get; }
        public abstract void CreateBallsList(int count);
        public static LogicAbstractApi CreateApi( int width, int height)
        {
            return new LogicApi(width, height);
        }

    }
    internal class LogicApi : LogicAbstractApi
    { 
        public override int Width { get; }
        public override int Height { get; }
        public override List<Ball> balls { get;}
        public LogicApi( int width, int height)
        {
            this.Width = width;
            this.Height = height;
            balls = new List<Ball>();
        }
        public override void CreateBallsList(int count)
        {  
            Random random = new Random();
            for (uint i = 0; i < count; ++i)
            {
                int radius = random.Next(20, 40);
                int x = random.Next(radius, Width - radius);
                int y = random.Next(radius, Height - radius);
                int newX = random.Next(radius);
                int newY = random.Next(radius);
                Ball ball = new Ball(radius,x,y,newX,newY);
                balls.Add(ball);
            }
        }
    }
}
