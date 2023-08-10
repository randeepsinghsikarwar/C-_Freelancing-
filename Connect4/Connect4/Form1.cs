using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{




    public partial class Connect4 : Form
    {

        // the board is a 7 x 6 grid of columns and rows
        // column 0 is at the left, column 6 is at the right
        // row 0 is at the top, row 5 is at the bottom
        int[,] board = new int[7, 6];

        // We also need to know whose turn it is (player 1 or player 2)
        int player = 1;

        // Brush Hint 1
        // We could set up an array of Brushes here, called brushColours
        // We want brushColour[0] to be Brushes.White
        // We want brushColour[1] to be Brushes.Yellow
        // We want brushColour[2] to be Brushes.Red

        Brush[] brushColor = new Brush[3] { Brushes.White, Brushes.Yellow, Brushes.Red };

        // We need to know who won -- to start, nobody
        int winner = 0;



        // Winner Hint 1
        // we could set up an array of 4 Points here that will hold the 4 winning
        // checkers if there is a winner.

        Point[] winningCheckers = new Point[4];

       



        public Connect4()
        {
            InitializeComponent();

            // set a form property that may or may not help with grpahical rendering issues
            this.DoubleBuffered = true;

            // Start with the winnerBox and the winnerLabel invisible.
            // We'll make them pop out when there is a winner.
            winnerBox.Visible = false;
            winnerLabel.Visible = false;

            for(int i = 0; i < winningCheckers.Length; i++)
            {
                winningCheckers[i] = new Point(-1, -1);
            }


        }

        private Brush pickBrushColour(int playerValue)
        {

            
            // This is a lovely if statement to determine which brush colour to return

            // Brush Hint 2
            
            // All the code below can be replaced by one line.
            // If you set your brushColour array up right (see Brush Hint 1)
            // then you can use the playerValue to index the brushColour array 
            // and you will get back the brush you need.

            // You'll be able to comment out or delete the rest of the code in this function.

            // Start by setting the brush Colour to White
            Brush brush = Brushes.White;

            // Then change it to Yellow or Red based on the playerValue.

            /* if (playerValue == 1)
             {
                 brush = Brushes.Yellow;
             }
             if (playerValue == 2)
             {
                 brush = Brushes.Red;
             }*/

            brush = brushColor[playerValue];
        
            // return the brush we've chosen.
            return brush;
        }

        private void drawBoard(Graphics g) {


            // We have a graphics context that represents the board, a 7 x 6 grid
            // We are going to use 100 x 100 pixel squares

            // For every column in the board
            for (int column = 0; column < 7; column++)
            {
                // for every row in each column
                for (int row = 0; row < 6; row++)
                {
                    // pick the right brush colour based on what the board
                    // representation says the checker colour is.
                    Brush brush = pickBrushColour(board[column, row]);

                    // draw the checker.
                    // we are going to draw the checker on Graphics object g
                    // we are going to find the top left of the box for this column and row
                    // and start 10 pixels after that in both cases
                    // We also pass in the brush colour that we chose above
                    drawChecker(g, column * 100 + 10, row * 100 + 10,brush);
                }
            }





            // Winner Hint 3
            // If there is a winner, we want to draw a smaller circle in the middle of the winning checkers.
            // So first, we only want to do this if there is a winner -- everything should be wrapped in an
            // if block.   Then, for every Point p in our list of winning Checkers, we want to draw a circle.
            // that sounds like a loop to me -- what kind is easiest to do something for every element of a collection?

            // If we have a Point p, we need to figure out the spot to draw the rectangle
            // This is the math:
            // Rectangle spot = new Rectangle(p.X * 100 + 40, p.Y * 100 + 40, 20, 20);
            // Then you can use fillElipse and give it a brush and the rectangle above to draw the spot.

         

                for (int i = 0; i < 4; i++)
                {
                    Rectangle spott = new Rectangle(winningCheckers[i].X * 100 + 40, winningCheckers[i].Y * 100 + 40, 20, 20);
                    g.FillEllipse(Brushes.Red, spott);
                }
            

        }

        private void drawChecker(Graphics g, int x, int y, Brush brush)
        {
            // We are going to pick a rectangle that starts at x,y on Graphics context g
            // we are going to draw an 80 x 80 rectangle (which is in fact a square)
            // We are going to put an ellipse in that rectangle (which is in fact a circle because the rectangle is a square)
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle spot = new Rectangle(x, y, 80, 80);
            g.FillEllipse(brush, spot);

            

        }


        // Switch the current player
        private void switchPlayer()
        {
            if (player == 1)
            {
                player = 2;
            } else
            {
                player = 1;
            }
        }

        // play a checker in a column

        private void playColumn(int column)
        {

            // If the top row is full, can't play here
            // Just stop and do nothing

            //EXTENSION: pop up message will appear if a particcular column is full. 

            if (board[column,0] > 0)
            {
                MessageBox.Show("The column is full");
                return;
            }

            // I'm going to start at the top and figure out if 
            // i could play a checker in this row.
            // If I *can*, then great, the checker will fall down 
            // to this row.
            // If I *can't*, then I'm not able to play in *this* row,
            // but since rowToPlay still is set to the row above me,
            // that is where I could play.

            int rowToPlay = 0;

            for (int row = 0; row < 6; row++)
            {
                if (board[column,row]==0)
                {
                    // If this spot is empty, then I could play here
                    rowToPlay = row;
                }
                    
            }

            // set the last playable row in this column to the current player

            board[column, rowToPlay] = player;

            // See if anyone has won!
            checkWinner();

            // switch player.
            switchPlayer();

            // We've changed whose turn it is and the representation of the board.
            // Better make them re-draw themselves

            boardBox.Invalidate(); // force re-draw
            playerBox.Invalidate();
        }

        private void checkHorizontalWinner()
        {
            // On every row
            for (int row = 0; row < 6; row++)
            {
                // Check the first 4 columns as starting points to win.
                for (int column = 0; column < 4; column++)
                {
                    // If this spot is empty, then we can't have a winner
                    if (board[column,row] == 0) { continue; }

                    int potentialWinner = board[column, row];
                    // Check the next 3 columns, if they all match, then we have a winner!
                    for (int winCheckColumn = 1; winCheckColumn <= 3; winCheckColumn++)
                    {
                        if (board[column+winCheckColumn,row] != potentialWinner)
                        {
                            potentialWinner = 0;
                        }
                    }

                    if (potentialWinner > 0)
                    {
                        winner = potentialWinner;
                        showWinner();
                        return;
                    }

                }
            }
        }

        private void checkVerticalWinner()
        {
            // On every column
            for (int column = 0; column < 7; column++)
            {
                // Check the first 3 rows as starting points
                for (int row = 0; row < 3; row++)
                {
                    // If this spot is empty, then we can't have a winner
                    if (board[column, row] == 0) { continue; }

                    int potentialWinner = board[column, row];
                    // Check the next 3 rows, if they all match, then we have a winner!
                    for (int winCheckRow = 1; winCheckRow <= 3; winCheckRow++)
                    {
                        if (board[column, row + winCheckRow] != potentialWinner)
                        {
                            potentialWinner = 0;
                        }
                    }

                    if (potentialWinner > 0)
                    {
                        winner = potentialWinner;
                        showWinner();
                        return;
                    }

                }
            }
        }
        private void checkDownRightWinner()
        {
            // On first 4 columns
            for (int column = 0; column < 4; column++)
            {
                // Check the first 3 rows as starting points
                for (int row = 0; row < 3; row++)
                {
                    // If this spot is empty, then we can't have a winner
                    if (board[column, row] == 0) { continue; }

                    int potentialWinner = board[column, row];
                    // Check the next 3 rows and columns, moving each one each time, if they all match, then we have a winner!
                    for (int winCheckDiag = 1; winCheckDiag <= 3; winCheckDiag++)
                    {
                        if (board[column+winCheckDiag, row + winCheckDiag] != potentialWinner)
                        {
                            potentialWinner = 0;
                        }
                    }

                    if (potentialWinner > 0)
                    {
                        winner = potentialWinner;
                        showWinner();
                        return;
                    }

                }
            }
        }
        private void checkDownLeftWinner()
        {
            // On last 4 columns
            for (int column = 3; column < 7; column++)
            {
                // Check the first 3 rows as starting points
                for (int row = 0; row < 3; row++)
                {
                    // If this spot is empty, then we can't have a winner
                    if (board[column, row] == 0) { continue; }

                    int potentialWinner = board[column, row];
                    // Check the next 3 rows and columns, moving each one each time, if they all match, then we have a winner!
                    for (int winCheckDiag = 1; winCheckDiag <= 3; winCheckDiag++)
                    {
                        if (board[column - winCheckDiag, row + winCheckDiag] != potentialWinner)
                        {
                            potentialWinner = 0;
                        }
                    }

                    if (potentialWinner > 0)
                    {
                        winner = potentialWinner;
                        showWinner();
                        return;
                    }

                }
            }
        }

        private void winChecker(int startColumn, int endColumn, int startRow, int endRow, int colMultiplier, int rowMultiplier)
        {
            // Starting at startColumn and going to endColumn
            for (int column = startColumn; column <= endColumn; column++)
            {
                // Starting at startRow and going to endRow
                for (int row = startRow; row <= endRow; row++)
                {
                    // If this spot is empty, then we can't have a winner
                    if (board[column, row] == 0) { continue; }

                    // We have a potential winner -- if we move 3 more steps in the appropriate direction and 
                    // we keep seeing the same player as the potential winner, then we have an actual winner!

                    int potentialWinner = board[column, row];

                    // Winner Hint 2a
                    // we might just have found the first checker of our winning combination.
                    // we want to set the 0-index of winningCheckers to a new Point object
                    // we use Point because it holds an x and a y value -- in this case, 
                    // we want to create a new Point(column,row) to store the column as the x and
                    // the y as the row.

                    winningCheckers[0] = new Point { X = row, Y = column };
/*                    Rectangle spot = new Rectangle(winningCheckers[0].X * 100 + 40, winningCheckers[0].Y * 100 + 40, 20, 20);
*/

                    // Check the next 3 rows and columns, moving each one each time, if they all match, then we have a winner!
                    for (int winCheck = 1; winCheck <= 3; winCheck++)
                    {
                        // Figure out which column and row to actually check 
                        int colCheck = column + colMultiplier * winCheck;
                        int rowCheck = row + rowMultiplier * winCheck;

                        // Winner Hint 2b
                        // We might have the winCheck-th checker of our winning combination.
                        // Set the winCheck index of winningCheckers to a new Point(colCheck,rowCheck);
                        winningCheckers[winCheck] = new Point { X = rowCheck, Y = colCheck };   
                        // See if that was a winner.
                        if (board[colCheck, rowCheck] != potentialWinner)
                        {
                            potentialWinner = 0;
                        }
                    }

                    if (potentialWinner > 0)
                    {
                        winner = potentialWinner;
                        showWinner();
                        return;
                    }

                }
            }
        }

        private void checkWinner()
        {
            //checkHorizontalWinner();
            //checkVerticalWinner();
            //checkDownRightWinner();
            //checkDownLeftWinner();

            
            winChecker(0, 3, 0, 5, 1, 0); // horizontal-- first 4 columns, all rows, move across columns
            winChecker(0, 6, 0, 2, 0, 1); // vertical -- all columns, first 3 rows, move down rows
            winChecker(0, 3, 0, 2, 1, 1); // right down diag-- first 4 columns, first 3 rows, move one column and one row
            winChecker(3, 6, 0, 2, -1, 1); // left down diag-- last 4 colums, first 3 rows, move one column left and one row down

            

        }

        // If there is a winner, show the winnerBox and the label, and re-draw the winner box.


     




        private void showWinner()
        {


            winnerBox.Visible = true;
            winnerLabel.Visible = true;
            winnerBox.Invalidate();
            winnerLabel.Text += "\n\n\n\n\n\nand points of winner checkers\n";
            for(int i = 0; i < 4; i++)
            {
                winnerLabel.Text += winningCheckers[i];
            }

            //EXTENSION: the buttons will stop working once the winner is decided. 

            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;





        }

       

        // To paint the boardBox, call drawBoard
        private void boardBox_Paint(object sender, PaintEventArgs e)
        {
            drawBoard(e.Graphics);
        }



        // To paint the playerbox, draw a checker in the brushcolour
        // of the current player.

        private void playerBox_Paint(object sender, PaintEventArgs e)
        {
            drawChecker(e.Graphics, 10, 10, pickBrushColour(player));
        }

        // To draw the winnerBox, draw a check in the brushcolour
        // of the winnning player
        private void winnerBox_Paint(object sender, PaintEventArgs e)
        {
            drawChecker(e.Graphics, 10, 10, pickBrushColour(winner));


        }


        // If any column button gets clicked, try to play a checker
        // in that column.

        private void button1_Click(object sender, EventArgs e)
        {
            playColumn(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            playColumn(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            playColumn(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            playColumn(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            playColumn(4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            playColumn(5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            playColumn(6);
        }

    }
}


/* 
 
    first task of assigning brush array is completed. 
    for the second task, a point arrray was declared and it will hold the points of the winning checkers. 
    unable to draw the rectangle in the center of the winning checkers. 
    
    extensions -
    notify player if a column is full. 
    buttons will stop working once the winner is declared. 
 */