using System.Collections.Concurrent;
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
        double NewX { get; }
        double NewY { get; }
        void changeVelocity(double Vx, double Vy);
        void Move(double time, ConcurrentQueue<IBall> queue);
        Task CreateMovementTask(int interval, ConcurrentQueue<IBall> queue);
        void SaveRequest(ConcurrentQueue<IBall> queue);
        void Stop();
    }
}
