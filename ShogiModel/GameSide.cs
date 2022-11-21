using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShogiModel
{
    // Ну тут-то всё точно понятно
    public enum GameSide : int
    {
        White = 0, 
        Black = 1
    }

    public static class GameSideOpponent
    {
        public static GameSide Opponent(this GameSide side)
        {
            return (side == GameSide.White) ? GameSide.Black : GameSide.White;
        }
    }
}
