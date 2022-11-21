using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShogiModel
{
    // Самый главный класс проекта ShogiModel.
    // Заведует игровой логикой
    public class Game
    {
        private string whiteTurnMsg = "Ход белых";
        private string blackTurnMsg = "Ход чёрных";
        private string whiteCheckMsg = "Шах чёрным";
        private string blackCheckMsg = "Шах белым";
        private string whiteWinMsg = "Белые победили";
        private string blackWinMsg = "Чёрные победили";

        public ShogiBoard Board { get; }
        public ShogiHand WhiteHand { get; }
        public ShogiHand BlackHand { get; }

        private GameSide currentMoveSide;
        private bool end;
        private GameSide? winner;
        private string currentMessage;

        public Game()
        {
            Board = new ShogiBoard();
            WhiteHand = new ShogiHand();
            BlackHand = new ShogiHand();
            currentMoveSide = GameSide.White;
            end = false;
            winner = null;
            currentMessage = whiteTurnMsg;
        }

        public ShogiHand Hand(GameSide side)
        {
            return (side == GameSide.White) ? WhiteHand : BlackHand;
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

        public string CurrentMessage()
        {
            return currentMessage;
        }

        // переключение стороны хода
        private void Turn()
        {
            currentMoveSide = (currentMoveSide == GameSide.White) ? GameSide.Black : GameSide.White;
            currentMessage = (currentMoveSide == GameSide.White) ? whiteTurnMsg : blackTurnMsg;
        }

        // Показывает возможные ходы, но в отличие от 
        // GameBoard делает это с учётом ситуации в игре,
        // а именно отслеживает шахи
        public List<(int x, int y)> AvailableMoves(int x, int y)
        {
            if (end)
            {
                return new List<(int x, int y)>();
            }
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
        
        // Двигает фигуру с from на to. Может отказать (вернуть false),
        // если ход не соответствует правилам
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
                        WhiteHand.Add((ShogiPiece)drop);
                    }
                    else if (currentMoveSide == GameSide.Black)
                    {
                        BlackHand.Add((ShogiPiece)drop);
                    }
                }

                if (IsCheckFrom(currentMoveSide))
                {
                    currentMessage = (currentMoveSide == GameSide.White) ? whiteCheckMsg : blackCheckMsg;
                }
                if (IsMateFrom(currentMoveSide))
                {
                    SetWinner(currentMoveSide);
                }

                Turn();
                return true;
            }
            return false;
        }

        // Сбрасывает фишку номер from из руки игрока side на to
        // Также может отказаться, если такой ход невозможен
        public bool Drop(GameSide side, int from, (int x, int y) to)
        {
            if (end)
            {
                return false;
            }

            ShogiPiece? piece = null;
            if (side == GameSide.White &&
                from >= 0 && from < WhiteHand.Count())
            {
                piece = WhiteHand[from];
            }
            else if (side == GameSide.Black &&
                from >= 0 && from < BlackHand.Count())
            {
                piece = BlackHand[from];
            }

            if (side == currentMoveSide && piece != null && Board.IsFree(to.x, to.y))
            {
                Board.Drop(piece, to);
                if (AvailableMoves(to.x, to.y).Count != 0 &&
                    !IsMateFrom(side))
                {
                    if (side == GameSide.White)
                    {
                        WhiteHand.RemoveAt(from);
                    }
                    else if (side == GameSide.Black)
                    {
                        BlackHand.RemoveAt(from);
                    }
                    Turn();
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

        // Переворачивает. То же самое
        public bool Promote(int x, int y)
        {
            if (end)
            {
                return false;
            }
            
            if (Board.Side(x, y) == currentMoveSide && 
                Board.CanBePromoted(x, y))
            {
                Board.Promote(x, y);
                return true;
            }
            return false;
        }

        // Сторона side сдаётся
        public bool Surrender(GameSide side)
        {
            if (end)
            {
                return false;
            }

            SetWinner(side.Opponent());
            return true;
        }

        // Ходящая сейчас сторона сдаётся
        public bool Surrender()
        {
            return Surrender(currentMoveSide);
        }

        private void SetWinner(GameSide side)
        {
            end = true;
            winner = side;
            currentMessage = (winner == GameSide.White) ? whiteWinMsg : blackWinMsg;
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
