using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tic_Tac_Toe
{
    public class Board
    {
        public const int RowSize = 3;

        public const int ColumnSize = 3;

        public string[] cells = new string[RowSize * ColumnSize];

        private int turns = 0;

        public string LastTurn = "", CurrentTurn = "X";

        public void MarkCell(int index)
        {
            cells[index] = CurrentTurn;
            SwitchTurn();
        }

        public void SwitchTurn()
        {
            LastTurn = CurrentTurn;
            if (CurrentTurn == "X")
                CurrentTurn = "O";
            else
                CurrentTurn = "X";
            turns++;
        }

        public void Clear()
        {
            for (int i = 0; i < 9; i++)
                cells[i] = "";

            turns = 0;
        }

        public bool IsGameOver()
        {
            return IsWin() || IsDraw();
        }

        public bool IsWin()
        {
            return IsRowWin() ||
            IsColumnWin() ||
            IsFirstDiagonalWin() ||
            IsSecondDiagonalWin();
        }

        public bool IsDraw()
        {
            return turns == 9;
        }

        private bool IsRowWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (RowWins(i))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsColumnWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (ColumnWins(i))
                {
                    return true;
                }
            }

            return false;
        }

        private bool RowWins(int row)
        {
            int startWith = row * RowSize;

            for (int i = startWith; i < startWith + RowSize; i++)
            {
                if (cells[i] != LastTurn)
                {
                    return false;
                }
            }
            return true;
        }

        private bool ColumnWins(int column)
        {
            int startWith = column * RowSize;

            for (int i = column; i < RowSize * ColumnSize; i += RowSize)
            {
                if (cells[i] != LastTurn)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsFirstDiagonalWin()
        {
            for (int i = 0; i < RowSize * ColumnSize; i += 4)
            {
                if (cells[i] != LastTurn)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsSecondDiagonalWin()
        {
            for (int i = 2; i < 7; i += 2)
            {
                if (cells[i] != LastTurn)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
