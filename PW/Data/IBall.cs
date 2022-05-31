using System.ComponentModel;

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
        void Move(double time);
        void CreateMovementTask(int interval);
        void Stop();
    }
}
