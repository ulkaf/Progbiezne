using Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Logic
{
    public abstract class LogicAbstractApi
    {

        public abstract double GetX(int i);
        public abstract double GetY(int i);
        public abstract int GetSize(int i);
        public abstract int GetCount { get; }
        public abstract IList CreateBalls(int count);
        public abstract void Start();
        public abstract void Stop();





        public static LogicAbstractApi CreateApi(int width, int height)
        {
            return new LogicApi(width, height);
        }

    }
    internal class LogicApi : LogicAbstractApi
    {


        private readonly DataAbstractApi dataLayer;



        public LogicApi(int width, int height)
        {
            dataLayer = DataAbstractApi.CreateApi(width, height);


        }




        public override void Start()
        {

            dataLayer.StartBallMovement();


        }

        public override void Stop()
        {
            dataLayer.stopMovement();
        }




        public override IList CreateBalls(int count) => dataLayer.CreateBallsList(count);

        public override double GetX(int i)
        {
            return dataLayer.GetX(i);
        }
        public override int GetCount { get => dataLayer.GetCount; }

        public override double GetY(int i)
        {
            return dataLayer.GetY(i);
        }
        public override int GetSize(int i)
        {
            return dataLayer.GetSize(i);
        }

    }
}
