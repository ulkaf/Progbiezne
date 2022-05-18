using Logic;
using System.Collections;



namespace Model
{
    public abstract class ModelAbstractApi
    {
        public abstract int width { get; }
        public abstract int height { get; }
        public abstract void StartMoving();
        public abstract IList Start(int ballVal);
        public abstract void Stop();


        public static ModelAbstractApi CreateApi(int Weight, int Height)
        {
            return new ModelApi(Weight, Height);
        }
    }
    internal class ModelApi : ModelAbstractApi
    {
        public override int width { get; }
        public override int height { get; }
        private readonly LogicAbstractApi LogicLayer;

        public ModelApi(int Width, int Height)
        {

            width = Width;
            height = Height;
            LogicLayer = LogicAbstractApi.CreateApi(width, height);


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
