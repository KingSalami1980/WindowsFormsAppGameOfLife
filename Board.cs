using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsAppGameOfLife
{
    class Board
    {
        private Square[] squares;
        private int iCount = 16;
        private int iSquareSize = 15;
        private int iTopMargin = 40;

        public Board(int count, int squareSize, int topMargin)
        {
            iCount = count;
            iSquareSize = squareSize;
            iTopMargin = topMargin;
            squares = new Square[iCount * iCount];
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < (iCount * iCount); i++)
            {
                int x = (i % iCount) * (iSquareSize + 1);
                int y = (i / iCount) * (iSquareSize + 1) + iTopMargin;
                squares[i] = new Square(new System.Drawing.Point(x, y), iSquareSize);
            }
        }

        public Square Square(int i)
        {
            if (i < (iCount * iCount) && i > -1)
            {
                return squares[i];
            }
            return null;
        }

        public void Run(object data)
        {
            int s = (int)data;
            while (s > 0)
            {
                UpdateSquareState();
                UpdateSquareDisplay();
                s--;
                Thread.Sleep(100);
            }
        }

        private void UpdateSquareState()
        {
            for (int i = 0; i < squares.Length; i++)
            {
                int neighbours = GetNeighbourCount(i);
                if (neighbours > 3 || neighbours < 2)
                {
                    squares[i].SetState(false);
                }
                else if (neighbours == 3)
                {
                    squares[i].SetState(true);
                }
                else
                {
                    squares[i].SetState();
                }
            }
        }

        private void UpdateSquareDisplay()
        {
            foreach (Square s in squares)
            {
                s.UpdateDisplay();
            }
        }

        private int GetNeighbourCount(int squareIndex)
        {
            int[] neighbours = GetNeighbourIndexes(squareIndex);
            int neighbourCount = 0;
            foreach (int i in neighbours)
            {
                if (i >= 0 && i < squares.Length && squares[i].IsAlive() && i != squareIndex && Math.Abs((i % iCount) - (squareIndex % iCount)) <= 1)
                {
                    neighbourCount++;
                }
            }

            return neighbourCount;
        }

        private int[] GetNeighbourIndexes(int si)
        {
            int[] index = new int[9];

            index[0] = si - iCount - 1;
            index[1] = si - iCount;
            index[2] = si - iCount + 1;

            index[3] = si - 1;
            index[4] = si;
            index[5] = si + 1;

            index[6] = si + iCount - 1;
            index[7] = si + iCount;
            index[8] = si + iCount + 1;

            return index;
        }
    }
}
