using System;
using System.Collections.Generic;
using System.Text;
using Logic;

namespace Model
{
    public abstract class ModelAbstractApi 
    {  
        public static ModelAbstractApi CreateApi()
        {
            return new ModelApi();
        }
        public abstract List<Ball> AddBalls();
    }
    internal class ModelApi : ModelAbstractApi
    { 
        private LogicAbstractApi LogicLayer = LogicAbstractApi.CreateApi(800,800);
        public override List<Ball> AddBalls() 
        {
            LogicLayer.CreateBallsList(10);
            return LogicLayer.ballsList;
        }
    }

}
