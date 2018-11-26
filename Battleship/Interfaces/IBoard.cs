namespace Battleship.Interfaces
{
    public interface IBoard
    {
        char[,] GridBoard { get; set; }
        char[,] GetGrid();
        void SetGrid(int q, int w);
        bool PlaceShips(int shipSize);
        void DisplayBoard(char[,] Board);
    }
}
