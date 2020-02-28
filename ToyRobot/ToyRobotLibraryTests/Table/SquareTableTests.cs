using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Table;
using ToyRobotLibrary.Utilities;
using Xunit;

namespace ToyRobotLibraryTests.Table
{
    public class SquareTableTests
    {
        #region Constructor tests

        [Theory]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(5)]
        public void Constructor_ShouldSucceed_WhenPassedValidLengths(int sideLength)
        {
            //TODO: It might be nicer to pass in some sort of object e.g. Dimensions<T, S> where T and S could be x & y or r & theta etc, or ints/doubles etc
            ITable table = new SquareTable(sideLength);
            Assert.NotNull(table);
            Assert.IsAssignableFrom<ITable>(table);
            Assert.IsAssignableFrom<RectangularTable>(table);
            Assert.IsType<SquareTable>(table);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Constructor_ShouldFail_WhenPassedInvalidLengths(int sideLength)
        {
            ITable table;
            Exception ex = Record.Exception(() => table = new SquareTable(sideLength));
            Assert.NotNull(ex);
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Table sides must be greater than 0.", ex.Message);
        }
        #endregion

        #region IsValidPosition tests

        [Theory]
        [InlineData(1,  0, 0)]//For a 1x1 grid, the only acceptable position is {x : 0, y: 0}
        [InlineData( 23, 6, 3)]
        [InlineData(5, 4, 4)]
        public void IsValidPosition_ShouldReturnTrue_WhenPassedValidCoordinates(int sideLength, int x, int y)
        {
            ITable table = new SquareTable(sideLength);
            Position position = new Position { x = x, y = y };
            Assert.True(table.IsValidPosition(position));
        }


        [Theory]
        [InlineData(1, 1, 1)]//For a 1x1 grid, the only acceptable position is {x : 0, y: 0}
        [InlineData(1, -1, -1)]//For a 1x1 grid, the only acceptable position is {x : 0, y: 0}
        [InlineData(23, 7, 24)]
        [InlineData(5, 5, 5)]//For a 5x5 grid, the acceptable range is {x : 0-> 4, y: 0 -> 4}
        public void IsValidPosition_ShouldReturnFalse_WhenPassedInvalidCoordinates(int sideLength, int x, int y)
        {
            ITable table = new SquareTable(sideLength);
            Position position = new Position { x = x, y = y };
            Assert.False(table.IsValidPosition(position));
        }
        #endregion

    }
}
