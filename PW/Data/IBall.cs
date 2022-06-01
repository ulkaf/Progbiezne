using System.ComponentModel;
using System.Threading.Tasks;

namespace Data
{
    public interface IBall : INotifyPropertyChanged
    {
        int ID { get; }
        int Size { get; }
        double Weight { get; }
        double X { get; }
        double Y { get; }
        double NewX { get; set; }
        double NewY { get; set; }
        void Move(double time);
        Task CreateMovementTask(int interval);
        void Stop();
    }
}
