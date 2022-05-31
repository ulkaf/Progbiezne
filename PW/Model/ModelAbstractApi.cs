using System.Collections;



namespace Model
{
    public abstract class ModelAbstractApi
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract void StartMoving();
        public abstract IList Create(int ballVal);

        public abstract IList Delete(int ballVal);
        public abstract void Stop();


        public static ModelAbstractApi CreateApi(int Weight, int Height)
        {
            return new ModelApi(Weight, Height);
        }
    }
    

}
