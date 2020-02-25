using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Table;
using ToyRobotLibrary.Utilities;
using Xunit;

namespace ToyRobotLibraryTests.Table
{
    /// <summary>
    /// Unit tests for any public constructors or methods of the RectangularTable class
    /// </summary>
    public class RectangularTableTests
    {
        #region Constructor tests

        [Theory]
        [InlineData(1, 1)]
        [InlineData(7, 23)]
        [InlineData(5, 5)]
        public void Constructor_ShouldSucceed_WhenPassedValidLengths(int width, int length)
        {
            //TODO: It might be nicer to pass in some sort of object e.g. Dimensions<T, S> where T and S could be x & y or r & theta etc, or ints/doubles etc
            ITable table = new RectangularTable(width, length);
            Assert.NotNull(table);
            Assert.IsAssignableFrom<ITable>(table);
            Assert.IsType<RectangularTable>(table);
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(0, 23)]
        [InlineData(5, -5)]
        public void Constructor_ShouldFail_WhenPassedInvalidLengths(int width, int length)
        {
            ITable table;
            Exception ex = Record.Exception(() => table = new RectangularTable(width, length));
            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
        }
        #endregion

        #region IsValidPosition tests

        [Theory]
        [InlineData(1, 1, 0, 0)]//For a 1x1 grid, the only acceptable position is {x : 0, y: 0}
        [InlineData(7, 23, 6, 3)]
        [InlineData(5, 5, 4, 4)]
        public void IsValidPosition_ShouldReturnTrue_WhenPassedValidCoordinates(int width, int length, int x, int y)
        {
            ITable table = new RectangularTable(width, length);
            Position position = new Position { x = x, y = y };
            Assert.True(table.IsValidPosition(position));
        }


        [Theory]
        [InlineData(1, 1, 1, 1)]//For a 1x1 grid, the only acceptable position is {x : 0, y: 0}
        [InlineData(1, 1, -1, -1)]//For a 1x1 grid, the only acceptable position is {x : 0, y: 0}
        [InlineData(7, 23, 7, 24)]
        [InlineData(5, 5, 5, 5)]//For a 5x5 grid, the acceptable range is {x : 0-> 4, y: 0 -> 4}
        public void IsValidPosition_ShouldReturnFalse_WhenPassedInvalidCoordinates(int width, int length, int x, int y)
        {
            ITable table = new RectangularTable(width, length);
            Position position = new Position { x = x, y = y };
            Assert.False(table.IsValidPosition(position));
        }
        #endregion
    }
}
