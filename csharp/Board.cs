using System;
using System.Collections.Generic;
using System.Text;

namespace GOF
{
    class Board
    {
        public int width { get; set; }
        public int height { get; set; }
        public bool[,] myBoard { get; set; }
        public bool beVerbose { get; set; }

        public Board(int width, int height, bool beVerbose)
        {
            this.width = width;
            this.height = height;
            this.beVerbose = beVerbose;
            this.myBoard = new bool[width, height];
        }

        /// <summary>
        /// Function to generate board with fixed height and width + random position of cells
        /// </summary>
        public void initBoard()
        {
            var random = new Random();

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    myBoard[x, y] = random.Next(2) == 0;
                }
            }
        }

        /// <summary>
        /// Function to update board
        /// </summary>
        public void updateBoard()
        {
            // temp board to hold state
            bool[,] newBoard = new bool[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int neighbors = countLiveNeighbors(x, y);
                    bool currentCell = myBoard[x, y];

                    // - wikipedia popis
                    // Any live cell with two or three live neighbours survives.
                    // Any dead cell with three live neighbours becomes a live cell.
                    // All other live cells die in the next generation.Similarly, all other dead cells stay dead.
                    if (currentCell && (neighbors == 2 || neighbors == 3) || !currentCell && neighbors == 3) newBoard[x, y] = true;
                    else newBoard[x, y] = false;
                }
            }

            myBoard = newBoard;

            //Print if verbose
            if (beVerbose)
            {
                Printer.printBoard(myBoard, width, height, true);
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Function to count neighbors according to cell position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int countLiveNeighbors(int x, int y)
        {
            int value = 0;

            for (var j = -1; j <= 1; j++)
            {
                // if y+j is off the board
                if (y + j < 0 || y + j >= height)
                {
                    continue;
                }

                // if y+j is off the board.
                int k = (y + j + height) % height;

                for (var i = -1; i <= 1; i++)
                {
                    // if x+i is off the board
                    if (x + i < 0 || x + i >= width)
                    {
                        continue;
                    }

                    // if x+i is off the board.
                    int h = (x + i + width) % width;

                    // is cell at (h,k) alive???
                    value += myBoard[h, k] ? 1 : 0;
                }
            }

            // Subtract 1 if (x,y) is alive since we counted it as a neighbor.
            return value - (myBoard[x, y] ? 1 : 0);
        }
    }
}
