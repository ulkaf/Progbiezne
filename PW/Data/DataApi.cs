using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    internal class DataApi : DataAbstractApi
    {
        private ObservableCollection<IBall> balls { get; }


        private readonly Random random = new Random();
        private readonly object locker = new object();
        private readonly Stopwatch stopwatch;
        private string logPath = "ball_log.json";
        private bool newSession;
        private bool stop;

        public override int Width { get; }
        public override int Height { get; }

      



        public DataApi(int width, int height)
        {
            balls = new ObservableCollection<IBall>();
            Width = width;
            Height = height;
            newSession = true;
            stopwatch = new Stopwatch();
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

        public override IList GetBalls()
        {
            return Balls;
        }


        public override void StopLoggingTask()
        {
            stop = true;
        }

        public override Task CreateLoggingTask(int interval, IList Balls)
        {
            stop = false;
            return CallLogger(interval, Balls);
        }


        public override void AppendObjectToJSONFile(string filename, string newJsonObject)
        {
          
            if (File.Exists(filename) && newSession)
            {
                newSession = false;
                File.Delete(filename);
            }

            using (StreamWriter sw = new StreamWriter(filename, true))
            {
                sw.WriteLine("[]");
            }

            string content;
            using (StreamReader sr = File.OpenText(filename))
            {
                content = sr.ReadToEnd();
            }

            content = content.TrimEnd();
            content = content.Remove(content.Length - 1, 1);
         
            if (content.Length == 1)
            {
                content = String.Format("{0}\n{1}\n]\n", content.Trim(), newJsonObject);
            }
            else
            {
                content = String.Format("{0},\n{1}\n]\n", content.Trim(), newJsonObject);
            }

            using (StreamWriter sw = File.CreateText(filename))
            {
                sw.Write(content);
            }
        }


        public override BallColisionInfo GetBallColisionInfo(IBall ball,double v1x,double v1y, IBall secondBall,double v2x,double v2y)
        {
            return new BallColisionInfo(ball,v1x,v1y, secondBall, v2x,  v2y);
        }

        public override WallColisionInfo GetWallColisionInfo(IBall ball, double oldNewX, double oldNewY)
        {
            return new WallColisionInfo(ball,oldNewX,oldNewY);
        }


        internal async Task CallLogger(int interval, IList Balls)
        {
            while (!stop)
            {
                stopwatch.Reset();
                stopwatch.Start();
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonBalls = JsonSerializer.Serialize(balls, options);
                string now = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");

                string newJsonObject = "{" + String.Format("\n\t\"datetime\": \"{0}\",\n\t\"balls\":{1}\n", now, jsonBalls) + "}";
                lock (locker)
                {
                    AppendObjectToJSONFile(logPath, newJsonObject);
                }
                stopwatch.Stop();
                await Task.Delay((int)(interval - stopwatch.ElapsedMilliseconds));
            }
        }
    }
}
