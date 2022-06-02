using System.Collections;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAbstractApi
    {

        public abstract int GetCount { get; }
        public abstract IList CreateBallsList(int count);
        public abstract IList DeleteBalls(int count);
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract IBall GetBall(int index);
        public abstract IList GetBalls();

        public abstract void StopLoggingTask();

        public abstract Task CreateLoggingTask(int interval, IList Balls);

        public abstract void AppendObjectToJSONFile(string filename, string newJsonObject);


        public static DataAbstractApi CreateApi(int width, int height)
        {
            return new DataApi(width, height);
        }
    }


}
