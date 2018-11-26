namespace Battleship.Interfaces
{
    public interface IPlayer
    {
        int getHitCount();
        int getMissCount();
        bool PromptCoordinates(int? x, int? y);
    }
}
