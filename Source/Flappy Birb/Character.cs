using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Flappy_Birb
{
    class Character
    {
        public Point size = new Point(30, 30);
        public Point coords;
        public int radius = 15;
        int terminalVelocity = 30;
        public int flapVelocity = -9;
        public int birdVelocity;
        private int playerScore = 0;


        public Character(int xCoord, int yCoord)
        {
            coords = new Point(xCoord, yCoord);
            //takes and sets the coordinates of the player

        }
        //constructor

        public void flap()
        {
            movementMath(flapVelocity);
        }
        //causes the bird to flap

        public void movementMath(int vel)
        {
            birdVelocity = vel;
            //calculates the velocity of the bird
        }
        //sets the velocity of the bird after a flap

        public void applyGravity(int g)
        {
            birdVelocity += g;
            //calculates the velocity of the bird

            if (Math.Abs(birdVelocity) > terminalVelocity)
            {
                if (birdVelocity < 0)
                {
                    birdVelocity = -terminalVelocity;
                }
                else
                {
                    birdVelocity = terminalVelocity;
                }
            }
            //makes sure that the bird cant move faster then terminal velocity
        }
        //make it so that the bird falls constantly

        public void move()
        { 
            coords.Y += birdVelocity;
        }
        //applies the velocity to the bird

        public void terrainCollision(int height)
        {
            if (coords.Y >= height - size.Y)
                coords.Y = height - size.Y;
            //stops the bird from going below the screen

            if (coords.Y <= 0)
                coords.Y = 0;
            //stops the bird from going above the ceiling
        }
        //does the collision maths for the ceiling and floor

        public void addScore(int score)
        {
            playerScore += score;
        }
        //adds the score from passing through a pipe onto the player's score

        public int checkScore()
        {
            return playerScore;
        }
        //returns the players score
    }
}
