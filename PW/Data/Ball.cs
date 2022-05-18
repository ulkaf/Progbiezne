using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Data

{
    public interface IBall : INotifyPropertyChanged
    {
        int ID { get; }
        int Size { get; }
        double Weight { get; }
        double X { get; set; }
        double Y { get; set; }
        double NewX { get; set; }
        double NewY { get; set; }

        void Move();
        void CreateMovementTask(int interval);

        void Stop();

        


    }

    internal class Ball : IBall
    {
        private readonly int size;
        private readonly int id;
        private double x;
        private double y;
        private double newX;
        private double newY;
        private readonly double weight;
        private readonly Stopwatch stopwatch = new Stopwatch();
        private Task task;
        private bool stop = false;

        public Ball(int identyfikator, int size, double x, double y, double newX, double newY, double weight)
        {
            id = identyfikator;
            this.size = size;
            this.x = x;
            this.y = y;
            this.newX = newX;
            this.newY = newY;
            this.weight = weight;
        }

        public int ID { get => id; }
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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
            }
        }

        public void Move()
        {
            X +=  NewX;
            Y +=  NewY;
        }


        public double Weight { get => weight; }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void CreateMovementTask(int interval)
        {
            stop = false;
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
                    Move();
                 
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

