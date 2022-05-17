using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : INotifyPropertyChanged
    {
        private readonly int size;
        private double x;
        private double y;
        private double newX;
        private double newY;
        private readonly double weight;
        private readonly Stopwatch stopwatch = new Stopwatch();
        private Task task;
        private bool stop = false;

        public Ball(int size, double x, double y, double newX, double newY, double weight)
        {
            this.size = size;
            this.x = x;
            this.y = y;
            this.newX = newX;
            this.newY = newY;
            this.weight = weight;
        }


        public int Size { get => size; }
        public double NewX
        {
            get => newX;
            set
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
            set
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
            set
            {
                if (value.Equals(x))
                {
                    return;
                }

                x = value;
                RaisePropertyChanged(nameof(X));
            }
        }
        public double Y
        {
            get => y;
            set
            {
                if (value.Equals(y))
                {
                    return;
                }

                y = value;
                RaisePropertyChanged(nameof(Y));
            }
        }

        public void Move(int interval)
        {
            X = x + NewX;
            Y = y + NewY;
        }

        public double Weight { get => weight; }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void CreateMovementTask(int interval)
        {
            this.stop = false;
            task = Run(interval);
        }

        private async Task Run(int interval)
        {
            while (!stop)
            {
                stopwatch.Reset();
                stopwatch.Start();
                if (!stop)
                {
                    Move(interval);
                    RaisePropertyChanged();
                }
                stopwatch.Stop();

                await Task.Delay((int)(interval - stopwatch.ElapsedMilliseconds));
            }
        }
        public void Stop()
        {
            this.stop = true;
        }



    }
}

