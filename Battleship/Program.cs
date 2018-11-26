using Battleship.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Battleship
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Configure the dependency injection container by creating a service collection
            // adding our dependencies, and finally building an IServiceProvider
            var serviceProvider = new ServiceCollection()                 
            .AddSingleton<IBoard, Board>()
            .AddSingleton<IPlayer, Player>()
            .BuildServiceProvider();

            Console.Title = "Battleship";
            Console.WriteLine("Welcome to Battleship!\r\n\r\n");
            Console.WriteLine("What is your name?");
            string name = Console.ReadLine();
            Console.WriteLine();

            var board = serviceProvider.GetService<IBoard>();
            var player = serviceProvider.GetService<IPlayer>();

            Console.WriteLine("Would you like to display ships on board? (y/n)");
            string strGridDisplayMode = Console.ReadLine();

            // Display ships on grid
            if (strGridDisplayMode == "y")
            {                                
                // Create list of battle ships
                var lstShips = new List<int>();

                // 1 ships of size 2
                lstShips.Add(2);

                // 2 ships of size 3 
                lstShips.Add(3);
                lstShips.Add(3);

                // 1 ship od size 4
                lstShips.Add(4);

                // 1 ship of size 5
                lstShips.Add(5);

                // Randomly place ships on grid
                foreach (var ship in lstShips)
                {
                    board.PlaceShips(ship);
                }                
            }

            while (player.getHitCount() < 17)
            {
                board.DisplayBoard(board.GetGrid());
                player.PromptCoordinates(null, null);
            }
            Console.WriteLine("Congratulations, " + name + "! You Win!\r\n");
            Console.WriteLine("You missed: " + player.getMissCount() + " times\r\n");
            Console.WriteLine("Thanks for playing Battleship. Press enter to quit.");
            Console.ReadLine();
        }
    }
}
