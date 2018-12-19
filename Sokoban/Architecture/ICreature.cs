namespace Sokoban
{
    public interface ICreature
    {
        string GetImageFileName();
        CreatureCommand Act(int x, int y);
        bool Conflict(ICreature conflictedObject);
    }
}