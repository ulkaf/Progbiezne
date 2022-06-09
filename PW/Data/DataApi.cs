using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    internal class DataApi : DataAbstractApi
    {
      
        private readonly Random random = new Random();
        private readonly Stopwatch stopwatch;
        private readonly string logPath = "ball_log.json";
        private bool newSession;
        private bool stop;

        public override int Width { get; }
        public override int Height { get; }

        public DataApi(int width, int height)
        {
         
            Width = width;
            Height = height;
            newSession = true;
            stopwatch = new Stopwatch();
        }



        public override IBall CreateBall(int count)
        {

        
                    int radius = 30;
                    double weight = radius;

                    double x = random.Next(radius + 20, Width - radius - 20);
                    double y = random.Next(radius + 20, Height - radius - 20);
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
                    Ball ball = new Ball(count, radius, x, y, newX, newY, weight);

                   



            return ball;
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

        internal async Task CallLogger(int interval, IList Balls)
        {
            while (!stop)
            {
                stopwatch.Reset();
                stopwatch.Start();
                var options = new JsonSerializerOptions { WriteIndented = true };
               // string jsonBalls = JsonSerializer.Serialize(balls, options);
                string now = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");

             //  string newJsonObject = "{" + String.Format("\n\t\"datetime\": \"{0}\",\n\t\"balls\":{1}\n", now, jsonBalls) + "}";

               // AppendObjectToJSONFile(logPath, newJsonObject);
                stopwatch.Stop();
                await Task.Delay((int)(interval - stopwatch.ElapsedMilliseconds));
            }
        }
    }
}
