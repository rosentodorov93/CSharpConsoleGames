using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Board
    {
        private Symbol[,] board;

        public Board(int size)
        {
            this.board = new Symbol[size,size];
        }
        public Board()
            :this(3)
        {

        }
        public Symbol[,] BoardState => this.board;
        public bool IsFull()
        {
            for (int row = 0; row < this.board.GetLength(0); row++)
            {
                for (int col = 0; col < this.board.GetLength(1); col++)
                {
                    if (this.board[row,col] == Symbol.None)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public void PlaceSymbol(Index position, Symbol symbol)
        {
            if (position.Row < 0 && position.Col < 0
                && position.Row >= this.board.GetLength(0) && position.Col >= this.board.GetLength(1))
            {
                throw new IndexOutOfRangeException("Invalid index");
            }

            this.board[position.Row, position.Col] = symbol;
        }
        public List<Index> GetEmptyPositions()
        {
            var emptyPositions = new List<Index>();

            for (int row = 0; row < this.board.GetLength(0); row++)
            {
                for (int col = 0; col < this.board.GetLength(1); col++)
                {
                    if (this.board[row,col] == Symbol.None)
                    {
                        emptyPositions.Add(new Index(row, col));
                    }
                }
            }

            return emptyPositions;
        }
    }
}
