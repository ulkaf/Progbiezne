using System;
using System.Collections.Generic;
using Data;


namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract event EventHandler Update;

        public abstract int GetX(int i);
        public abstract int GetY(int i);
        public abstract int GetSize(int i);
        public abstract int GetCount { get; }
        public abstract void CreateBalls(int count);
        public abstract void Start();
        public abstract void Stop();
        public abstract void SetInterval(int ms);
        public abstract void UpdateBalls();



        public static LogicAbstractApi CreateApi(int width, int height, TimerApi timer = default(TimerApi))
        {
            return new LogicApi(width, height, timer ?? TimerApi.CreateBallTimer());
        }

    }
    internal class LogicApi : LogicAbstractApi
    {
        private readonly TimerApi timer;
       
        private DataAbstractApi dataLayer;
        public LogicApi(int width, int height, TimerApi WPFTimer)
        {
            dataLayer = DataAbstractApi.CreateApi(width, height);
            timer = WPFTimer;
            SetInterval(30);
            timer.Tick += (sender, args) => UpdateBalls();
        }
       

        public override event EventHandler Update { add => timer.Tick += value; remove => timer.Tick -= value; }

    
    

        public override void Start()
        {
            timer.Start();
        }

        public override void Stop()
        {
            timer.Stop();
        }

        public override void SetInterval(int ms)
        {
            timer.Interval = TimeSpan.FromMilliseconds(ms);
        }

        public override void UpdateBalls()
        {
           dataLayer.UpdateBallsList();
        }

        public override void CreateBalls(int count)
        {
            dataLayer.CreateBallsList(count);
        }

        public override int GetX(int i)
        {
            return dataLayer.GetX(i);
        }
        public override int GetCount { get => dataLayer.GetCount; }

        public override int GetY(int i)
        {
            return dataLayer.GetY(i);
        }
        public override int GetSize(int i)
        {
            return dataLayer.GetSize(i);
        }
    }
}
