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
        private bool end;
        private GameSide? winner;
        private List<ShogiPiece> whiteHand;
        private List<ShogiPiece> blackHand;
        private GameSide currentMoveSide;

        public Game()
        {
            Board = new ShogiBoard();
            end = false;
            winner = null;
            whiteHand = new List<ShogiPiece>();
            blackHand = new List<ShogiPiece>();
            currentMoveSide = GameSide.White;
        }

        public bool End()
        {
            return end;
        }

        public GameSide? Winner()
        {
            return winner;
        }

        public GameSide CurrentMoveSide()
        {
            return currentMoveSide;
        }

        private void Turn()
        {
            currentMoveSide = (currentMoveSide == GameSide.White) ? GameSide.Black : GameSide.White;
        }

        public List<(int x, int y)> AvailableMoves(int x, int y)
        {
            List<(int x, int y)> physicallyAvailableMoves = Board.AvailableMoves(x, y);
            List<(int x, int y)> result = new List<(int x, int y)>();
            foreach ((int x, int y) move in physicallyAvailableMoves)
            {
                GameSide side = (GameSide)Board.Side(x, y);
                if (!WouldBeCheckFrom(side.Opponent(), (x, y), move))
                {
                    result.Add(move);
                }
            }

            return result;
        }

        public bool Move((int x, int y) from, (int x, int y) to)
        {
            if (end == false &&
                Board.Side(from.x, from.y) == currentMoveSide &&
                !Board.OneSideStrict(from, to) &&
                Board.AvailableMoves(from.x, from.y).Contains(to) &&
                !WouldBeCheckFrom(currentMoveSide.Opponent(), from, to))
            {
                ShogiPiece? drop = Board.Move(from, to);
                if (drop != null)
                {
                    if (currentMoveSide == GameSide.White)
                    {
                        whiteHand.Add((ShogiPiece)drop);
                    }
                    else if (currentMoveSide == GameSide.Black)
                    {
                        blackHand.Add((ShogiPiece)drop);
                    }
                }

                if (IsCheckFrom(currentMoveSide))
                {
                    Console.WriteLine("CHECK!");
                }
                if (IsMateFrom(currentMoveSide))
                {
                    end = true;
                    winner = currentMoveSide;
                    Console.WriteLine("WIN");
                    Console.WriteLine((winner == GameSide.White) ? "WHITE" : "BLACK");
                }
                
                Turn();
                return true;
            }
            return false;
        }

        public bool Drop(GameSide side, int from, (int x, int y) to)
        {
            if (end)
            {
                return false;
            }

            ShogiPiece? piece = null;
            if (side == GameSide.White &&
                from >= 0 && from < whiteHand.Count)
            {
                piece = whiteHand[from];
            }
            else if (side == GameSide.Black &&
                from >= 0 && from < blackHand.Count)
            {
                piece = blackHand[from];
            }

            if (piece != null && Board.IsFree(to.x, to.y))
            {
                Board.Drop(piece, to);
                if (AvailableMoves(to.x, to.y).Count != 0 &&
                    !IsMateFrom(side))
                {
                    if (side == GameSide.White)
                    {
                        whiteHand.RemoveAt(from);
                    }
                    else if (side == GameSide.Black)
                    {
                        blackHand.RemoveAt(from);
                    }
                    return true;
                }
                else
                {
                    Board.Undo();
                    return false;
                }
            }
            return false;
        }

        private bool WouldBeCheckFrom(GameSide side, (int x, int y) from, (int x, int y) to)
        {
            Board.Move(from, to);
            bool res = IsCheckFrom(side);
            Board.Undo();
            return res;
        }
        private bool WouldBeMateFrom(GameSide side, (int x, int y) from, (int x, int y) to)
        {
            Board.Move(from, to);
            bool res = IsMateFrom(side);
            Board.Undo();
            return res;
        }
        private bool IsCheckFrom(GameSide side)
        {
            for (int i = 0; i < Board.Size; i++)
            {
                for (int j = 0; j < Board.Size; j++)
                {
                    if (Board.Side(i, j) == side)
                    {
                        var moves = Board.AvailableMoves(i, j);
                        if ((side == GameSide.White && moves.Contains(Board.BlackKing())) || 
                            (side == GameSide.Black && moves.Contains(Board.WhiteKing())))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool IsMateFrom(GameSide side)
        {
            return (side == GameSide.Black && AvailableMoves(Board.WhiteKing().x, Board.WhiteKing().y).Count == 0) ||
                   (side == GameSide.White && AvailableMoves(Board.BlackKing().x, Board.BlackKing().y).Count == 0);
        }
    }
}
