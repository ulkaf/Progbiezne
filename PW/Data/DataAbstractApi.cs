using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract double GetX(int i);
        public abstract double GetY(int i);
        public abstract int GetSize(int i);
        public abstract double GetWeight(int i);
        public abstract int GetCount { get; }
        public abstract IList CreateBallsList(int count);
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract void UpdateBallsList();

        public static DataAbstractApi CreateApi(int width, int height)
        {
            return new DataApi(width,height);
        }
    }

    internal class DataApi : DataAbstractApi
    {
        private  ObservableCollection<Ball> balls { get; }

        private Random random = new Random();

        public override int Width { get; }
        public override int Height { get; }

     

        public DataApi( int width, int height)
        {
            balls = new ObservableCollection<Ball>();
            Width = width;
            Height = height;
            
        }

        public ObservableCollection<Ball> Balls => balls;

        public override IList CreateBallsList(int count)
        {
            
            if (count > 0)
            {
                for (uint i = 0; i < count; i++)
                {
                    int radius = random.Next(20, 40);
                    double weight = random.Next(30, 60);
                    double x = random.Next(radius, Width - radius);
                    double y = random.Next(radius, Height - radius);
                    double newX = random.Next(radius);
                    double newY = random.Next(radius);
                    Ball ball = new Ball(radius, x, y, newX, newY, weight);
                    balls.Add(ball);
                }
            }
            if (count < 0)
            {
                for (int i = count; i < 0; i++)
                {
                    if (balls.Count > 0)
                    {
                        balls.Remove(balls[balls.Count - 1]);
                    };
                }
            }
            return balls;
        }
        public override double GetX(int i)
        {
            return balls[i].X;
        }
        public override int GetCount { get => balls.Count; }

        public override double GetY(int i)
        {
            return balls[i].Y;
        }
        public override int GetSize(int i)
        {
            return balls[i].Size;
        }

        public override double GetWeight(int i)
        {
            return balls[i].Weight;
        }
   

        public  void UpdateBall(Ball ball) 
        { 
               
                if (ball.X + ball.NewX >= 0 && ball.X + ball.NewX <= Width - ball.Size)
                {
                    ball.X += ball.NewX;
                }
                else
                {
                    if (ball.NewX > 0)
                    {
                        ball.X = Width - ball.Size;
                    }
                    else
                    {
                        ball.X = 0;
                    }

                    ball.NewX *= -1;

                }

                if (ball.Y + ball.NewY >= 0 && ball.Y + ball.NewY <= Height - ball.Size)
                {
                    ball.Y += ball.NewY;
                }
                else
                {
                    if (ball.NewY > 0)
                    {
                        ball.Y = Height - ball.Size;
                    }
                    else
                    {
                        ball.Y = 0;
                    }

                    ball.NewY *= -1;
                }
            
        }

        public override void UpdateBallsList()
        {   
            for (int i = 0; i < balls.Count; i++)
            {
                Ball ball = balls[i];

               UpdateBall(ball);
                
            }
        }

    }
}
