using Data;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Logic
{
    internal class LogicApi : LogicAbstractApi
    {
        private readonly DataAbstractApi dataLayer;
        private ObservableCollection<IBall> balls { get; }

        private ConcurrentQueue<IBall> queue;

        public LogicApi(int width, int height)
        {
            dataLayer = DataAbstractApi.CreateApi(width, height);
            Width = width;
            Height = height;
            balls = new ObservableCollection<IBall>();
            queue = new ConcurrentQueue<IBall>();
        }

        public override int Width { get; }
        public override int Height { get; }
        public override void Start()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].PropertyChanged += BallPositionChanged;  
                balls[i].CreateMovementTask(30, queue);
            }
            dataLayer.CreateLoggingTask(queue);

        }
        public override void Stop()
        {
            for (int i = 0; i <balls.Count; i++)
            {
                balls[i].Stop();
                balls[i].PropertyChanged -= BallPositionChanged;

            }
            
        }


        public override IList CreateBalls(int count)
        {
           

            for (int i = 0; i < count; i++)
            {
                bool contain = true;
                bool licz;
                
               
                while (contain)
                {
                    balls.Add(dataLayer.CreateBall(i + 1));
                    licz = false;
                    for (int j = 0; j < i; j++)
                    {

                        if (balls[i].X <= balls[j].X + balls[j].Size && balls[i].X + balls[i].Size >= balls[j].X)
                        {
                            if (balls[i].Y <= balls[j].Y + balls[j].Size && balls[i].Y + balls[i].Size >= balls[j].Y)
                            {

                                licz = true;
                                balls.Remove(balls[i]);
                                break;
                            }
                        }
                    }
                    if (!licz)
                    {
                        contain = false;
                    }
                }
 
               
              

            }
            return balls;
        }

        public override IList DeleteBalls(int count)
        {
            for (int i = 0; i < count; i++)
            {

                if (balls.Count > 0)
                {
                    balls.Remove(balls[balls.Count - 1]);
                };

            }
            return balls;
        }
      

        internal void WallCollision(IBall ball)
        {

            double diameter = ball.Size;
            double right = Width - diameter;
            double down = Height - diameter;
            if (ball.X <= 5)
            {
                if (ball.NewX <= 0)
                {
                    ball.changeVelocity(-ball.NewX, ball.NewY, true);
              

                }
            }

            else if (ball.X >= right - 5)
            {
                if (ball.NewX > 0)
                {
                    ball.changeVelocity(-ball.NewX, ball.NewY, true);
                }
            }
            if (ball.Y <= 5)
            {
                if (ball.NewY <= 0)
                {
                    ball.changeVelocity(ball.NewX, -ball.NewY, true);
                }
            }

            else if (ball.Y >= down - 5)
            {
                if (ball.NewY > 0)
                {
                    ball.changeVelocity(ball.NewX, -ball.NewY, true);
                }
            }
        }

        internal void BallBounce(IBall ball)
        {
            for (int i = 0; i <balls.Count; i++)
            {
                IBall secondBall = balls[i];
                if (ball.ID == secondBall.ID)
                {
                    continue;
                }

                if (Collision(ball, secondBall))
                {
                    double relativeX = ball.X - secondBall.X;
                    double relativeY = ball.Y - secondBall.Y;
                    double relativeNewX = ball.NewX - secondBall.NewX;
                    double relativeNewY = ball.NewY - secondBall.NewY;
                    if (relativeX * relativeNewX + relativeY * relativeNewY > 0)
                    {
                        return;
                    }

                    lock (ball)
                    {
                        double u1x;
                        double u1y;
                        double m1 = ball.Weight;
                        double v1x = ball.NewX;
                        double v1y = ball.NewY;

                        lock (secondBall)
                        {

                            double m2 = secondBall.Weight;
                            double v2x = secondBall.NewX;
                            double v2y = secondBall.NewY;


                            u1x = (m1 - m2) * v1x / (m1 + m2) + (2 * m2) * v2x / (m1 + m2);
                            u1y = (m1 - m2) * v1y / (m1 + m2) + (2 * m2) * v2y / (m1 + m2);


                            double u2x = 2 * m1 * v1x / (m1 + m2) + (m2 - m1) * v2x / (m1 + m2);
                            double u2y = 2 * m1 * v1y / (m1 + m2) + (m2 - m1) * v2y / (m1 + m2);

                            secondBall.changeVelocity(u2x, u2y, false);

                        }

                        ball.changeVelocity(u1x, u1y, false);

                    }
                    return;
                }
            }

        }


        internal bool Collision(IBall a, IBall b)
        {
            if (a == null || b == null)
            {
                return false;
            }

            return Distance(a, b) <= (a.Size / 2 + b.Size / 2);
        }

        internal double Distance(IBall a, IBall b)
        {
            double x1 = a.X + a.Size / 2;
            double y1 = a.Y + a.Size / 2;
            double x2 = b.X + b.Size / 2;
            double y2 = b.Y + b.Size / 2;

            return Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
        }

        internal void BallPositionChanged(object sender, PropertyChangedEventArgs args)
        {
            IBall ball = (IBall)sender;
            WallCollision(ball);
            BallBounce(ball);
        }
    }
}
