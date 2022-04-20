using System;

namespace Logic
{
    public class Ball
    {
        private int size ;
        private int x;
        private int y;

        public Ball(int size, int x, int y)
        {
            this.size = size;
            this.x = x;
            this.y = y;
        }

        public static Ball getRecord()
        {
            var ba = new Ball(100, 20, 10);
            return ba;
        }

        public int Size { get => size; set => size = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }
}
