using Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    internal class ModelApi : ModelAbstractApi
    {
        public override int Width { get; }
        public override int Height { get; }
        private readonly LogicAbstractApi LogicLayer;

        public ModelApi(int width, int height)
        {

            Width = width;
            Height = height;
            LogicLayer = LogicAbstractApi.CreateApi(Width, Height);


        }

        public override void StartMoving()
        {
            LogicLayer.Start();
        }


        public override void Stop()
        {
            LogicLayer.Stop();
        }

        public override IList Start(int ballVal) => LogicLayer.CreateBalls(ballVal);

    }
}
