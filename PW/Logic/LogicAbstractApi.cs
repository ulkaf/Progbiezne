using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data;


namespace Logic
{
    public abstract class LogicAbstractApi
    {
       

        public abstract int GetX(int i);
        public abstract int GetY(int i);
        public abstract int GetSize(int i);
        public abstract int GetCount { get; }
        public abstract IList CreateBalls(int count);
        public abstract void Start();
        public abstract void Stop();
        
        public abstract void UpdateBalls();



        public static LogicAbstractApi CreateApi(int width, int height)
        {
            return new LogicApi(width, height);
        }

    }
    internal class LogicApi : LogicAbstractApi
    {
       
       
        private DataAbstractApi dataLayer;
        private List<Task> tasks;
        private bool stop = false;

        public LogicApi(int width, int height)
        {
            dataLayer = DataAbstractApi.CreateApi(width, height);
            tasks = new List<Task>();
           
        }



        public int Tasks
        {
            get => tasks.Count;
        }



        public override void Start()
        {
            stop = false;
            tasks.Add(Task.Run(() => UpdateBalls()));
            
        }

        public override void Stop()
        {
         stop = true;
        }



        public override async void UpdateBalls()
        {
            while (!stop)
            {
                await Task.Delay(30);
                dataLayer.UpdateBallsList();
            }
            }

        public override IList CreateBalls(int count) => dataLayer.CreateBallsList(count);
      
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
