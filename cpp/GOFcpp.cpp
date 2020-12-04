#include <iostream>
#include "Board.h"
#include <omp.h>
#include <chrono>

#define THREAD_NUM 4

Board serialGOF(Board board, int numOfGenerations) {
    for (int i = 0; i < numOfGenerations; i++) {
        board.updateBoard();
    }
    return board;
}

Board parallelGOF(Board board, int numOfGenerations) {
    
    #pragma omp parallel num_threads(4)
    {
        #pragma omp for
        for (int i = 0; i < numOfGenerations; i++) {
            board.updateBoard();
        }
    }

    return board;
}


/// <summary>
/// Main
/// </summary>
/// <returns></returns>
int main()
{
    int width = 100;
    int height = 100;
    int numberOfGenerations = 1000;

    //Want to print progress and result?
    bool beVerbose = false;

    Board board = Board(width, height, beVerbose);
    board.initBoard();

    Board parallelBoard = Board(width, height, beVerbose);
    parallelBoard.myBoard = board.myBoard;

    //serial
    auto start = chrono::high_resolution_clock::now();
    board = serialGOF(board, numberOfGenerations);
    auto finish = chrono::high_resolution_clock::now();
    chrono::duration<double> elapsed = finish - start;
    cout << "Serial: " << elapsed.count() << " s\n";


    //parallel
    start = chrono::high_resolution_clock::now();
    parallelBoard = parallelGOF(parallelBoard, numberOfGenerations);
    finish = chrono::high_resolution_clock::now();
    elapsed = finish - start;
    cout << "Parallel: " << elapsed.count() << " s\n";

}