﻿using System.Collections;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAbstractApi
    {


        public abstract IBall CreateBall(int count);
        public abstract int Width { get; }
        public abstract int Height { get; }
   


        public abstract void StopLoggingTask();

        public abstract Task CreateLoggingTask(int interval, IList Balls);

        public abstract void AppendObjectToJSONFile(string filename, string newJsonObject);


        public static DataAbstractApi CreateApi(int width, int height)
        {
            return new DataApi(width, height);
        }
    }


}
