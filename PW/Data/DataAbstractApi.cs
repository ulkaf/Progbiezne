using System.Collections;

namespace Data
{
    public abstract class DataAbstractApi
    {

        public abstract int GetCount { get; }
        public abstract IList CreateBallsList(int count);
        public abstract int Width { get; }
        public abstract int Height { get; }


        public abstract IBall GetBall(int index);

        public static DataAbstractApi CreateApi(int width, int height)
        {
            return new DataApi(width, height);
        }
    }

   
}
