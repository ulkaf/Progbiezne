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

        public int Size { get => size; set => size = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }
}
