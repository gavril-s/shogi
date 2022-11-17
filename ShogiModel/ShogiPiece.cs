using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShogiModel
{
    public class ShogiPiece
    {
        public string Name { get; }
        public ShogiPiece()
        {
            Name = "";
        }

        public ShogiPiece(string name)
        {
            Name = name;
        }
    }
}
