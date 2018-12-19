using System.Drawing;

namespace Sokoban
{
    public class CreatureAnimation
    {
        public CreatureCommand Command;
        public ICreature Creature;
        public Point Location;
        public Point TargetLogicalLocation;
    }
}