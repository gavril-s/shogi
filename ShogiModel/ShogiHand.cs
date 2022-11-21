using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShogiModel
{
    // Описание коллекции фишек на руке у игрока (сброс)
    // (на самом деле просто небольшая обёртка для List<ShogiPiece>
    public class ShogiHand
    {
        private List<ShogiPiece> pieces;

        public ShogiHand()
        {
            pieces = new List<ShogiPiece>();
        }

        public string Name(int x)
        {
            if (x < 0 || x >= pieces.Count)
            {
                return "";
            }
            return pieces[x].Name();
        }

        public string NameRUS(int x)
        {
            if (x < 0 || x >= pieces.Count)
            {
                return "";
            }
            return pieces[x].NameRUS();
        }

        public int Count()
        {
            return pieces.Count;
        }

        internal void Add(ShogiPiece piece)
        {
            pieces.Add(piece);
        }

        internal void Remove(ShogiPiece piece)
        {
            pieces.Remove(piece);
        }

        internal void RemoveAt(int ind)
        {
            pieces.RemoveAt(ind);
        }

        internal ShogiPiece this[int key]
        {
            get => pieces[key];
        }
    }
}
