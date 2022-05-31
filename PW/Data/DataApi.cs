using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading;

namespace Data
{
    internal class DataApi : DataAbstractApi
    {
        private ObservableCollection<IBall> balls { get; }


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
                    int radius = 30;
                    double weight = radius;
                    bool contin = true;
                    bool licz;
                    double x = random.Next(radius, Width - radius);
                    double y = random.Next(radius, Height - radius);




                    while (contin)
                    {
                        licz = false;
                        for (int j = 0; j < GetCount; j++)
                        {
                            if (x <= balls[j].X + balls[j].Size && x + radius >= balls[j].X)
                            {
                                if (y <= balls[j].Y + balls[j].Size && y + radius >= balls[j].Y)
                                {
                                    x = random.Next(radius, Width - radius);
                                    licz = true;
                                    break;
                                }
                            }
                        }
                        if (!licz)
                            contin = false;
                    }
                    double newX = 0;
                    double newY = 0;
                    while (newX == 0)
                    {
                        newX = random.Next(-5, 5) + random.NextDouble();
                    }
                    while (newY == 0)
                    {
                        newY = random.Next(-5, 5) + random.NextDouble();
                    }
                    Ball ball = new Ball(i + 1 + ballsCount, radius, x, y, newX, newY, weight);

                    balls.Add(ball);

                }
            }


            return balls;
        }

        public override IList DeleteBalls(int count)
        {
            for (int i = 0; i<count; i++)
            {

                if (balls.Count > 0)
                {
                    balls.Remove(balls[balls.Count - 1]);
                };

            }
            return Balls;
    
    
    }
        public override int GetCount { get => balls.Count; }



        public override IBall GetBall(int index)
        {
            return balls[index];
        }


    }
}
