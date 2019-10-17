using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Birb
{
    class Leaderboard
    {
        int pointer;
        int score;
        string name;


        public Leaderboard(int _score, string _name)
        {
            score = _score;
            name = _name;
        }
        //constructor

        public Leaderboard(int _score, string _name, int _pointer)
        {
            score = _score;
            name = _name;
            pointer = _pointer;
        }
        //constructor for pre-sorted values

        public int returnPointer()
        {
            return pointer;
        }
        //returns the pointer attribute

        public void changePointer(int newValue)
        {
            pointer = newValue;
        }
        //updates the pointer attribute

        public int returnScore()
        {
            return score;
        }
        //returns the score attribute

        public string returnName()
        {
            return name;
        }
        //returns the name attribute
    }
}
