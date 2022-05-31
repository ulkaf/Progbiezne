using System.Collections;



namespace Model
{
    public abstract class ModelAbstractApi
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract void StartMoving();
        public abstract IList Start(int ballVal);
        public abstract void Stop();


        public static ModelAbstractApi CreateApi(int Weight, int Height)
        {
            return new ModelApi(Weight, Height);
        }
    }
    

}
