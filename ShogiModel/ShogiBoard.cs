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
        public int PromoteBorder = 3;
        private ShogiPiece?[,] prevBoardState;
        private (int x, int y) prevWhiteKing;
        private (int x, int y) prevBlackKing;
        private ShogiPiece?[,] Board;
        private (int x, int y) whiteKing;
        private (int x, int y) blackKing;

        internal ShogiBoard()
        {
            Board = new ShogiPiece?[Size, Size];
            FillBoardDefault();
        }

        private void SaveBoard()
        {
            prevBoardState = (ShogiPiece?[,])Board.Clone();
            prevWhiteKing = whiteKing;
            prevBlackKing = blackKing;
        }

        internal ShogiPiece? Move((int x, int y) from, (int x, int y) to)
        {
            SaveBoard();
            if (InsideBoard(from.x, from.y) && InsideBoard(to.x, to.y))
            {
                if (from == whiteKing)
                {
                    whiteKing = to;
                }
                if (from == blackKing)
                {
                    blackKing = to;
                }
                ShogiPiece? drop = Board[to.x, to.y];
                Board[to.x, to.y] = Board[from.x, from.y];
                Board[from.x, from.y] = null;
                if (drop != null)
                {
                    ShogiPiece dropPiece = (ShogiPiece)drop;
                    return dropPiece.Opposite().Unpromote();
                }
            }
            return null;
        }

        internal void Drop(ShogiPiece? piece, (int x, int y) to)
        {
            SaveBoard();
            if (InsideBoard(to.x, to.y))
            {
                Board[to.x, to.y] = piece;
            }
        }

        internal void Promote(int x, int y)
        {
            SaveBoard();
            if (InsideBoard(x, y) && !IsFree(x, y))
            {
                ShogiPiece piece = (ShogiPiece)Board[x, y];
                Board[x, y] = piece.Promote();
            }
        }

        internal void Undo()
        {
            Board = (ShogiPiece?[,])prevBoardState.Clone();
            whiteKing = prevWhiteKing;
            blackKing = prevBlackKing;
        }

        public string NameRUS(int x, int y)
        {
            if (!InsideBoard(x, y) || IsFree(x, y))
            {
                return "";
            }
            ShogiPiece piece = (ShogiPiece)Board[x, y];
            return piece.NameRUS();
        }

        public string Name(int x, int y)
        {
            if (!InsideBoard(x, y) || IsFree(x, y))
            {
                return "";
            }
            ShogiPiece piece = (ShogiPiece)Board[x, y];
            return piece.Name();
        }

        public (int x, int y) WhiteKing()
        {
            return whiteKing;
        }

        public (int x, int y) BlackKing()
        {
            return blackKing;
        }

        public bool CanBePromoted(int x, int y)
        {
            if (!InsideBoard(x, y) || IsFree(x, y))
            {
                return false;
            }
            ShogiPiece piece = (ShogiPiece)Board[x, y];

            return piece.CanBePromoted() &&
                ((piece.Side() == GameSide.White && x < PromoteBorder) ||
                 (piece.Side() == GameSide.Black && x >= Size - PromoteBorder));
        }

        public bool InsideBoard(int x, int y)
        {
            return (x >= 0 && x < Board.GetLength(0) &&
                    y >= 0 && y < Board.GetLength(1));
        }

        public bool IsFree(int x, int y)
        {
            return (InsideBoard(x, y) && Board[x, y] == null);
        }

        public bool OneSideSoft((int x, int y) first, (int x, int y) second)
        {
            if (!InsideBoard(first.x, first.y) || !InsideBoard(second.x, second.y))
            {
                return true;
            }
            if (IsFree(first.x, first.y) || IsFree(second.x, second.y))
            {
                return true;
            }
            ShogiPiece firstPiece  = (ShogiPiece)Board[first.x, first.y];
            ShogiPiece secondPiece = (ShogiPiece)Board[second.x, second.y];
            return firstPiece.Side() == secondPiece.Side();
        }

        public bool OneSideStrict((int x, int y) first, (int x, int y) second)
        {
            if (!InsideBoard(first.x, first.y) || !InsideBoard(second.x, second.y))
            {
                return false;
            }
            if (IsFree(first.x, first.y) || IsFree(second.x, second.y))
            {
                return false;
            }
            ShogiPiece firstPiece = (ShogiPiece)Board[first.x, first.y];
            ShogiPiece secondPiece = (ShogiPiece)Board[second.x, second.y];
            return firstPiece.Side() == secondPiece.Side();
        }

        public GameSide? Side(int x, int y)
        {
            if (!InsideBoard(x, y) || IsFree(x, y))
            {
                return null;
            }
            ShogiPiece piece = (ShogiPiece)Board[x, y];
            return piece.Side();
        }

        private bool AddIfReachable(List<(int x, int y)> list, (int x, int y) from, (int x, int y) to)
        {
            if (IsFree(to.x, to.y) || !OneSideStrict(from, to))
            {
                list.Add(to);
                return true;
            }
            return false;
        }

        internal List<(int x, int y)> AvailableMoves(int x, int y)
        {
            List<(int x, int y)> result = new List<(int x, int y)>();

            if (!InsideBoard(x, y) || Board[x, y] == null)
            {
                return result;
            }

            (int xSign, int ySign)[] signIter;

            switch (Board[x, y])
            {
                case ShogiPiece.BishopWhite:
                case ShogiPiece.BishopBlack:
                    signIter = new (int xSign, int ySign)[]
                    {
                        ( 1,  1),
                        ( 1, -1),
                        (-1,  1),
                        (-1, -1),
                    };
                    for (int j = 0; j < signIter.Length; j++)
                    {
                        int xSign = signIter[j].xSign;
                        int ySign = signIter[j].ySign;
                        for (int i = 1;
                            InsideBoard(x + i * xSign, y + i * ySign) &&
                            (IsFree(x + i * xSign, y + i * ySign) ||
                            !OneSideSoft((x, y), (x + i * xSign, y + i * ySign)));
                            i++)
                        {
                            result.Add((x + i * xSign, y + i * ySign));
                            if (!OneSideSoft((x, y), (x + i * xSign, y + i * ySign)))
                            {
                                break;
                            }
                        }
                    }
                    break;

                case ShogiPiece.DragonWhite:
                case ShogiPiece.DragonBlack:
                    signIter = new (int xSign, int ySign)[]
                    {
                        ( 1,  0),
                        ( 0,  1),
                        (-1,  0),
                        ( 0, -1),
                    };
                    for (int j = 0; j < signIter.Length; j++)
                    {
                        int xSign = signIter[j].xSign;
                        int ySign = signIter[j].ySign;
                        for (int i = 1;
                            InsideBoard(x + i * xSign, y + i * ySign) &&
                            (IsFree(x + i * xSign, y + i * ySign) ||
                            !OneSideSoft((x, y), (x + i * xSign, y + i * ySign)));
                            i++)
                        {
                            result.Add((x + i * xSign, y + i * ySign));
                            if (!OneSideSoft((x, y), (x + i * xSign, y + i * ySign)))
                            {
                                break;
                            }
                        }
                    }
                    AddIfReachable(result, (x, y), (x + 1, y + 1));
                    AddIfReachable(result, (x, y), (x + 1, y - 1));
                    AddIfReachable(result, (x, y), (x - 1, y + 1));
                    AddIfReachable(result, (x, y), (x - 1, y - 1));
                    break;

                case ShogiPiece.GoldWhite:
                case ShogiPiece.PromotedKnightWhite:
                case ShogiPiece.PromotedLanceWhite:
                case ShogiPiece.PromotedPawnWhite:
                case ShogiPiece.PromotedSilverWhite:
                    AddIfReachable(result, (x, y), (x - 1, y));
                    AddIfReachable(result, (x, y), (x - 1, y - 1));
                    AddIfReachable(result, (x, y), (x - 1, y + 1));
                    AddIfReachable(result, (x, y), (x, y - 1));
                    AddIfReachable(result, (x, y), (x, y + 1));
                    AddIfReachable(result, (x, y), (x + 1, y));
                    break;
                case ShogiPiece.GoldBlack:
                case ShogiPiece.PromotedKnightBlack:
                case ShogiPiece.PromotedLanceBlack:
                case ShogiPiece.PromotedPawnBlack:
                case ShogiPiece.PromotedSilverBlack:
                    AddIfReachable(result, (x, y), (x + 1, y));
                    AddIfReachable(result, (x, y), (x + 1, y - 1));
                    AddIfReachable(result, (x, y), (x + 1, y + 1));
                    AddIfReachable(result, (x, y), (x, y - 1));
                    AddIfReachable(result, (x, y), (x, y + 1));
                    AddIfReachable(result, (x, y), (x - 1, y));
                    break;

                case ShogiPiece.HorseWhite:
                case ShogiPiece.HorseBlack:
                    signIter = new (int xSign, int ySign)[]
                    {
                        ( 1,  1),
                        ( 1, -1),
                        (-1,  1),
                        (-1, -1),
                    };
                    for (int j = 0; j < signIter.Length; j++)
                    {
                        int xSign = signIter[j].xSign;
                        int ySign = signIter[j].ySign;
                        for (int i = 1;
                            InsideBoard(x + i * xSign, y + i * ySign) &&
                            (IsFree(x + i * xSign, y + i * ySign) ||
                            !OneSideSoft((x, y), (x + i * xSign, y + i * ySign)));
                            i++)
                        {
                            result.Add((x + i * xSign, y + i * ySign));
                            if (!OneSideSoft((x, y), (x + i * xSign, y + i * ySign)))
                            {
                                break;
                            }
                        }
                    }
                    AddIfReachable(result, (x, y), (x + 1, y));
                    AddIfReachable(result, (x, y), (x - 1, y));
                    AddIfReachable(result, (x, y), (x, y + 1));
                    AddIfReachable(result, (x, y), (x, y - 1));
                    break;

                case ShogiPiece.KingWhite:
                case ShogiPiece.KingBlack:
                    AddIfReachable(result, (x, y), (x + 1, y));
                    AddIfReachable(result, (x, y), (x + 1, y + 1));
                    AddIfReachable(result, (x, y), (x, y + 1));
                    AddIfReachable(result, (x, y), (x - 1, y + 1));
                    AddIfReachable(result, (x, y), (x - 1, y));
                    AddIfReachable(result, (x, y), (x - 1, y - 1));
                    AddIfReachable(result, (x, y), (x, y - 1));
                    AddIfReachable(result, (x, y), (x + 1, y - 1));
                    break;

                case ShogiPiece.KnightWhite:
                    AddIfReachable(result, (x, y), (x - 2, y + 1));
                    AddIfReachable(result, (x, y), (x - 2, y - 1));
                    break;
                case ShogiPiece.KnightBlack:
                    AddIfReachable(result, (x, y), (x + 2, y + 1));
                    AddIfReachable(result, (x, y), (x + 2, y - 1));
                    break;

                case ShogiPiece.LanceWhite:
                    for (int i = 1;
                        InsideBoard(x - i, y) &&
                        (IsFree(x - i, y) ||
                        !OneSideSoft((x, y), (x - i, y)));
                        i++)
                    {
                        result.Add((x - i, y));
                        if (!OneSideSoft((x, y), (x - i, y)))
                        {
                            break;
                        }
                    }
                    break;
                case ShogiPiece.LanceBlack:
                    for (int i = 1;
                        InsideBoard(x + i, y) &&
                        (IsFree(x + i, y) ||
                        !OneSideSoft((x, y), (x + i, y)));
                        i++)
                    {
                        result.Add((x + i, y));
                        if (!OneSideSoft((x, y), (x + i, y)))
                        {
                            break;
                        }
                    }
                    break;

                case ShogiPiece.PawnWhite:
                    AddIfReachable(result, (x, y), (x - 1, y));
                    break;
                case ShogiPiece.PawnBlack:
                    AddIfReachable(result, (x, y), (x + 1, y));
                    break;

                case ShogiPiece.RookWhite:
                case ShogiPiece.RookBlack:
                    signIter = new (int xSign, int ySign)[]
                    {
                        (1, 0),
                        (0, 1),
                        (-1, 0),
                        (0, -1),
                    };
                    for (int j = 0; j < signIter.Length; j++)
                    {
                        int xSign = signIter[j].xSign;
                        int ySign = signIter[j].ySign;
                        for (int i = 1;
                            InsideBoard(x + i * xSign, y + i * ySign) &&
                            (IsFree(x + i * xSign, y + i * ySign) ||
                            !OneSideSoft((x, y), (x + i * xSign, y + i * ySign)));
                            i++)
                        {
                            result.Add((x + i * xSign, y + i * ySign));
                            if (!OneSideSoft((x, y), (x + i * xSign, y + i * ySign)))
                            {
                                break;
                            }
                        }
                    }
                    break;

                case ShogiPiece.SilverWhite:
                    AddIfReachable(result, (x, y), (x - 1, y));
                    AddIfReachable(result, (x, y), (x - 1, y - 1));
                    AddIfReachable(result, (x, y), (x - 1, y + 1));
                    AddIfReachable(result, (x, y), (x + 1, y - 1));
                    AddIfReachable(result, (x, y), (x + 1, y + 1));
                    break;
                case ShogiPiece.SilverBlack:
                    AddIfReachable(result, (x, y), (x + 1, y));
                    AddIfReachable(result, (x, y), (x + 1, y - 1));
                    AddIfReachable(result, (x, y), (x + 1, y + 1));
                    AddIfReachable(result, (x, y), (x - 1, y - 1));
                    AddIfReachable(result, (x, y), (x - 1, y + 1));
                    break;

                default:
                    break;
            }

            return result;
        }
        private void FillBoardDefault()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Board[i, j] = null;
                }
            }

            Board[Size - 1, 0       ] = ShogiPiece.LanceWhite;
            Board[Size - 1, Size - 1] = ShogiPiece.LanceWhite;
            Board[0,        0       ] = ShogiPiece.LanceBlack;
            Board[0,        Size - 1] = ShogiPiece.LanceBlack;

            Board[Size - 1, 1       ] = ShogiPiece.KnightWhite;
            Board[Size - 1, Size - 2] = ShogiPiece.KnightWhite;
            Board[0,        1       ] = ShogiPiece.KnightBlack;
            Board[0,        Size - 2] = ShogiPiece.KnightBlack;

            Board[Size - 1, 2       ] = ShogiPiece.SilverWhite;
            Board[Size - 1, Size - 3] = ShogiPiece.SilverWhite;
            Board[0,        2       ] = ShogiPiece.SilverBlack;
            Board[0,        Size - 3] = ShogiPiece.SilverBlack;

            Board[Size - 1, 3       ] = ShogiPiece.GoldWhite;
            Board[Size - 1, Size - 4] = ShogiPiece.GoldWhite;
            Board[0,        3       ] = ShogiPiece.GoldBlack;
            Board[0,        Size - 4] = ShogiPiece.GoldBlack;

            Board[Size - 1, 4] = ShogiPiece.KingWhite;
            Board[0,        4] = ShogiPiece.KingBlack;
            whiteKing = (Size - 1, 4);
            blackKing = (0, 4);

            Board[Size - 2, 1       ] = ShogiPiece.BishopWhite;
            Board[1,        Size - 2] = ShogiPiece.BishopBlack;

            Board[Size - 2, Size - 2] = ShogiPiece.RookWhite;
            Board[1,        1       ] = ShogiPiece.RookBlack;

            for (int i = 0; i < Size; i++)
            {
                Board[Size - 3, i] = ShogiPiece.PawnWhite;
                Board[2,        i] = ShogiPiece.PawnBlack;
            }
        }
    }
}
