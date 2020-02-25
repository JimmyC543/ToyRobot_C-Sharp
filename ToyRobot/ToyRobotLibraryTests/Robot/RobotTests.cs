using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Robot;
using ToyRobotLibrary.Table;
using ToyRobotLibrary.Utilities;
using Xunit;

namespace ToyRobotLibraryTests.Robot
{
    public class RobotTests
    {
        #region Constructor tests

        [Fact]
        public void Constructor_ShouldSucceed_WhenPassedValidTable()
        {
            //Arrange
            var mockTable = new Mock<RectangularTable>(1, 1);

            //Act
            IRobot robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);

            //Assert
            Assert.NotNull(robot);
            Assert.IsAssignableFrom<IRobot>(robot);
            Assert.IsType<ToyRobotLibrary.Robot.Robot>(robot);
        }
        [Fact]
        public void Constructor_ShouldThrowException_WhenPassedInvalidTable()
        {
            //Arrange
            IRobot robot;

            //Act
            Exception ex = Record.Exception(() => robot = new ToyRobotLibrary.Robot.Robot(null));

            //Assert
            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Contains("Can't use a robot without a table instance.", ex.Message);
        }

        #endregion


        #region Place tests
        [Theory]
        [InlineData(1, 1, 0, 0, 0)]
        [InlineData(5, 5, 3, 2, 3)]
        [InlineData(51, 15, 31, 12, 2)]
        public void Place_ShouldSucceed_WhenPassedValidCoordinatesAndOrientation(int numRows, int numCols, int xPlacement, int yPlacement, int orientationVal)
        {
            //Arrange
            var mockTable = new Mock<RectangularTable>(numRows, numCols);
            IRobot robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);
            var placementPosition = new Position { x = xPlacement, y = yPlacement };
            var positionField = robot.GetType().GetField("_position", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var orientationField = robot.GetType().GetField("_orientation", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            //Act
            robot.Place(placementPosition, (Orientation)orientationVal);

            var positionValue = positionField.GetValue(robot);
            var orientationValue = orientationField.GetValue(robot);

            //Assert
            Assert.Equal(positionValue, placementPosition);
            Assert.Equal(orientationValue, (Orientation)orientationVal);
        }

        [Theory]
        [InlineData(1, 1, -1, 0, 0)]
        [InlineData(5, 5, 3, 5, 3)]
        [InlineData(51, 15, 31, 12, 5)]
        public void Place_ShouldDoNothing_WhenPassedInvalidCoordinates(int numRows, int numCols, int xPlacement, int yPlacement, int orientationVal)
        {
            //Arrange
            var mockTable = new Mock<RectangularTable>(numRows, numCols);
            IRobot robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);
            var placementPosition = new Position { x = xPlacement, y = yPlacement };
            var positionField = robot.GetType().GetField("_position", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var orientationField = robot.GetType().GetField("_orientation", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            //Act
            robot.Place(placementPosition, (Orientation)orientationVal);

            var positionValue = positionField.GetValue(robot);
            var orientationValue = orientationField.GetValue(robot);

            //Assert
            Assert.Null(positionValue);
            Assert.Null(orientationValue);
        }
        #endregion


    }
}
