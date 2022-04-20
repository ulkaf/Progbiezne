using System;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract int Radius { get; }

        public static DataAbstractApi CreateApi()
        {
            return new DataApi();
        }
    }

    internal class DataApi : DataAbstractApi
    {
        public override int Radius => 100;
    }
}
