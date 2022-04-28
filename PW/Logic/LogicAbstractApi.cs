using System;
using System.Collections.Generic;


namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract event EventHandler Update;
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract List<Ball> balls { get; }
        public abstract void CreateBallsList(int count);
        public abstract void UpdateBalls();
        public abstract void Start();
        public abstract void Stop();
        public abstract void SetInterval(int ms);

        public static LogicAbstractApi CreateApi(int width, int height, TimerApi timer = default(TimerApi))
        {
            return new LogicApi(width, height, timer ?? TimerApi.CreateBallTimer());
        }

    }
    internal class LogicApi : LogicAbstractApi
    {
        private readonly TimerApi timer;

        public override int Width { get; }
        public override int Height { get; }
        public override List<Ball> balls { get; }
        public LogicApi(int width, int height, TimerApi WPFTimer)
        {
            Width = width;
            Height = height;
            timer = WPFTimer;
            balls = new List<Ball>();
            SetInterval(30);
            timer.Tick += (sender, args) => UpdateBalls();
        }
        public override void CreateBallsList(int count)
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
            if (count< 0)
            {
                for (int i = count; i < 0; i++)
                {
                    if (balls.Count > 0)
                    {
                        balls.Remove(balls[balls.Count - 1]);
                    };
                }
            }
        }

        public override event EventHandler Update { add => timer.Tick += value; remove => timer.Tick -= value; }

        public override void UpdateBalls()
        {
            foreach (Ball ball in balls)
            {
                ball.newPosition(600, 480);

            }
        }

        public override void Start()
        {
            timer.Start();
        }

        public override void Stop()
        {
            timer.Stop();
        }

        public override void SetInterval(int ms)
        {
            timer.Interval = TimeSpan.FromMilliseconds(ms);
        }
    }
}
