using System;
using System.Collections.Generic;
using System.Text;

namespace TripleTriad
{
    public class Board
    {
        private Card[,] cells;

        public Board()
        {
            cells = new Card[3, 3];
        }

        public static Board create()
        {
            return new Board();
        }

        // Method to get the state of a specific cell
        public Card GetCellState(int row, int col)
        {
            return cells[row, col];
        }

        // Method to set the state of a specific cell
        public void SetCellState(int row, int col, Card state)
        {
            cells[row, col] = state;
        }

        public bool CheckFull()
        {
            //int flag = 0;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (cells[i,j] == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
