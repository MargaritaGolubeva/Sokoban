using System.Windows.Forms;

namespace Sokoban
{
    public static class Global
    {
        public static bool Touch = false;
        public static bool Move = false;
        public static int DelX = 0;
        public static int DelY = 0;
        public static int X = -1;
        public static int Y = -1;
    }

    public class Fence : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool Conflict(ICreature conflictedObject)
        {
            return false;
        }

        public string GetImageFileName()
        {
            return "wallNew.png";
        }
    }

    public class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            if (Global.Move)
            {
                Global.Move = false;
                return new CreatureCommand() { DeltaX = Global.DelX, DeltaY = Global.DelY };
            }

            switch (Game.KeyPressed)
            {
                case Keys.Left:
                    if (x - 1 >= 0)
                    {
                        return GetShiftPlayerWithBox(x - 1, y, -1, 0);
                    }
                    break;
                case Keys.Right:
                    if (x + 1 < Game.MapWidth)
                    {
                        return GetShiftPlayerWithBox(x + 1, y, 1, 0);
                    }
                    break;
                case Keys.Up:
                    if (y - 1 >= 0)
                    {
                        return GetShiftPlayerWithBox(x, y - 1, 0, -1);
                    }
                    break;
                case Keys.Down:
                    if (y + 1 < Game.MapHeight)
                    {
                        return GetShiftPlayerWithBox(x, y + 1, 0, 1);
                    }
                    break;
            }
            return new CreatureCommand() { };
        }

        private CreatureCommand GetShiftPlayerWithBox(int wayX, int wayY, int shiftBoxX, int shiftBoxY)
        {
            Box box = new Box();
            var map = Game.Map[wayX, wayY];
            if (!(map is Fence) && !(map is Stock))
            {
                if (map is Box)
                {
                    if (box.Touch(wayX + shiftBoxX, wayY + shiftBoxY))
                    {
                        Global.Touch = true;
                        Global.DelX = shiftBoxX;
                        Global.DelY = shiftBoxY;
                        Global.X = wayX;
                        Global.Y = wayY;
                    }
                    return new CreatureCommand();
                }
                return new CreatureCommand() { DeltaX = shiftBoxX, DeltaY = shiftBoxY };
            }
            return new CreatureCommand();
        }

        public bool Conflict(ICreature conflictedObject)
        {
            return false;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }

    public class Box : ICreature
    {
        public bool Touch(int x, int y)
        {
            return Game.Map[x, y] is null || Game.Map[x, y] is Stock;
        }

        public CreatureCommand Act(int x, int y)
        {
            if (Global.Touch && Global.X == x && Global.Y == y)
            {
                Global.Touch = false;
                Global.Move = true;
                return new CreatureCommand() { DeltaX = Global.DelX, DeltaY = Global.DelY };
            }

            return new CreatureCommand();
        }

        public bool Conflict(ICreature conflictedObject)
        {
            return true;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    public class Stock : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool Conflict(ICreature conflictedObject)
        {
            if (conflictedObject is Box)
            {
                Game.Scores += 10;
            }
            return false;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }
}
