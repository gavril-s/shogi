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

        public Game()
        {
            Board = new ShogiBoard();
        }
    }
}
