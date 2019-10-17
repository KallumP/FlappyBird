using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace Flappy_Birb {
    public partial class Form1 : Form {
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

        //game loop
        private void timer1_Tick(object sender, EventArgs e) {

            //stops the game from starting right after hitting restart
            if ( clickedToStart == true ) {

                playerOne.applyGravity(gravity);
                playerOne.move();
                playerOne.terrainCollision(gameHeight);
                checkSpawnPipe();

                foreach ( Pipe p in pipes )
                    p.movePipe(gameSpeed);

                removePipes();

                //causes the game screen to refresh
                pictureBox1.Invalidate();

                //after everything is done, it will check for a collision
                pipeCollsion();

                //updates the text boxes in game
                int frameRate = 1000 / gameLoop.Interval;
                showScore_txt.Text = Convert.ToString(playerOne.checkScore());
                fps_txt.Text = Convert.ToString(frameRate);
            }
        }

        //Constructor
        public Form1() {
            InitializeComponent();
        }

        //loads the form
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


        //an event for when the user presses a key
        private void Form1_KeyDown(object sender, KeyEventArgs e) {

            //checks for the jump inputs
            if ( e.KeyCode == Keys.Space || e.KeyCode == Keys.Up ) {

                //checks to see if the user is clicking to start the game
                if ( clickedToStart == false ) {
                    clickToStart_lbl.Visible = false;
                    clickedToStart = true;

                }

                //causes the player to flap, as long as the game has started
                playerOne.flap();
            }

            //takes in an input to close the program
            else if ( e.KeyCode == Keys.Escape )
                Close();
        }

        //draws the game
        private void pictureBox1_Paint(object sender, PaintEventArgs e) {

            //draws out the character 
            e.Graphics.DrawEllipse(Pens.Blue, playerOne.coords.X, playerOne.coords.Y, playerOne.size.X, playerOne.size.Y);
            // e.Graphics.DrawImage(Image.FromFile("Bird.png"), playerOne.coords.X, playerOne.coords.Y, playerOne.size.X, playerOne.size.Y);

            //draws out a pipe for each pipe in the list of pipes
            foreach ( Pipe p in pipes ) {
                e.Graphics.DrawRectangle(Pens.Red, p.coords.X, 0, pipeWidth, p.coords.Y);
                e.Graphics.DrawRectangle(Pens.Red, p.coords.X, p.coords.Y + pipeGap, pipeWidth, gameHeight);

                //e.Graphics.DrawImage(Image.FromFile("PipeBase.png"), p.coords.X, 0, pipeWidth, p.coords.Y);
                //e.Graphics.DrawImage(Image.FromFile("PipeBase.png"), p.coords.X, p.coords.Y + pipeGap, pipeWidth, gameHeight);
            }
        }

        //checks to see if a pipe needs to be spawned
        public void checkSpawnPipe() {

            //checks to see if enough ticks have passed to spawn a new pipe
            if ( ticksToSpawnPipe > spawnRate ) {

                //spawns a new pipe
                spawnPipe();

                //resets the time since last spawn
                ticksToSpawnPipe = 0;
            }

            //incremenats the tick counter
            ticksToSpawnPipe++;
        }

        //spawns in a pipe
        public void spawnPipe() {

            //calculates the random Y coordinate of the pipe
            int yOfPipe = rnd.Next(spawnMargin, gameHeight - pipeGap - spawnMargin);

            //spawns in the new pipe
            pipes.Add(new Pipe(gameWidth, yOfPipe));
        }

        //checks to see if there is a collsion with the pipes
        public void pipeCollsion() {

            //loops through all of the pipes in the game
            foreach ( Pipe p in pipes ) {

                //checks to see if the player is in the pipe's width
                if ( playerOne.coords.X + playerOne.size.X > p.coords.X && playerOne.coords.X < p.coords.X + pipeWidth ) {

                    //checks to see if the player was outside the safe gap of the pipes
                    if ( playerOne.coords.Y < p.coords.Y || playerOne.coords.Y + playerOne.size.Y > p.coords.Y + pipeGap ) {

                        //if there was a collision, then it game overs
                        gameOver();

                    } else {

                        //checks to see if the score was already taken from this pipe
                        if ( p.checkPassed() == false ) {

                            //incremeants the user's score
                            playerOne.addScore(p.returnScore());

                            //makes the game faster
                            makeHarder();
                        }
                    }
                }
            }
        }

        //does the game over stuff
        public void gameOver() {

            //ends the game if the player hits a pipe
            gameLoop.Stop();

            //stops showing the in game display
            fps_lbl.Visible = false;
            fps_txt.Visible = false;
            showScore_txt.Visible = false;

            //sets this to false, to stop the game starting immediately when restarting
            clickedToStart = false;

            //shows the game over part of the game
            restart_btn.Visible = true; restart_btn.Enabled = true;
            score_txt.Visible = true;
            gameOver_lbl.Visible = true;
            yourScore_lbl.Visible = true;

            //shows the other elements of the game
            addToLead_btn.Enabled = true;
            leaderBoard_lBox.Enabled = true;
            name_tBox.Enabled = true;

            //shows the score of the game
            score_txt.Text = Convert.ToString(playerOne.checkScore());
        }

        //starts the game up
        public void gameStart() {

            //removes all elements from the pipes list
            pipes.Clear();

            //sets the height and width of the picture box
            gameHeight = pictureBox1.Height;
            gameWidth = pictureBox1.Width;

            //sets the players coordinates back to the start
            playerOne.coords.Y = gameHeight / 2;
            playerOne.coords.X = gameWidth / 10;

            //adds a pipe as soon as the game starts
            spawnPipe();

            //resets the score to zero
            playerOne.addScore(-playerOne.checkScore());

            //removes the game over elements
            restart_btn.Visible = false; restart_btn.Enabled = false;
            score_txt.Visible = false;
            gameOver_lbl.Visible = false;
            yourScore_lbl.Visible = false;

            //stops the user from interacting with the elements while playing
            leaderBoard_lBox.Enabled = false;
            addToLead_btn.Enabled = false;
            name_tBox.Enabled = false;

            //shows the in game display
            fps_lbl.Visible = true;
            fps_txt.Visible = true;
            showScore_txt.Visible = true;
            clickToStart_lbl.Visible = true;

            //sets the game speed to the start
            gameSpeed = baseGameSpeed;

            //resets the difficulty counter
            passesToSpeedUp = 0;

            //resets the spawn counter
            ticksToSpawnPipe = 0;

            //resets the spawnrate
            spawnRate = 60;

            //resets how far from the top and bottom the gaps spawn
            spawnMargin = 30;

            //restarts the game loop
            gameLoop.Start();
        }

        //checks to see if the pipes are off screen and then removes them
        public void removePipes() {

            //if there are any pipes
            if ( pipes.Count < 0 ) {
                //creates a new instance of the pipe class identical to the first pipe in the list of pipes
                Pipe p = pipes[0];

                //checks to see if the pipe's X coordinate is smaller than 0
                if ( p.offScreen(pipeWidth) == true )

                    //removes the first pipe in the list
                    pipes.RemoveAt(0);
            }
        }

        //resets the game
        private void restart_btn_Click(object sender, EventArgs e) {

            //starts the game
            gameStart();

            //draws out the game
            pictureBox1.Invalidate();
        }

        //adds the current score to the leadboard class
        private void addToLead_btn_Click(object sender, EventArgs e) {

            //gets the name entered
            enteredName = name_tBox.Text;

            //checks to see if there was an input
            if ( enteredName == "" )

                //sets the default name
                enteredName = "No Name";

            //adds the score
            addScore(playerOne.checkScore());
            sortScore();
            outputScore();
            saveLeaderBoard();
        }

        //adds the score to the leaderboard class
        public void addScore(int newScore) {

            //adds the new number to the list
            board.Add(new Leaderboard(newScore, enteredName));
        }

        public void sortScore() {

            //gets the size of the list after adding the new one in
            int size = board.Count();

            //checks to see if anything was in the list already
            if ( size == 1 )

                //sets the pointer to 1 (no sorting needed, cus first item in the list)
                board[0].changePointer(-1);

            else {

                //variables used in the sorting algorithm
                int currentPointer = startPointer;
                int prevPointer = startPointer;
                int newIndex = size - 1;

                //variable used to keep track of when to exit the do loop
                bool exit = false;


                do {

                    //if the current value is smaller than the new value, then we need to compare the next value
                    if ( board[currentPointer].returnScore() > board[newIndex].returnScore() ) {

                        //if the new value is larger than the last value in the list
                        if ( board[currentPointer].returnPointer() == -1 ) {

                            //the last value in the list now points to the new value
                            board[currentPointer].changePointer(newIndex);

                            //sets the new index to be the last value in the list
                            board[newIndex].changePointer(-1);

                            //makes the sorting stop after putting the value at the end
                            exit = true;

                        } else {

                            //saves the previous pointer
                            prevPointer = currentPointer;

                            //updates the current pointer to point to the next value in the list.
                            currentPointer = board[currentPointer].returnPointer();
                        }

                        //if the current index's value is bigger than the new value then we need to put the new value here
                    } else if ( board[currentPointer].returnScore() <= board[newIndex].returnScore() ) {

                        //if the new value was smaller than the first value in the list
                        if ( currentPointer == startPointer ) {

                            //the starting pointer changes to the new value's index
                            startPointer = newIndex;

                            //the new value points to the value that it was smaller than
                            board[newIndex].changePointer(currentPointer);

                            //if the current value is larger or equal to the new value, the new value belongs before the current value
                        } else {

                            //previous value points to the new value
                            board[prevPointer].changePointer(newIndex);

                            //the new value points to where the value it was smaller than
                            board[newIndex].changePointer(currentPointer);
                        }

                        //makes the sorting end after sorting the value
                        exit = true;
                    }

                    //loops until the value has been sorted
                } while ( exit == false );
            }
        }
        //sorts the newly added score

        //outputs the score to the listbox
        public void outputScore() {

            //makes sure that there was a board to output
            if ( board.Count != 0 ) {

                //clears the list box
                leaderBoard_lBox.Items.Clear();

                //variable for exiting the while loop
                bool exitter = false;

                //makes a pointer variable to keep track of where in the leaderboard we are
                int sortPointer = startPointer;

                do {

                    //a temporary container to hold the current value
                    int value;

                    //gets the value from sortPointer's index the leaderboard
                    value = board[sortPointer].returnScore();


                    leaderBoard_lBox.Items.Add(value + "   " + board[sortPointer].returnName());

                    //sets the sortPointer to the next value (in order) in the leaderboard
                    sortPointer = board[sortPointer].returnPointer();

                    //if the pointer was set to -1 from the last line of code
                    if ( sortPointer == -1 )

                        // sets the while loop to exit
                        exitter = true;

                    //only loops while there is more values to output
                } while ( exitter == false );
            }
        }

        //saves the leader board to a text file
        public void saveLeaderBoard() {

            //opens the file to write to
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\Board.txt");

            //has the first line as the start pointer
            sw.WriteLine(startPointer);


            //loops through each score
            for ( int i = 0; i < board.Count(); i++ )

                //writes out the index, the score, the name, and then the pointer
                sw.WriteLine(i + "," + board[i].returnScore() + "," + board[i].returnName() + "," + board[i].returnPointer());

            //saves and closes the file
            sw.Close();
        }

        //loads the leader board from a text file into the program
        public void loadLeaderBoard() {

            //opens the file to read from 
            StreamReader sr = new StreamReader(Application.StartupPath + "\\Board.txt");

            //variables used in the load proccess
            string line;
            string[] newVariables;

            //gets the start pointer
            startPointer = Convert.ToInt16(sr.ReadLine());

            //loops until there is nothing left in the file
            while ( (line = sr.ReadLine()) != null ) {

                //splits each line from the text document and puts it into an array value
                newVariables = line.Split(new char[] { ',' });

                //adds a new object into the board list
                board.Add(new Leaderboard(Convert.ToInt16(newVariables[1]), newVariables[2], Convert.ToInt16(newVariables[3])));
            }
        }

        //makes the game harder
        public void makeHarder() {

            //if the game is set to speed up
            if ( passesToSpeedUp > difficultyIncreaseFrequency ) {

                //speed up the speed at which the pipes move
                gameSpeed += 1;

                //sets the spawn rate to scale properly
                spawnRate = distanceBetwenPipes / gameSpeed;

                //reset the counter for speeding up
                passesToSpeedUp = 0;
            }

            //makes the pipes spawn further and further away from the top and bottom of the screen which makes the game playable late game
            if ( spawnMargin < 150 )
                spawnMargin += 10;

            //incremeant the counter for speeding up
            passesToSpeedUp++;
        }
    }
}
