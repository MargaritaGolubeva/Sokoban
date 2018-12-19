using System.Windows.Forms;

namespace Sokoban
{
    class Game
    {
        public const int CountLevel = 3;

        private const string level1 = @"
FFFFFFFFFFFFFFFFFF
F                F
F                F
F                F
F    B           F
F                F
F                F
FPB             SF
F                F
FFFFFFFFFFFFFFFFFF";

        private const string level2 = @"
FFFFFFFFFFFFFFFFFF
F                F
F      B         F
F                F
F       FFFF     F
F       FS  B    F
F       FFFF     F
FP         B     F
F                F
FFFFFFFFFFFFFFFFFF";

        private const string level3 = @"
FFFFFFFFFFFFFFFFFF
F         F     SF
F         F      F
F         F      F
F   F     F      F
F   F     F      F
F   F    BF      F
FPB F           BF
F   F            F
FFFFFFFFFFFFFFFFFF";

        public static ICreature[,] Map;
        public static int Scores;

        public static Keys KeyPressed;
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);

        public static void CreateMap(int level)
        {
            Scores = 0;
            Map = CreatureMapCreator.CreateMap(SelectLevel(level));
        }

        private static string SelectLevel(int number)
        {
            switch (number)
            {
                case 1: return level1;
                case 2: return level2;
                case 3: return level3;
                default: return level1;
            }

        }
    }
}