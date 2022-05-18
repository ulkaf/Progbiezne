using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract double GetX(int i);
        public abstract double GetNewX(int i);
        public abstract void SetNewX(int i, double value);
        public abstract double GetY(int i);
        public abstract double GetNewY(int i);
        public abstract void SetNewY(int i, double value);
        public abstract int GetSize(int i);
        public abstract double GetWeight(int i);
        public abstract int GetID(int i);
        public abstract int GetCount { get; }
        public abstract IList CreateBallsList(int count);
        public abstract int Width { get; }
        public abstract int Height { get; }


        public abstract IBall GetBall(int index);

        public static DataAbstractApi CreateApi(int width, int height)
        {
            return new DataApi(width, height);
        }
    }

    internal class DataApi : DataAbstractApi
    {
        private ObservableCollection<IBall> balls { get; }
        private readonly Mutex mutex = new Mutex();

        private readonly Random random = new Random();

        public override int Width { get; }
        public override int Height { get; }



        public DataApi(int width, int height)
        {
            balls = new ObservableCollection<IBall>();
            Width = width;
            Height = height;

        }

        public ObservableCollection<IBall> Balls => balls;

        public override IList CreateBallsList(int count)
        {

            if (count > 0)
            {
                int ballsCount = balls.Count;
                for (int i = 0; i < count; i++)
                {
                    mutex.WaitOne();
                    int radius = random.Next(20, 40);
                    double weight = random.Next(30, 60);
                    double x = random.Next(radius, Width - radius);
                    double y = random.Next(radius, Height - radius);
                    double newX = random.Next(-10, 10);
                    double newY = random.Next(-10, 10);
                    Ball ball = new Ball(i + ballsCount, radius, x, y, newX, newY, weight);

                    balls.Add(ball);
                    mutex.ReleaseMutex();

                }
            }
            if (count < 0)
            {
                for (int i = count; i < 0; i++)
                {

                    if (balls.Count > 0)
                    {
                        mutex.WaitOne();
                        balls.Remove(balls[balls.Count - 1]);
                        mutex.ReleaseMutex();
                    };

                }
            }
            return balls;
        }
        public override double GetX(int i)
        {
            return balls[i].X;
        }
        public override double GetNewX(int i)
        {
            return balls[i].NewX;
        }
        public override void SetNewX(int i, double value)
        {
            balls[i].NewX = value;
        }
        public override int GetCount { get => balls.Count; }

        public override double GetY(int i)
        {
            return balls[i].Y;
        }
        public override double GetNewY(int i)
        {
            return balls[i].NewY;
        }
        public override void SetNewY(int i, double value)
        {
            balls[i].NewY = value;
        }
        public override int GetSize(int i)
        {
            return balls[i].Size;
        }

        public override double GetWeight(int i)
        {
            return balls[i].Weight;
        }
        public override int GetID(int i)
        {
            return balls[i].ID;
        }

        public override IBall GetBall(int index)
        {
            return balls[index];
        }


    }
}
