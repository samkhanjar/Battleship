using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Battleship.Interfaces;
using Battleship;

namespace Test
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Check_For_Correct_Placement_Of_Ship()
        {
            // Arrange
            // Configure the dependency injection container by creating a service collection
            // adding our dependencies, and finally building an IServiceProvider
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IBoard, Board>()
            .BuildServiceProvider();

            var board = serviceProvider.GetService<IBoard>();

            // Act
            var isCorrectlyPlaced = board.PlaceShips(3);

            // Assert
            Assert.IsTrue(isCorrectlyPlaced);
        }

        [TestMethod]
        public void Check_For_Valid_Entry()
        {
            // Arrange
            // Configure the dependency injection container by creating a service collection
            // adding our dependencies, and finally building an IServiceProvider
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IBoard, Board>()
            .AddSingleton<IPlayer, Player>()
            .BuildServiceProvider();

            var board = serviceProvider.GetService<IBoard>();
            var player = serviceProvider.GetService<IPlayer>();

            // Act
            var IsValidEntry = player.PromptCoordinates(3, 7);

            // Assert
            Assert.IsTrue(IsValidEntry);
        }

        [TestMethod]
        public void Check_For_Invalid_Entry()
        {
            // Arrange
            // Configure the dependency injection container by creating a service collection
            // adding our dependencies, and finally building an IServiceProvider
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IBoard, Board>()
            .AddSingleton<IPlayer, Player>()
            .BuildServiceProvider();

            var board = serviceProvider.GetService<IBoard>();
            var player = serviceProvider.GetService<IPlayer>();

            // Act
            var IsValidEntry = player.PromptCoordinates(-1, 7);

            // Assert
            Assert.IsTrue(IsValidEntry);
        }
    }
}
