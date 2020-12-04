using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GOF
{
    class Program
    {
        /// <summary>
        /// Serial Game of Life
        /// </summary>
        /// <param name="board"></param>
        /// <param name="numberOfGenerations"></param>
        static void serialGOF(Board board, int numberOfGenerations)
        {
            Console.WriteLine("Serial running...");
            for (int counter = 0; counter < numberOfGenerations; counter++)
            {
                board.updateBoard();
            }
        }

        /// <summary>
        /// Parallel Game of Life
        /// </summary>
        /// <param name="board"></param>
        /// <param name="numberOfGenerations"></param>
        static void parallelGOF(Board board, int numberOfGenerations)
        {
            Console.WriteLine("Parallel running...");
            Parallel.For(0, numberOfGenerations, (i, state) =>
            {
                board.updateBoard();
            });
        }

        static void Main(string[] args)
        {
            int width = 75;
            int height = 75;

            //Want to print progress and result?
            bool beVerbose = false;

            //Initialize board and parallel board with same board!
            Board board = new Board(height, width, beVerbose);
            board.initBoard();

            Board parallelBoard = new Board(height, width, beVerbose);
            parallelBoard.myBoard = board.myBoard;

            //Set number of generations to run
            int numOfGenerations = 1000;

            //Serial Game of Life
            var s1 = Stopwatch.StartNew();
            serialGOF(board, numOfGenerations);
            s1.Stop();
            Console.WriteLine("Serial: " + ((double)(s1.Elapsed.TotalSeconds)).ToString("0.00000 s") + "\n");

            //Parallel Game of Life
            var s2 = Stopwatch.StartNew();
            parallelGOF(parallelBoard, numOfGenerations);
            s2.Stop();
            Console.WriteLine("Parallel: " + ((double)(s2.Elapsed.TotalSeconds)).ToString("0.00000 s"));


        }
    }
}
