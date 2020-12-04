from multiprocessing import Pool
import time
import threading
import random
import numpy as np
import os
import copy

class Board:
    def __init__(self, width, height, beVerbose):
        self.width = width
        self.height = height
        self.beVerbose = beVerbose
        self.myBoard = np.zeros((width, height), dtype=bool)

    '''
    Function to init board
    '''
    def initBoard(self):
        for y in range(self.height):
            for x in range(self.width):
                rnd = random.randint(0, 1)
                self.myBoard[x][y] = True if rnd == 1 else False
              
    '''
    Function to update board
    '''  
    def updateBoard(self, tmp):
        tmpBoard = copy.deepcopy(self.myBoard)
        for y in range(self.height):
            for x in range(self.width):
                neighbors = self.countLiveNeighbors(x, y)
                currentCell = self.myBoard[x][y]

                if ((currentCell == True and (neighbors == 2 or neighbors == 3)) or (currentCell == False and neighbors == 3)):
                    tmpBoard[x][y] = True
                else:
                    tmpBoard[x][y] = False
        
        self.myBoard = copy.deepcopy(tmpBoard)

        if self.beVerbose:
            self.printBoard()

    '''
    Function to count live neighbors of cell
    '''
    def countLiveNeighbors(self, x, y):
        val = 0

        for j in range(-1, 2):
            if (y + j < 0 or y + j >= self.height):
                continue

            k = (y + j + self.height) % self.height

            for i in range(-1, 2):
                if (x + i < 0 or y + i >= self.width):
                    continue

                h = (x + i + self.width) % self.width
                val = val + (1 if self.myBoard[h][k] else 0)
        return val - (1 if self.myBoard[x][y] else 0) 

    '''
    Function to print board
    '''
    def printBoard(self):
        for y in range(self.height):
            strng = ""
            for x in range(self.width):
                if self.myBoard[x][y] == False: strng = strng + " "
                else: strng = strng + "X"
            print(strng)
        print("\n\n\n")


### Function to do serial game of life
def serialGOF(board, numOfGenerations):
    for i in range(numOfGenerations):
        board.updateBoard(numOfGenerations)
        
### Function to do parallel game of life
def parallelGOF(board, numOfGenerations):
    pool = Pool()
    hm = pool.map(board.updateBoard, range(numOfGenerations))
    pool.close()
    pool.join()


if __name__ == "__main__":
    # Parameters
    width = 50
    height = 50
    numOfGenerations = 150
    beVerbose = False

    # Init serial and parallel board
    board = Board(width, height, beVerbose)
    board.initBoard()
    parallelBoard = Board(width, height, beVerbose)
    parallelBoard.myBoard = board.myBoard

    # Serial Game of Life
    s1 = time.time()
    serialGOF(board, numOfGenerations)
    e1 = time.time()
    print("\nSerial: {:10.5f}".format(e1 - s1))

    # Parallel Game of Life
    s2 = time.time()
    parallelGOF(board, numOfGenerations)
    e2 = time.time()
    print("\nParallel: {:10.5f}".format(e2 - s2))
    print("\n")
