﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


namespace Flappy_Birb {
    public partial class Form1 : Form {
        bool glControlLoaded = false;
        int gravity = 1;
        List<Pipe> pipes = new List<Pipe>();
        List<Leaderboard> board = new List<Leaderboard>();
        Character playerOne;
        Random rnd = new Random();
        bool clickedToStart = false;
        int startPointer = 0;
        string enteredName;
        int baseGameSpeed = 4;
        int spawnMargin = 30;
        int maxSpawnMargin;

        int gameWidth;
        int gameHeight;

        int passesToSpeedUp;
        int difficultyIncreaseFrequency = 4;

        int ticksToSpawnPipe;
        int spawnRate = 60;
        int gameSpeed;
        int pipeWidth = 50;
        int pipeGap = 160;
        int distanceBetwenPipes = 240;

        private void timer1_Tick(object sender, EventArgs e) {
            if (clickedToStart == true)
            //stops the game from starting right after hitting restart
            {
                playerOne.applyGravity(gravity);
                playerOne.move();
                playerOne.terrainCollision(gameHeight);
                checkSpawnPipe();

                foreach (Pipe p in pipes)
                    p.movePipe(gameSpeed);

                removePipes();

                pictureBox1.Invalidate();
                //causes the game screen to refresh

                GLControl_canvas.Invalidate();

                pipeCollsion();
                //after everything is done, it will check for a collision

                int frameRate = 1000 / gameLoop.Interval;
                showScore_txt.Text = Convert.ToString(playerOne.checkScore());
                fps_txt.Text = Convert.ToString(frameRate);
                //updates the text boxes in game
            }
        }
        //game loop

        public Form1() {
            InitializeComponent();
        }
        //dunno what this is

        private void Form1_Load(object sender, EventArgs e) {
            loadLeaderBoard();
            outputScore();


            maxSpawnMargin = (gameHeight - pipeGap) / 2;

            int startCoordx = gameWidth / 10;
            int startCoordy = gameHeight / 3;
            playerOne = new Character(startCoordx, startCoordy);
            //creates a new character with the correct starting coordinates

            gameStart();
        }
        //loads the form

        private void GLControl_canvas_Load(object sender, EventArgs e) {
            GL.ClearColor(Color.Black);
            glControlLoaded = true;
        }
        //loads up the gl control

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up) {
                if (clickedToStart == false) {
                    clickToStart_lbl.Visible = false;
                    clickedToStart = true;
                    //checks to see if the user is clicking to start the game
                }
                playerOne.flap();
                //causes the player to flap, as long as the game has started
            }
            //takes in a jump input

            else if (e.KeyCode == Keys.Escape) {
                Close();
            }
            //takes in an input to close the program
        }
        //detect when the user presses a key to jump

        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            e.Graphics.DrawEllipse(Pens.Blue, playerOne.coords.X, playerOne.coords.Y, playerOne.size.X, playerOne.size.Y);

            // e.Graphics.DrawImage(Image.FromFile("Bird.png"), playerOne.coords.X, playerOne.coords.Y, playerOne.size.X, playerOne.size.Y);
            //draws out the character 

            foreach (Pipe p in pipes) {
                e.Graphics.DrawRectangle(Pens.Red, p.coords.X, 0, pipeWidth, p.coords.Y);
                e.Graphics.DrawRectangle(Pens.Red, p.coords.X, p.coords.Y + pipeGap, pipeWidth, gameHeight);

                //e.Graphics.DrawImage(Image.FromFile("PipeBase.png"), p.coords.X, 0, pipeWidth, p.coords.Y);
                //e.Graphics.DrawImage(Image.FromFile("PipeBase.png"), p.coords.X, p.coords.Y + pipeGap, pipeWidth, gameHeight);

                //draws the not safe zone
            }
            //draws out a pipe for each pipe in the list of pipes
        }
        //draws the game

        private void GLControl_canvas_Paint(object sender, PaintEventArgs e) {

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            GL.Viewport(0, 0, GLControl_canvas.Width, GLControl_canvas.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, GLControl_canvas.Width, GLControl_canvas.Height, 0, -1, 1);
            GL.MatrixMode(MatrixMode.Modelview);

            //Basic Setup for viewing
          

            if (glControlLoaded != false) {
                //makes sure that the glcontrol is there before drawing to it

                GL.Begin(BeginMode.Polygon);

                //loops through and draws out a cirle to represent the player
                for (int i = 0; i <= 360; i += 20) {

                    double X = playerOne.radius + playerOne.coords.X + Math.Sin(i / 57.2) * playerOne.radius;
                    double Y = playerOne.radius + playerOne.coords.Y + Math.Cos(i / 57.2) * playerOne.radius;

                    GL.Color3(Color.Yellow);
                    GL.Vertex2(X, Y);
                }

                GL.End();

                foreach (Pipe p in pipes) {
                    GL.Begin(BeginMode.Polygon);
                    GL.Color3(Color.Green);
                    GL.Vertex2(p.coords.X, 0);
                    GL.Vertex2(p.coords.X + pipeWidth, 0);
                    GL.Vertex2(p.coords.X + pipeWidth, p.coords.Y);
                    GL.Vertex2(p.coords.X, p.coords.Y);
                    GL.End();

                    GL.Begin(BeginMode.Polygon);
                    GL.Vertex2(p.coords.X, p.coords.Y + pipeGap);
                    GL.Vertex2(p.coords.X + pipeWidth, p.coords.Y + pipeGap);
                    GL.Vertex2(p.coords.X + pipeWidth, GLControl_canvas.Height);
                    GL.Vertex2(p.coords.X, GLControl_canvas.Height);
                    GL.End();
                }

                GLControl_canvas.SwapBuffers();
            }
        }
        //draws the game using opengl

        public void checkSpawnPipe() {
            if (ticksToSpawnPipe > spawnRate) {
                spawnPipe();
                ticksToSpawnPipe = 0;
            }
            //only spawns something at the rate specified

            ticksToSpawnPipe++;
            //counter goes up to keep track of when to spawn a pipe
        }
        //checks to see if a pipe needs to be spawned

        public void spawnPipe() {
            int yOfPipe = rnd.Next(spawnMargin, gameHeight - pipeGap - spawnMargin);

            pipes.Add(new Pipe(gameWidth, yOfPipe));
            //calculates the random Y coordinate of the pipe and spawns it in
        }
        //spawns in a pipe

        public void pipeCollsion() {
            foreach (Pipe p in pipes)
            //loops through all of the pipes in the game
            {
                if (playerOne.coords.X + playerOne.size.X > p.coords.X &&
                playerOne.coords.X < p.coords.X + pipeWidth)
                //checks to see if the player is in the pipe's width
                {
                    if (playerOne.coords.Y < p.coords.Y ||
                    playerOne.coords.Y + playerOne.size.Y > p.coords.Y + pipeGap)
                    //checks to see if the player was outside the safe gap of the pipes
                    {
                        gameOver();
                        //if there was a collision, then it game overs
                    } else {
                        if (p.checkPassed() == false) {
                            playerOne.addScore(p.returnScore());
                            makeHarder();
                        }
                    }
                }
            }
        }
        //checks to see if there is a collsion with the pipes

        public void gameOver() {
            gameLoop.Stop();
            //ends the game if the player hits a pipe

            fps_lbl.Visible = false;
            fps_txt.Visible = false;
            showScore_txt.Visible = false;
            //stops showing the in game display

            clickedToStart = false;
            //sets this to false, to stop the game starting immediately when restarting

            restart_btn.Visible = true; restart_btn.Enabled = true;


            score_txt.Visible = true;
            gameOver_lbl.Visible = true;
            yourScore_lbl.Visible = true;
            //shows the game over part of the game

            addToLead_btn.Enabled = true;
            leaderBoard_lBox.Enabled = true;
            name_tBox.Enabled = true;
            //shows the other elements of the game

            score_txt.Text = Convert.ToString(playerOne.checkScore());
        }
        //does the game over stuff

        public void gameStart() {
            pipes.Clear();
            //removes all elements from the pipes list

            gameHeight = pictureBox1.Height;
            gameWidth = pictureBox1.Width;
            //sets the height and width of the picture box

            playerOne.coords.Y = gameHeight / 2;
            playerOne.coords.X = gameWidth / 10;
            //sets the players coordinates back to the start

            spawnPipe();
            //adds a pipe as soon as the game starts

            playerOne.addScore(-playerOne.checkScore());
            //resets the score to zero

            restart_btn.Visible = false; restart_btn.Enabled = false;
            score_txt.Visible = false;
            gameOver_lbl.Visible = false;
            yourScore_lbl.Visible = false;
            //removes the game over elements

            leaderBoard_lBox.Enabled = false;
            addToLead_btn.Enabled = false;
            name_tBox.Enabled = false;
            //stops the user from interacting with the elements while playing

            fps_lbl.Visible = true;
            fps_txt.Visible = true;
            showScore_txt.Visible = true;
            clickToStart_lbl.Visible = true;
            //shows the in game display

            gameSpeed = baseGameSpeed;
            //sets the game speed to the start

            passesToSpeedUp = 0;
            //resets the difficulty counter

            ticksToSpawnPipe = 0;
            //resets the spawn counter

            spawnRate = 60;
            //resets the spawnrate

            spawnMargin = 30;
            //resets how far from the top and bottom the gaps spawn

            gameLoop.Start();
            //restarts the game loop
        }
        //starts the game up

        public void removePipes() {
            if (pipes.Count < 0)
            //if there are any pipes
            {
                Pipe p = pipes[0];
                //creates a new instance of the pipe class identical to the first pipe in the list of pipes

                if (p.offScreen(pipeWidth) == true)
                //checks to see if the pipe's X coordinate is smaller than 0
                {
                    pipes.RemoveAt(0);
                    //removes the first pipe in the list
                }
            }

        }
        //checks to see if the pipes are off screen and then removes them

        private void restart_btn_Click(object sender, EventArgs e) {
            gameStart();
            pictureBox1.Invalidate();
            GLControl_canvas.Invalidate();
        }
        //resets the game

        private void addToLead_btn_Click(object sender, EventArgs e) {
            enteredName = name_tBox.Text;
            if (enteredName == "")
                enteredName = "No Name";
            //gets the name entered by the user

            addScore(playerOne.checkScore());
            sortScore();
            outputScore();
            saveLeaderBoard();
        }
        //adds the current score to the leadboard class

        public void addScore(int newScore) {
            board.Add(new Leaderboard(newScore, enteredName));
            //adds the new number to the list
        }
        //adds the score to the leaderboard class

        public void sortScore() {
            int size = board.Count();
            //gets the size of the list after adding the new one in

            if (size == 1) {
                board[0].changePointer(-1);
                //if there wasn't anything in the list already, then the pointer is set to -1
                //means that the value being added is the first in the list, so no sorting is needed
            } else {
                int currentPointer = startPointer;
                int prevPointer = startPointer;
                int newIndex = size - 1;
                //variables used in the sorting algorithm

                bool exit = false;
                do {
                    if (board[currentPointer].returnScore() > board[newIndex].returnScore())
                    //if the current value is smaller than the new value, then we need to compare the next value
                    {
                        if (board[currentPointer].returnPointer() == -1)
                        //if the new value is larger than the last value in the list
                        {
                            board[currentPointer].changePointer(newIndex);
                            //the last value in the list now points to the new value

                            board[newIndex].changePointer(-1);
                            //sets the new index to be the last value in the list

                            exit = true;
                            //makes the sorting stop after putting the value at the end
                        } else {
                            prevPointer = currentPointer;
                            //saves the previous pointer

                            currentPointer = board[currentPointer].returnPointer();
                            //updates the current pointer to point to the next value in the list.
                        }
                    } else if (board[currentPointer].returnScore() <= board[newIndex].returnScore())
                      //if the current index's value is bigger than the new value then we need to put the new value here
                      {
                        if (currentPointer == startPointer)
                        //if the new value was smaller than the first value in the list
                        {
                            startPointer = newIndex;
                            //the starting pointer changes to the new value's index

                            board[newIndex].changePointer(currentPointer);
                            //the new value points to the value that it was smaller than
                        } else
                          //if the current value is larger or equal to the new value, the new value belongs before the current value
                          {
                            board[prevPointer].changePointer(newIndex);
                            //previous value points to the new value

                            board[newIndex].changePointer(currentPointer);
                            //the new value points to where the value it was smaller than
                        }
                        exit = true;
                        //makes the sorting end after sorting the value
                    }
                }
                while (exit == false);
                //loops until the value has been sorted
            }
        }
        //sorts the newly added score

        public void outputScore() {
            leaderBoard_lBox.Items.Clear();

            bool exitter = false;
            //variable for exiting the while loop

            int sortPointer = startPointer;
            //makes a pointer variable to keep track of where in the leaderboard we are

            do {
                int value;
                //a temporary container to hold the current value

                value = board[sortPointer].returnScore();
                //gets the value from sortPointer's index the leaderboard

                leaderBoard_lBox.Items.Add(value + "   " + board[sortPointer].returnName());

                sortPointer = board[sortPointer].returnPointer();
                //sets the sortPointer to the next value (in order) in the leaderboard

                if (sortPointer == -1)
                //if the pointer was set to -1 from the last line of code
                {
                    exitter = true;
                    // sets the while loop to exit
                }
            }
            while (exitter == false);
            //only loops while there is more values to output
        }
        //outputs the score to the listbox

        public void saveLeaderBoard() {
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\Board.txt");

            sw.WriteLine(startPointer);
            //has the first line as the start pointer

            for (int i = 0; i < board.Count(); i++) {
                sw.WriteLine(i + "," + board[i].returnScore() + "," + board[i].returnName() + "," + board[i].returnPointer());
                //writes out the index, the score, the name, and then the pointer
            }

            sw.Close();
            //saves and closes the file
        }
        //saves the leader board to a text file

        public void loadLeaderBoard() {
            StreamReader sr = new StreamReader(Application.StartupPath + "\\Board.txt");
            string line;
            string[] newVariables = new string[3];

            startPointer = Convert.ToInt16(sr.ReadLine());
            //gets the start pointer

            while ((line = sr.ReadLine()) != null) {
                newVariables = line.Split(new char[] { ',' });
                //splits each line from the text document and puts it into an array value

                board.Add(new Leaderboard(Convert.ToInt16(newVariables[1]), newVariables[2], Convert.ToInt16(newVariables[3])));
                //adds a new object into the board list
            }

            //outputScore();
            //updates the leaderboard after the loading is done
        }
        //loads the leader board from a text file into the program

        public void makeHarder() {
            if (passesToSpeedUp > difficultyIncreaseFrequency)
            //if the game is set to speed up
            {
                gameSpeed += 1;
                //speed up the speed at which the pipes move

                spawnRate = distanceBetwenPipes / gameSpeed;
                //sets the spawn rate to scale properly

                passesToSpeedUp = 0;
                //reset the counter for speeding up
            }

            if (spawnMargin < 150)
                spawnMargin += 10;
            //makes the pipes spawn further and further away from the top and bottom of the screen
            //makes the game playable late game

            passesToSpeedUp++;
            //incremeant the counter for speeding up
        }

        //makes the game harder
    }
}
