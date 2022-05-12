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
        public abstract int GetX(int i);
        public abstract int GetY(int i);
        public abstract int GetSize(int i);
        public abstract int GetCount { get; }
        public abstract IList CreateBallsList(int count);
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract void UpdateBallsList();

        public abstract void StopBalls();
        public static DataAbstractApi CreateApi(int width, int height)
        {
            return new DataApi(width,height);
        }
    }

    internal class DataApi : DataAbstractApi
    {
        private  ObservableCollection<Ball> balls { get; }

        private List<Task> tasks;
        public override int Width { get; }
        public override int Height { get; }

        public DataApi( int width, int height)
        {
            balls = new ObservableCollection<Ball>();
            Width = width;
            Height = height;
            tasks = new List<Task>();
        }

        public ObservableCollection<Ball> Balls => balls;

        public override IList CreateBallsList(int count)
        {
            Random random = new Random();
            if (count > 0)
            {
                for (uint i = 0; i < count; i++)
                {
                    int radius = random.Next(20, 40);
                    int x = random.Next(radius, Width - radius);
                    int y = random.Next(radius, Height - radius);
                    int newX = random.Next(radius);
                    int newY = random.Next(radius);
                    Ball ball = new Ball(radius, x, y, newX, newY);
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
        public override int GetX(int i)
        {
            return balls[i].X;
        }
        public override int GetCount { get => balls.Count; }

        public override int GetY(int i)
        {
            return balls[i].Y;
        }
        public override int GetSize(int i)
        {
            return balls[i].Size;
        }

        public int Tasks
        {
            get => tasks.Count;
        }

        public async void UpdateBall(Ball ball) 
        { while (true)
            {
                await Task.Delay(30);
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
        }

        public override void UpdateBallsList()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                Ball ball = balls[i];

               tasks.Add(Task.Run(() => UpdateBall(ball)));
                
            }
        }
        public override void StopBalls()
        {

        }
    }
}
