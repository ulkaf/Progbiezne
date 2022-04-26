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


        public int Size { get => size; }
        public int X { get => x; }
        public int Y { get => y; }

        public void newPosition(int gridWidth, int gridHeight)
        {
            if (this.x + this.newX >= 0 && this.x + this.newX <= gridWidth - this.size)
                this.x += this.newX;
            else
            {
                if (this.newX > 0)
                    this.x = gridWidth - this.size;
                else
                    this.x = 0;

                this.newX *= -1;
            }

            if (this.y + this.newY >= 0 && this.y + this.newY <= gridHeight - this.size)
                this.y += this.newY;
            else
            {
                if (this.newY > 0)
                    this.y = gridHeight - this.size;
                else
                    this.y = 0;

                this.newY *= -1;
            }

        }
    }
}
