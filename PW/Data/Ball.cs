using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Data
{
    internal class Ball : INotifyPropertyChanged
    {
        private readonly int size;
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
        public int X { get => x;
            set
            {
                if (value.Equals(x)) return;
                x = value;
                RaisePropertyChanged(nameof(X));
            }
        }
        public int Y { get => y;
            set
            {
                if (value.Equals(y)) return;
                y = value;
                RaisePropertyChanged(nameof(Y));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void newPosition(int gridWidth, int gridHeight)
        {
            if (x + newX >= 0 && x + newX <= gridWidth - size)
            {
                x += newX;
            }
            else
            {
                if (newX > 0)
                {
                    x = gridWidth - size;
                }
                else
                {
                    x = 0;
                }

                newX *= -1;

            }

            if (y + newY >= 0 && y + newY <= gridHeight - size)
            {
                y += newY;
            }
            else
            {
                if (newY > 0)
                {
                    y = gridHeight - size;
                }
                else
                {
                    y = 0;
                }

                newY *= -1;
            }

        }
    }
}

