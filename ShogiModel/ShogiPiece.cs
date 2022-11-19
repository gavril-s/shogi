using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShogiModel
{
    internal enum ShogiPiece : int
    {
        BishopWhite         = 1,
        DragonWhite         = 2,
        GoldWhite           = 3,
        HorseWhite          = 4,
        KingWhite           = 5,
        KnightWhite         = 6,
        LanceWhite          = 7,
        PawnWhite           = 8,
        RookWhite           = 9,
        SilverWhite         = 10,
        PromotedKnightWhite = 11,
        PromotedLanceWhite  = 12,
        PromotedPawnWhite   = 13,
        PromotedSilverWhite = 14,
        BishopBlack         = -1,
        DragonBlack         = -2,
        GoldBlack           = -3,
        HorseBlack          = -4,
        KingBlack           = -5,
        KnightBlack         = -6,
        LanceBlack          = -7,
        PawnBlack           = -8,
        RookBlack           = -9,
        SilverBlack         = -10,
        PromotedKnightBlack = -11,
        PromotedLanceBlack  = -12,
        PromotedPawnBlack   = -13,
        PromotedSilverBlack = -14,
    }

    internal static class ShogiPiecesUtils
    {
        static private Dictionary<ShogiPiece, string> names = new Dictionary<ShogiPiece, string>
        {
            { ShogiPiece.BishopWhite,         "BishopWhite" },
            { ShogiPiece.DragonWhite,         "DragonWhite" },
            { ShogiPiece.GoldWhite,           "GoldWhite" },
            { ShogiPiece.HorseWhite,          "HorseWhite" },
            { ShogiPiece.KingWhite,           "KingWhite" },
            { ShogiPiece.KnightWhite,         "KnightWhite" },
            { ShogiPiece.LanceWhite,          "LanceWhite" },
            { ShogiPiece.PawnWhite,           "PawnWhite" },
            { ShogiPiece.RookWhite,           "RookWhite" },
            { ShogiPiece.SilverWhite,         "SilverWhite" },
            { ShogiPiece.PromotedKnightWhite, "PromotedKnightWhite" },
            { ShogiPiece.PromotedLanceWhite,  "PromotedLanceWhite" },
            { ShogiPiece.PromotedPawnWhite,   "PromotedPawnWhite" },
            { ShogiPiece.PromotedSilverWhite, "PromotedSilverWhite" },
            { ShogiPiece.BishopBlack,         "BishopBlack" },
            { ShogiPiece.DragonBlack,         "DragonBlack" },
            { ShogiPiece.GoldBlack,           "GoldBlack" },
            { ShogiPiece.HorseBlack,          "HorseBlack" },
            { ShogiPiece.KingBlack,           "KingBlack" },
            { ShogiPiece.KnightBlack,         "KnightBlack" },
            { ShogiPiece.LanceBlack,          "LanceBlack" },
            { ShogiPiece.PawnBlack,           "PawnBlack" },
            { ShogiPiece.RookBlack,           "RookBlack" },
            { ShogiPiece.SilverBlack,         "SilverBlack" },
            { ShogiPiece.PromotedKnightBlack, "PromotedKnightBlack" },
            { ShogiPiece.PromotedLanceBlack,  "PromotedLanceBlack" },
            { ShogiPiece.PromotedPawnBlack,   "PromotedPawnBlack" },
            { ShogiPiece.PromotedSilverBlack, "PromotedSilverBlack" },
        };

        static private Dictionary<ShogiPiece, string> namesRUS = new Dictionary<ShogiPiece, string>
        {
            { ShogiPiece.BishopWhite,         "Слон (белые)" },
            { ShogiPiece.DragonWhite,         "Дракон (белые)" },
            { ShogiPiece.GoldWhite,           "Золото (белые)" },
            { ShogiPiece.HorseWhite,          "Лошадь (белые)" },
            { ShogiPiece.KingWhite,           "Король (белые)" },
            { ShogiPiece.KnightWhite,         "Конь (белые)" },
            { ShogiPiece.LanceWhite,          "Стрелка (белые)" },
            { ShogiPiece.PawnWhite,           "Пешка (белые)" },
            { ShogiPiece.RookWhite,           "Ладья (белые)" },
            { ShogiPiece.SilverWhite,         "Серебро (белые)" },
            { ShogiPiece.PromotedKnightWhite, "Перевёрнутый конь (белые)" },
            { ShogiPiece.PromotedLanceWhite,  "Перевёрнутая стрелка (белые)" },
            { ShogiPiece.PromotedPawnWhite,   "Токин (белые)" },
            { ShogiPiece.PromotedSilverWhite, "Перевёрнутое серебро (белые)" },
            { ShogiPiece.BishopBlack,         "Слон (чёрные)" },
            { ShogiPiece.DragonBlack,         "Дракон (чёрные)" },
            { ShogiPiece.GoldBlack,           "Золото (чёрные)" },
            { ShogiPiece.HorseBlack,          "Лошадь (чёрные)" },
            { ShogiPiece.KingBlack,           "Король (чёрные)" },
            { ShogiPiece.KnightBlack,         "Конь (чёрные)" },
            { ShogiPiece.LanceBlack,          "Стрелка (чёрные)" },
            { ShogiPiece.PawnBlack,           "Пешка (чёрные)" },
            { ShogiPiece.RookBlack,           "Ладья (чёрные)" },
            { ShogiPiece.SilverBlack,         "Серебро (чёрные)" },
            { ShogiPiece.PromotedKnightBlack, "Перевёрнутый конь (чёрные)" },
            { ShogiPiece.PromotedLanceBlack,  "Перевёрнутая стрелка (чёрные)" },
            { ShogiPiece.PromotedPawnBlack,   "Токин (чёрные)" },
            { ShogiPiece.PromotedSilverBlack, "Перевёрнутое серебро (чёрные)" },
        };

        public static string Name(this ShogiPiece piece)
        {
            return names[piece];
        }

        public static string NameRUS(this ShogiPiece piece)
        {
            return namesRUS[piece];
        }

        public static GameSide Side(this ShogiPiece piece)
        {
            return (((int)piece) > 0 ? GameSide.White : GameSide.Black);
        }

        public static ShogiPiece Opposite(this ShogiPiece piece)
        {
            return (ShogiPiece)(-(int)piece);
        }

        public static ShogiPiece Unpromote(this ShogiPiece piece)
        {
            switch (piece)
            {
                case ShogiPiece.PromotedKnightWhite:
                    return ShogiPiece.KnightWhite;
                case ShogiPiece.PromotedKnightBlack:
                    return ShogiPiece.KnightBlack;
                case ShogiPiece.PromotedLanceWhite:
                    return ShogiPiece.LanceWhite;
                case ShogiPiece.PromotedLanceBlack:
                    return ShogiPiece.LanceBlack;
                case ShogiPiece.PromotedPawnWhite:
                    return ShogiPiece.PawnWhite;
                case ShogiPiece.PromotedPawnBlack:
                    return ShogiPiece.PawnBlack;
                case ShogiPiece.PromotedSilverWhite:
                    return ShogiPiece.SilverWhite;
                case ShogiPiece.PromotedSilverBlack:
                    return ShogiPiece.SilverBlack;
                default:
                    return piece;
            }
        }
    }
}
