namespace Battleship
{
    /// <summary>
    /// Class to hold X and Y positions
    /// </summary>
    public class Position
    {
        public Position(int Row, int Column)
        {
            row = Row;
            column = Column;
        }

        public int row { get; set; }
        public int column { get; set; }
    }
}
