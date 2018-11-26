using Battleship.Enums;
using Battleship.Interfaces;
using System;
using System.Collections.Generic;

namespace Battleship
{
    /// <summary>
    /// Board class to set the grid and place ships randomly on the grid.
    /// </summary>
    public class Board : IBoard
    {
        private const int GRID_SIZE = 10;
        private readonly Random random;
        private static List<KeyValuePair<int, int>> shipLocations;
        private int shipCount = 0;
        private bool isShipPlacedCorrectly = false;

        public char[,] GridBoard { get; set; }

        public Board()
        {
            GridBoard = new char[GRID_SIZE, GRID_SIZE];
            random = new Random();
            shipLocations = new List<KeyValuePair<int, int>>();
        }

        public char[,] GetGrid()
        {
            return GridBoard;
        }

        public void SetGrid(int x, int y)
        {
            GridBoard[x, y] = 'S';
        }

        public void DisplayBoard(char[,] Board)
        {
            int row;
            int column;

            Console.WriteLine("  ¦ 0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("--+--------------------");
            for (row = 0; row < GRID_SIZE; row++)
            {
                Console.Write((row).ToString() + " ¦ ");
                for (column = 0; column < GRID_SIZE; column++)
                {
                    Console.Write(Board[column, row] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n");
        }        

        // Places ship randomly on board
        public bool PlaceShips(int shipSize)
        {
            // Adding the first ship 
            shipCount++;
            // Determine ship direction (i.e. vertical or horizontal)
            var shipDirection = (shipCount % 2) == 1 ? ShipDirection.Vertical : ShipDirection.Horizontal;

            // If ship direction is horizontal 
            if (shipDirection == ShipDirection.Horizontal)
            {
                // Generate initial location X and Y position of the ship
                var ship_y_position = random.Next(0, GRID_SIZE);
                var ship_x_position = random.Next(0, GRID_SIZE);
                var success = false;
                
                var position = new Position(ship_x_position, ship_y_position);

                // Check for ship overlap 
                while (!success)
                {
                    position.column = ship_y_position;
                    position.row = ship_x_position;
                    
                    // Check if location exists
                    if (!shipLocations.Contains(new KeyValuePair<int, int>(position.row, position.column)))
                    {
                        var attempts = 0;
                        var tempShipLocations = new List<KeyValuePair<int, int>>();

                        // If number of attempts equals to (shipsize - 1) then we have successfully placed a ship on the board
                        while (attempts < shipSize)
                        {
                            // Determine if next position is from the left or right of the initial position.                              
                            if ((GRID_SIZE - ship_x_position) < shipSize)
                            {
                                position.column = ship_y_position;
                                position.row = ship_x_position--;

                                // Check ship exists at this location if it's clear then add the location otherwise generate new set of numbers for X and Y 
                                if (!shipLocations.Contains(new KeyValuePair<int, int>(position.row, position.column)) &&
                                    !tempShipLocations.Contains(new KeyValuePair<int, int>(position.row, position.column)))
                                {           
                                    // Add coords and increment attempts counter
                                    tempShipLocations.Add(new KeyValuePair<int, int>(position.row, position.column));
                                    attempts++;
                                }
                                else
                                {
                                    // Reset attempts back to zero and clear the temp key value pair if we have an over lap 
                                    attempts = 0;
                                    tempShipLocations.Clear();
                                    ship_y_position = random.Next(0, GRID_SIZE);
                                    ship_x_position = random.Next(0, GRID_SIZE);
                                }
                            }
                            else
                            {     
                                // Next postion is right of initial position
                                position.column = ship_y_position;
                                position.row = ship_x_position++;

                                if (!shipLocations.Contains(new KeyValuePair<int, int>(position.row, position.column)) &&
                                    !tempShipLocations.Contains(new KeyValuePair<int, int>(position.row, position.column)))
                                {                                   
                                    tempShipLocations.Add(new KeyValuePair<int, int>(position.row, position.column));
                                    attempts++;
                                }
                                else
                                {
                                    attempts = 0;
                                    tempShipLocations.Clear();
                                    ship_y_position = random.Next(0, GRID_SIZE);
                                    ship_x_position = random.Next(0, GRID_SIZE);
                                }
                            }
                        }
                            
                        foreach(var location in tempShipLocations)
                        {
                            shipLocations.Add(new KeyValuePair<int, int>(location.Key, location.Value));
                        }

                        success = true;
                        isShipPlacedCorrectly = success;
                    }
                    else
                    {
                        ship_y_position = random.Next(0, GRID_SIZE);
                        ship_x_position = random.Next(0, GRID_SIZE);
                    }
                }
            }
            else // ship direction is vertical 
            {
                // Generate initial location X and Y position of the ship
                var ship_y_position = random.Next(0, GRID_SIZE);
                var ship_x_position = random.Next(0, GRID_SIZE);
                var success = false;

                var position = new Position(ship_x_position, ship_y_position);

                // Check for ship overlap 
                while (!success)
                {
                    position.column = ship_y_position;
                    position.row = ship_x_position;

                    // If initial position doesn't exists then check if ship can be placed there
                    if (!shipLocations.Contains(new KeyValuePair<int, int>(position.row, position.column)))
                    {
                        var attempts = 0;
                        var tempShipLocations = new List<KeyValuePair<int, int>>();

                        while (attempts < shipSize)
                        {
                            // Determine if next position is above or bellow the initial position
                            if ((GRID_SIZE - ship_y_position) < shipSize)
                            {
                                position.column = ship_y_position--;
                                position.row = ship_x_position;

                                // Check ship exists at this location if it's clear then add the location otherwise generate new set of numbers for X and Y 
                                if (!shipLocations.Contains(new KeyValuePair<int, int>(position.row, position.column)) &&
                                    !tempShipLocations.Contains(new KeyValuePair<int, int>(position.row, position.column)))
                                {
                                    // Add coords and increment attempts counter
                                    tempShipLocations.Add(new KeyValuePair<int, int>(position.row, position.column));
                                    attempts++;
                                }
                                else
                                {
                                    // Reset attempts back to zero and clear the temp key value pair if we have an over lap
                                    attempts = 0;
                                    tempShipLocations.Clear();
                                    ship_y_position = random.Next(0, GRID_SIZE);
                                    ship_x_position = random.Next(0, GRID_SIZE);
                                }
                            }
                            else
                            {
                                // Next position is bellow initial position 
                                position.column = ship_y_position++;
                                position.row = ship_x_position;

                                if (!shipLocations.Contains(new KeyValuePair<int, int>(position.row, position.column)) && 
                                    !tempShipLocations.Contains(new KeyValuePair<int, int>(position.row, position.column)))
                                {
                                    tempShipLocations.Add(new KeyValuePair<int, int>(position.row, position.column));
                                    attempts++;
                                }
                                else
                                {
                                    attempts = 0;
                                    tempShipLocations.Clear();
                                    ship_y_position = random.Next(0, GRID_SIZE);
                                    ship_x_position = random.Next(0, GRID_SIZE);
                                }
                            }
                        }

                        foreach (var location in tempShipLocations)
                        {
                            shipLocations.Add(new KeyValuePair<int, int>(location.Key, location.Value));
                        }

                        success = true;
                        isShipPlacedCorrectly = success;
                    }
                    else
                    {
                        ship_y_position = random.Next(0, GRID_SIZE);
                        ship_x_position = random.Next(0, GRID_SIZE);
                    }
                }
            }

            foreach(var ship in shipLocations)
            {
                SetGrid(ship.Key, ship.Value);
            }

            return isShipPlacedCorrectly;
        }
    }
}
