using System;

namespace Logic
{
    public class Ball
    {
        private int size;
        private int x;
        private int y;
        private int newX;
        private int newY;

        public Ball(int size, int x, int y, int newX, int newY)
        {
            this.size = size;
            this.x = x;
            this.y = y;
            this.newX = newX;
            this.newY = newY;

        }

        public static Ball getRecord()
        {
            var ba = new Ball(100, 20, 10, 5, 5);
            return ba;
        }

        public int Size { get => size; }
        public int X { get => x; }
        public int Y { get => y; }

        public void newPosition(int gridWidth, int gridHigh)
        {
            if (this.x + this.newX >= this.size && this.x + this.newX <= gridWidth - this.size)
                this.x += this.newX;
            else
            {
                if (this.x + this.newX > gridWidth - this.size)
                    this.x = gridWidth - this.size;
                else
                    this.x = this.size;

                this.newX *= -1;
            }

            if (this.y + this.newY >= this.size && this.y + this.newY <= gridHigh - this.size)
                this.y += this.newY;
            else
            {
                if (this.y + this.newY > gridHigh - this.size)
                    this.y = gridHigh - this.size;
                else
                    this.y = this.size;

                this.newY *= -1;
            }

        }
    }
}
