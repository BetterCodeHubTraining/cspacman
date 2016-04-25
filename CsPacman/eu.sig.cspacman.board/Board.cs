using System.Diagnostics;

namespace eu.sig.cspacman.board
{
    public class Board : IBoard
    {

        private readonly Square[,] board;

        public int Width
        {
            get { return board.GetLength(0); }
        }

        public int Height
        {
            get { return board.GetLength(1); }
        }

        public Board(Square[,] grid)
        {
            Debug.Assert(grid != null);
            this.board = grid;
            Debug.Assert(Invariant(), "Initial grid cannot contain null squares");
        }

        public bool Invariant()
        {
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y] == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Square SquareAt(int x, int y)
        {
            Debug.Assert(WithinBorders(x, y));
            Square result = board[x, y];
            Debug.Assert(result != null, "Follows from invariant.");
            return result;
        }

        public bool WithinBorders(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
    }
}
