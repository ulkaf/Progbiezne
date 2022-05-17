using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

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
        public abstract void stopMovement();
        public abstract void StartBallMovement();

        public static DataAbstractApi CreateApi(int width, int height)
        {
            return new DataApi(width, height);
        }
    }

    internal class DataApi : DataAbstractApi
    {
        private ObservableCollection<Ball> balls { get; }
        private readonly Mutex mutex = new Mutex();

        private readonly Random random = new Random();

        public override int Width { get; }
        public override int Height { get; }



        public DataApi(int width, int height)
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
                    double newX = random.Next(5, 15);
                    double newY = random.Next(5, 15);
                    Ball ball = new Ball(radius, x, y, newX, newY, weight);
                    ball.PropertyChanged += BallPositionChanged;
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



        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public override void StartBallMovement()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                Ball ball = balls[i];
                ball.CreateMovementTask(30);

            }
        }

        public override void stopMovement()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                Ball ball = balls[i];

                ball.Stop();

            }
        }

        public void WallCollision(Ball ball)
        {

            double diameter = ball.Size;

            double right = 600 - diameter;

            double down = 480 - diameter;


            if (ball.X + ball.NewX <= 0)
            {
                ball.NewX = -ball.NewX;
            }

            if (ball.X + ball.NewX >= right)
            {

                ball.NewX = -ball.NewX;
            }
            if (ball.Y + ball.NewY <= 0)
            {
                ball.NewY = -ball.NewY;
            }

            if (ball.Y + ball.NewY >= down)
            {
                ball.NewY = -ball.NewY;
            }
        }
        public void BallPositionChanged(object sender, PropertyChangedEventArgs args)
        {
            Ball ball = (Ball)sender;
            mutex.WaitOne();
            // UpdateBall(ball);
            WallCollision(ball);
            RaisePropertyChanged();
            mutex.ReleaseMutex();
        }
     
    }
}
