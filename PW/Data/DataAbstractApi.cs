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
                int ballsCount = balls.Count;
                for (int i = 0; i < count; i++)
                {   
                    int radius = random.Next(20, 40);
                    double weight = random.Next(30, 60);
                    double x = random.Next(radius, Width - radius);
                    double y = random.Next(radius, Height - radius);
                    double newX = random.Next(1, 10);
                    double newY = random.Next(1, 10);
                    Ball ball = new Ball(i+ballsCount,radius, x, y, newX, newY, weight);
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


        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public override void StartBallMovement()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].CreateMovementTask(30);

            }
        }

        public override void stopMovement()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Stop();
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

            if (ball.Y + ball.NewY>= down)
            {
                ball.NewY = -ball.NewY;
            }
        }

        public void BallBounce(Ball ball)
        {
            for (int i = 0; i < balls.Count; i++)
            {
                Ball secondBall = balls[i];
                if (ball.ID == secondBall.ID) continue;
                if (Collision(ball, secondBall))
                {

                    double m1 = ball.Weight;
                    double m2 = secondBall.Weight;
                    double v1x = ball.NewX;
                    double v1y = ball.NewY;
                    double v2x = secondBall.NewX;
                    double v2y = secondBall.NewY;

                    double u1x = (m1 - m2) * v1x / (m1 + m2) + (2 * m2) * v2x / (m1 + m2);
                    double u1y = (m1 - m2) * v1y / (m1 + m2) + (2 * m2) * v2y / (m1 + m2);

                    double u2x = 2 * m1 * v1x / (m1 + m2) + (m2 - m1) * v2x / (m1 + m2);
                    double u2y = 2 * m1 * v1y / (m1 + m2) + (m2 - m1) * v2y / (m1 + m2);

                    ball.NewX = u1x;
                    ball.NewY = u1y;
                    secondBall.NewX = u2x;
                    secondBall.NewY = u2y;

                }



            }

        }




        public bool Collision(Ball a, Ball b)
        {
            if (a == null || b == null)
                return false;

            return Distance(a, b) <= (a.Size / 2 + b.Size / 2);
        }

        private double Distance(Ball a, Ball b)
        {
            double x1 = a.X + a.Size / 2 + a.NewX;
            double y1 = a.Y + a.Size / 2 + a.NewY;
            double x2 = b.X + b.Size / 2 + b.NewY;
            double y2 = b.Y + b.Size / 2 + b.NewY;

            return Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
        }

        public void BallPositionChanged(object sender, PropertyChangedEventArgs args)
        {
            Ball ball = (Ball)sender;
            mutex.WaitOne();
            WallCollision(ball);
            BallBounce(ball);
            mutex.ReleaseMutex();
        }

    }
}
