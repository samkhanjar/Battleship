using Battleship.Interfaces;
using System;

namespace Battleship
{
    /// <summary>
    /// Prompts user to enter X and Y position for a hit or a miss
    /// </summary>
    public class Player : IPlayer
    {
        private readonly IBoard _board;

        public int HitCount = 0;
        public int MissCount = 0;

        int x = 0;
        int y = 0;

        // Inject IBoard interface 
        public Player(IBoard board)
        {
            _board = board;
        }

        // Get number of miss hits
        public int getMissCount()
        {
            return MissCount;
        }

        // Get number of hits
        public int getHitCount()
        {
            return HitCount;
        }    

        public bool PromptCoordinates(int? row, int? col)
        {            
            string line = string.Empty;
            int value;
            bool isValidEntry = false;

            // Enter the X coordinate
            if (row.HasValue) {
                int.TryParse(row.Value.ToString(), out value);
            }
            else
            {
                Console.WriteLine("Enter X");
                line = Console.ReadLine();
                int.TryParse(line, out value);
            }                
                   
            // Check if value entered is positive integer
            if (value >= 0)
            {
                x = value;
            }
            else
            {
                Console.WriteLine("Not an integer!");
            }

            // Enter the Y coordinate            
            if (col.HasValue)
            {
                int.TryParse(col.Value.ToString(), out value);
            }
            else
            {                
                Console.WriteLine("Enter Y");
                line = Console.ReadLine();
                int.TryParse(line, out value);
            }                

            // Check if value entered is positive integer
            if (value >= 0)
            {
                y = value;
            }
            else
            {
                Console.WriteLine("Not an integer!");
            }

            try
            {
                // Check if location entered equals to 'S' then change it to 'H' for a hit. Otherwise set it to 'M' for a miss
                if (_board.GridBoard[x, y].Equals('S'))
                {
                    _board.GridBoard[x, y] = 'H';
                    Console.Clear();
                    Console.WriteLine("Hit!\r\n");
                    HitCount += 1;
                }
                else 
                {
                    _board.GridBoard[x, y] = 'M';
                    Console.Clear();
                    Console.WriteLine("Miss!\r\n");
                    MissCount += 1;
                }

                isValidEntry = true;
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("\r\nError: Please enter numbers between 0 and 9. (Inclusive)\r\n");
                isValidEntry = false;
            }

            return isValidEntry;
        }
    }
}
