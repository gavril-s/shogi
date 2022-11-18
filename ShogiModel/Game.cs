using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShogiModel
{
    public class Game
    {
        public ShogiBoard Board { get; }
        public GameSide currentMoveSide;

        public Game()
        {
            Board = new ShogiBoard();
            currentMoveSide = GameSide.White;
        }

        private void Turn()
        {
            currentMoveSide = (currentMoveSide == GameSide.White) ? GameSide.Black : GameSide.White;
        }

        public bool Move((int x, int y) from, (int x, int y) to)
        {
            if (Board.Side(from.x, from.y) == currentMoveSide &&
                !Board.OneSideStrict(from, to) &&
                Board.AvailableMoves(from.x, from.y).Contains(to))
            {
                Board.Move(from, to);
                Turn();
                return true;
            }
            return false;
        }
    }
}
