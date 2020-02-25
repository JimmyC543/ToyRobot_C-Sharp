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

        [Theory]
        [InlineData(1, 1, 0, 0, -1)]
        [InlineData(5, 5, 3, 3, 4)]
        public void Place_ShouldDoNothing_WhenPassedInvalidOrientation(int numRows, int numCols, int xPlacement, int yPlacement, int orientationVal)
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

        #region Rotate tests

        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(1, 1, 2)]
        [InlineData(2, 1, 3)]
        [InlineData(3, 1, 0)]
        [InlineData(0, -1, 3)]
        [InlineData(3, -1, 2)]
        [InlineData(2, -1, 1)]
        [InlineData(1, -1, 0)]
        public void Rotate_ShouldSucceed_WithSingleSpin(int startingOrientation, int spin, int expectedOrientation)
        {
            //Arrange
            var mockTable = new Mock<RectangularTable>(1, 1);
            IRobot robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);
            var positionField = robot.GetType().GetField("_position", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var orientationField = robot.GetType().GetField("_orientation", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            positionField.SetValue(robot, new Position { x = 0, y = 0 });
            orientationField.SetValue(robot, (Orientation)startingOrientation);

            //Act
            robot.Rotate((SpinDirection)spin);

            //Assert
            Assert.Equal((Orientation)expectedOrientation, orientationField.GetValue(robot));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        public void Rotate_ShouldDoNothingBeforeRobotIsPlaced(int spin)
        {
            //Arrange
            var mockTable = new Mock<RectangularTable>(1, 1);
            IRobot robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);
            var orientationField = robot.GetType().GetField("_orientation", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            //Act
            robot.Rotate((SpinDirection)spin);

            //Assert
            Assert.Null(orientationField.GetValue(robot));
        }
        #endregion
    }
}
