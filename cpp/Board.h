#pragma once
#include <iostream>
#include <vector>
#include <stdlib.h>     /* srand, rand */
using namespace std;

class Board
{
public:
	int width;
	int height;
	bool** myBoard;
	bool beVerbose;

	//functions
	Board(int height, int width, bool beVerbose);
	void initBoard();
	void updateBoard();
	int countLiveNeighbors(int x, int y);
	void printBoard(bool replace);

};

