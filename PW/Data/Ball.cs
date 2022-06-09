using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Data

{
    internal class Ball : IBall
    {
        private readonly int size;
        private readonly int id;
        private double x;
        private double y;
        private double newX;
        private double newY;
        private readonly double weight;
        private readonly Stopwatch stopwatch;
        private bool stop;

        private readonly object locker = new object();
        public Ball(int identyfikator, int size, double x, double y, double newX, double newY, double weight)
        {
            id = identyfikator;
            this.size = size;
            this.x = x;
            this.y = y;
            this.newX = newX;
            this.newY = newY;
            this.weight = weight;
            stop = false;
            stopwatch = new Stopwatch();

        }

        public int ID { get => id; }
        public int Size { get => size; }
        public double Weight { get => weight; }

        public void changeVelocity(double Vx, double Vy)
        {
            lock (locker)
            {
                NewX = Vx;
                NewY = Vy;


            }
        }

        public double NewX
        {
            get => newX;
            private set
            {
                if (value.Equals(newX))
                {
                    return;
                }

                newX = value;
            }
        }
        public double NewY
        {
            get => newY;
            private set
            {
                if (value.Equals(newY))
                {
                    return;
                }

                newY = value;
            }
        }
        public double X
        {
            get => x;
            private set
            {
                if (value.Equals(x))
                {
                    return;
                }

                x = value;

            }
        }
        public double Y
        {
            get => y;
            private set
            {
                if (value.Equals(y))
                {
                    return;
                }

                y = value;

            }
        }


        public void SaveRequest(ConcurrentQueue<IBall> queue)
        {
            queue.Enqueue(new Ball(ID, Size, X, Y, NewX, NewY, Weight));
        }
        public void Move(double time, ConcurrentQueue<IBall> queue)
        {
            lock (locker)
            {
                X += NewX * time;
                Y += NewY * time;
                RaisePropertyChanged(nameof(X));
                RaisePropertyChanged(nameof(Y));
                SaveRequest(queue);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Task CreateMovementTask(int interval, ConcurrentQueue<IBall> queue)
        {
            stop = false;
            return Run(interval, queue);
        }

        private async Task Run(int interval, ConcurrentQueue<IBall> queue)
        {
            while (!stop)
            {
                stopwatch.Reset();
                stopwatch.Start();
                if (!stop)
                {
                    Move(((interval - stopwatch.ElapsedMilliseconds) / 16), queue);
                }
                stopwatch.Stop();

                await Task.Delay((int)(interval - stopwatch.ElapsedMilliseconds));
            }
        }
        public void Stop()
        {
            stop = true;
        }

    }
}

