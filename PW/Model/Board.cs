using Logic;
using System;

namespace Model
{
    public class Board
    {
        private Ball ball = new Logic.Ball(10, 300, 300);

        public Ball Ball { get => ball; } 
    }
}
