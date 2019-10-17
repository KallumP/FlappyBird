using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Flappy_Birb
{
    class Pipe
    {
        public Point coords = new Point(0, 0);
        private int pipeScore;
        private bool passed = false;

        public Pipe(int xLocation, int yLocation)
        {
            coords.X = xLocation;
            coords.Y = yLocation;
            pipeScore = 1;
        }
        //constructor

        public void movePipe(int speed)
        {
            for (int i = 0; i< speed; i++)
            coords.X --;


            //coords.x -= speed;
        }
        //moves the pipe towards the player

        public bool checkPassed()
        {
            return passed;
        }
        //returns whether the pipe has been passed or not

        public int returnScore()
        {
            passed = true;
            return pipeScore;
        }
        //sets the passed value to true, and returns the score awarded for passing the pipe

        public bool offScreen(int width)
        {
            if ((coords.X + width) <= 0)
                return true;
            else
                return false;
        }
        //returns whether the pipe is off screen or not

        public int returnXLocation()
        {
            return coords.X;
        }
    }
}
