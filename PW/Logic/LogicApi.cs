using Data;
using System;
using System.Collections;
using System.ComponentModel;
using System.Text.Json;
using System.Threading;

namespace Logic
{
    internal class LogicApi : LogicAbstractApi
    {
        private readonly DataAbstractApi dataLayer;
        private string logPath = "ball_log.json";
        private readonly object locker = new object();  

        public LogicApi(int width, int height)
        {
            dataLayer = DataAbstractApi.CreateApi(width, height);
            Width = width;
            Height = height;
     
        }

        public override int Width { get; }
        public override int Height { get; }

        public override void Start()
        {   
            for (int i = 0; i < dataLayer.GetCount; i++)
            {
                dataLayer.GetBall(i).CreateMovementTask(30);

            }
            dataLayer.CreateLoggingTask(1000, dataLayer.GetBalls());
        }

        public override void Stop()
        {
            for (int i = 0; i < dataLayer.GetCount; i++)
            {
                dataLayer.GetBall(i).Stop();

            }
            dataLayer.StopLoggingTask();
        }
        public override IList CreateBalls(int count)
        {
            int previousCount = dataLayer.GetCount;
            IList temp = dataLayer.CreateBallsList(count);
            for (int i = 0; i < dataLayer.GetCount - previousCount; i++)
            {
                dataLayer.GetBall(previousCount + i).PropertyChanged += BallPositionChanged;
            }
            return temp;
        }

        public override IList DeleteBalls(int count)
        {
            return dataLayer.DeleteBalls(count);
        }
        public override IBall GetBall(int index)
        {
            return dataLayer.GetBall(index);
        }


        public override int GetCount { get => dataLayer.GetCount; }


        internal void WallCollision(IBall ball)
        {
            bool collision = false;
            double diameter = ball.Size;

            double right = Width - diameter;

            double down = Height - diameter;

            double oldNewX = ball.NewX;
            double oldNewY = ball.NewY;
            if (ball.X <= 5)
            {
                if (ball.NewX <= 0)
                {
                    ball.NewX = -ball.NewX;
                    collision = true;
                }
            }

            else if (ball.X >= right -5)
            {
                if (ball.NewX > 0)
                {
                    ball.NewX = -ball.NewX;
                    collision = true;
                }
            }
            if (ball.Y <= 5)
            {
                if (ball.NewY <= 0)
                {
                    ball.NewY = -ball.NewY;
                    collision = true;
                }
            }

            else if (ball.Y >= down - 5)
            {
                if (ball.NewY > 0)
                {
                    ball.NewY = -ball.NewY;
                    collision = true;
                }
            }


          
            if (collision == true)
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonCollisionInfo = JsonSerializer.Serialize(dataLayer.GetWallColisionInfo(ball,oldNewX,oldNewY), options);
                string now = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
                string newJsonObject = "{" + String.Format("\n\t\"datetime\": \"{0}\",\n\t\"WallCollision\":{1}\n", now, jsonCollisionInfo) + "}";

                dataLayer.AppendObjectToJSONFile(logPath, newJsonObject);
            }
            


        }

        internal void BallBounce(IBall ball)
        {
            for (int i = 0; i < dataLayer.GetCount; i++)
            {
                IBall secondBall = dataLayer.GetBall(i);
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
                    if(relativeX*relativeNewX+relativeY*relativeNewY >0)
                    {
                        return;
                    }

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

                    lock (locker)

                    {
                        ball.NewX = u1x;
                        ball.NewY = u1y;
                        secondBall.NewX = u2x;
                        secondBall.NewY = u2y;

                        var options = new JsonSerializerOptions { WriteIndented = true };
                        string jsonCollisionInfo = JsonSerializer.Serialize(dataLayer.GetBallColisionInfo(ball,v1x,v1y,secondBall,v2x,v2y), options);
                        string now = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
                        string newJsonObject = "{" + String.Format("\n\t\"datetime\": \"{0}\",\n\t\"BallCollision\":{1}\n", now, jsonCollisionInfo) + "}";

                        dataLayer.AppendObjectToJSONFile(logPath, newJsonObject);

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
            double x1 = a.X + a.Size / 2 ;
            double y1 = a.Y + a.Size / 2 ;
            double x2 = b.X + b.Size / 2 ;
            double y2 = b.Y + b.Size / 2 ;

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
