﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev
{
    class Board
    {
        Random rnd = new Random();
        int boardSize;
        int[,] board;
        char[,] revealedBoard;
        private void EmptyBoard()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    board[i, j] = 0;
                    revealedBoard[i, j] = '#';
                }
            }
        }
        private void AddMines(int mineChance)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    int rndMineCreate = rnd.Next(0, mineChance);
                    if (rndMineCreate == 0)
                    {
                        board[i, j] = -1;
                        IncrementBoardVal(i - 1, j - 1);
                        IncrementBoardVal(i - 1, j + 1);
                        IncrementBoardVal(i - 1, j);
                        IncrementBoardVal(i + 1, j - 1);
                        IncrementBoardVal(i + 1, j + 1);
                        IncrementBoardVal(i + 1, j);
                        IncrementBoardVal(i, j - 1);
                        IncrementBoardVal(i, j + 1);
                    }
                }
            }
        }
        private void IncrementBoardVal(int i, int j)
        {
            if ((i < boardSize) && (j < boardSize) 
                && (i >= 0) && (j >=0)
                && (board[i, j] != -1))
            {
                board[i, j]++;
            }
        }
        public void CreateBoard(int size)
        {
            boardSize = size;
            board = new int[boardSize, boardSize];
            revealedBoard = new char[boardSize, boardSize];
            EmptyBoard();
            AddMines(5);
        }
        public bool RevealPos(int x, int y)
        {
            bool mine = false;
            if ((x >= 0) && (y >= 0)
                    && (x < boardSize) && (y < boardSize)
                    && (revealedBoard[x, y] == '#'))
            {
                char val = Convert.ToString(board[x, y])[0];
                if (val == '-')
                    val = 'X';
                revealedBoard[x, y] = val;
                if (val == '0')
                {
                    RevealPos(x - 1, y - 1);
                    RevealPos(x - 1, y + 1);
                    RevealPos(x - 1, y);
                    RevealPos(x + 1, y - 1);
                    RevealPos(x + 1, y + 1);
                    RevealPos(x + 1, y);
                    RevealPos(x, y - 1);
                    RevealPos(x, y + 1);
                }
                if (val == 'X')
                    mine = true;
            }
            return mine;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    char val = Convert.ToString(board[i, j])[0];
                    if (val == '-')
                        val = 'X';
                    Console.Write("[" + val + "]");
                }
                Console.WriteLine();
            }
        }
        public void PrintRevealed()
        {
            for (int i = 0; i < boardSize; i++)
            {
                if (i == 0)
                {
                    for (int header_j=0; header_j < boardSize; header_j++)
                    {
                        Console.Write("-" + header_j + "-");
                    }
                    Console.WriteLine();
                }
                for (int j = 0; j < boardSize; j++)
                {
                    Console.Write("[" + revealedBoard[i, j] + "]");
                }
                Console.WriteLine("-" + i + "-");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Board b = new Board();
            b.CreateBoard(10);
            b.PrintBoard();
            while (true)
            {
                b.PrintRevealed();
                Console.WriteLine("Enter y to reveal: ");
                int y = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter x to reveal: ");
                int x = Convert.ToInt32(Console.ReadLine());
                bool mine = b.RevealPos(x, y);
                if (mine)
                {
                    b.PrintBoard();
                    Console.WriteLine("You chose a mine!");
                    Console.WriteLine("Would you like to play again? (y, n)");
                    char resp = Console.ReadLine()[0];
                    if (resp == 'y')
                    {
                        b.CreateBoard(10);
                    }
                    else
                        break;
                }
            }
            Console.Read();
        }
    }
}

