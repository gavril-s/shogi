using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShogiModel
{
    public class ShogiBoard
    {
        public int Size = 9;
        private ShogiPiece[,] Board;

        public ShogiBoard()
        {
            Board = new ShogiPiece[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Board[i, j] = new ShogiPiece();
                }
            }
            Board[0, 0] = new ShogiPiece("pshk");
        }

        public ShogiPiece this[int x, int y]
        {
            get => Board[x, y];
        }
    }
}
