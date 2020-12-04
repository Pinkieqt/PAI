using System;
using System.Collections.Generic;
using System.Text;

namespace GOF
{
    static class Printer
    {
        public static void printBoard(bool[,] board, int width, int height, bool replace)
        {
            if (replace) Console.SetCursorPosition(0, 0);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (board[i, j] == false)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("X");
                    }
                }
                Console.WriteLine("");
            }

        }

    }
}
