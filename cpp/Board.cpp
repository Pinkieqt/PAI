#include "Board.h"
#include <time.h>
#include<windows.h>

Board::Board(int h, int w, bool bv)
{
	height = h;
	width = w;
	beVerbose = bv;

	//Initialize two dimensional array
	myBoard = new bool* [h];
	for (int i = 0; i < h; i++)
	{
		myBoard[i] = new bool[h];
		memset(myBoard[i], false, h * sizeof(bool));
	}
}

void Board::initBoard()
{
	srand(time(NULL));
	for (int y = 0; y < height; y++) {
		for (int x = 0; x < width; x++) {
			int rnd = rand() % 2 + 0;
			bool val = true ? rnd == 1 : false;
			myBoard[x][y] = val;
		}
	}
}

void Board::updateBoard()
{
	bool** tmpBoard = new bool* [height];
	for (int i = 0; i < height; i++)
	{
		tmpBoard[i] = new bool[height];
		memset(tmpBoard[i], false, height * sizeof(bool));
	}

	for (int y = 0; y < height; y++) {
		for (int x = 0; x < width; x++) {
			int neighbors = countLiveNeighbors(x, y);
			bool currentCell = myBoard[x][y];

			if (currentCell == true && (neighbors == 2 || neighbors == 3) || currentCell == false && neighbors == 3) tmpBoard[x][y] = true;
			else tmpBoard[x][y] = false;
		}
	}

	myBoard = tmpBoard;

	if (beVerbose) {
		printBoard(true);
	}
}

int Board::countLiveNeighbors(int x, int y)
{
	int value = 0;

	for (int j = -1; j <= 1; j++)
	{
		// if y+j is off the board
		if (y + j < 0 || y + j >= height)
		{
			continue;
		}

		// if y+j is off the board.
		int k = (y + j + height) % height;

		for (int i = -1; i <= 1; i++)
		{
			// if x+i is off the board
			if (x + i < 0 || x + i >= width)
			{
				continue;
			}

			// if x+i is off the board.
			int h = (x + i + width) % width;

			// is cell at (h,k) alive???
			value += myBoard[h][k] ? 1 : 0;
		}
	}

	// Subtract 1 if (x,y) is alive since we counted it as a neighbor.
	return value - (myBoard[x][y] ? 1 : 0);
}

void Board::printBoard(bool replace)
{
	if (replace) {
		HANDLE console = GetStdHandle(STD_OUTPUT_HANDLE);
		COORD coord;
		coord.X = coord.Y = 0;
		SetConsoleCursorPosition(console, coord);
	}


	for (int i = 0; i < width; i++) {
		for (int j = 0; j < height; j++) {
			if (myBoard[i][j] == false) {
				cout << " ";
			}
			else {
				cout << "X";
			}
		}
		cout << "\n";
	}

}
