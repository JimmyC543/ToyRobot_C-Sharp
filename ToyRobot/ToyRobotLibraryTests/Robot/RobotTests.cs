using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
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
            var mockTable = new Mock<RectangularTable>(1, 1);
            IRobot robot;

            //Act
            robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);
            var positionField = robot.GetType().GetField("_position", BindingFlags.NonPublic | BindingFlags.Instance);
            var orientationField = robot.GetType().GetField("_orientation", BindingFlags.NonPublic | BindingFlags.Instance);
            var tableField = robot.GetType().GetField("_table", BindingFlags.NonPublic | BindingFlags.Instance);


            //Assert
            Assert.Null(orientationField.GetValue(robot));
            Assert.Null(positionField.GetValue(robot));
            Assert.NotNull(tableField.GetValue(robot));
            Assert.False(robot.IsPlaced);
        }

        [Fact]
        public void Constructor_PositionAndOrientationShouldBeNull_WhenFirstCreated()
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
            var positionField = robot.GetType().GetField("_position", BindingFlags.NonPublic | BindingFlags.Instance);
            var orientationField = robot.GetType().GetField("_orientation", BindingFlags.NonPublic | BindingFlags.Instance);

            //Act
            robot.Place(placementPosition, (Orientation)orientationVal);

            var positionValue = positionField.GetValue(robot);
            var orientationValue = orientationField.GetValue(robot);

            //Assert
            Assert.Equal(positionValue, placementPosition);
            Assert.Equal(orientationValue, (Orientation)orientationVal);
            Assert.True(robot.IsPlaced);
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
            var positionField = robot.GetType().GetField("_position", BindingFlags.NonPublic | BindingFlags.Instance);
            var orientationField = robot.GetType().GetField("_orientation", BindingFlags.NonPublic | BindingFlags.Instance);

            //Act
            robot.Place(placementPosition, (Orientation)orientationVal);

            var positionValue = positionField.GetValue(robot);
            var orientationValue = orientationField.GetValue(robot);

            //Assert
            Assert.Null(positionValue);
            Assert.Null(orientationValue);
            Assert.False(robot.IsPlaced);
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
            var positionField = robot.GetType().GetField("_position", BindingFlags.NonPublic | BindingFlags.Instance);
            var orientationField = robot.GetType().GetField("_orientation", BindingFlags.NonPublic | BindingFlags.Instance);

            //Act
            robot.Place(placementPosition, (Orientation)orientationVal);

            var positionValue = positionField.GetValue(robot);
            var orientationValue = orientationField.GetValue(robot);

            //Assert
            Assert.Null(positionValue);
            Assert.Null(orientationValue);
            Assert.False(robot.IsPlaced);
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
            var positionField = robot.GetType().GetField("_position", BindingFlags.NonPublic | BindingFlags.Instance);
            var orientationField = robot.GetType().GetField("_orientation", BindingFlags.NonPublic | BindingFlags.Instance);
            var isPlacedProperty = robot.GetType().GetProperty("IsPlaced", BindingFlags.Public | BindingFlags.Instance);

            positionField.SetValue(robot, new Position { x = 0, y = 0 });
            orientationField.SetValue(robot, (Orientation)startingOrientation);
            isPlacedProperty.SetValue(robot, true);

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
            var orientationField = robot.GetType().GetField("_orientation", BindingFlags.NonPublic | BindingFlags.Instance);

            //Act
            robot.Rotate((SpinDirection)spin);

            //Assert
            Assert.Null(orientationField.GetValue(robot));
        }
        #endregion

        #region Move tests

        [Theory]
        [InlineData(2, 2, 0, 2, 3)]//Here I'm testing the movement from a single point (2,2) in all 4 directions,
        [InlineData(2, 2, 1, 3, 2)]//and if the Move method is working correctly, the final position coordinates
        [InlineData(2, 2, 2, 2, 1)]//should be as expected.
        [InlineData(2, 2, 3, 1, 2)]
        public void Move_ShouldSucceed_WhenValidMovement(int initX, int initY, int orientation, int expectedX, int expectedY)
        {
            //Arrange
            var mockTable = new Mock<RectangularTable>(5, 5);
            IRobot robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);
            var positionField = robot.GetType().GetField("_position", BindingFlags.NonPublic | BindingFlags.Instance);
            var orientationField = robot.GetType().GetField("_orientation", BindingFlags.NonPublic | BindingFlags.Instance);
            var isPlacedProperty = robot.GetType().GetProperty("IsPlaced",  BindingFlags.Public | BindingFlags.Instance);

            positionField.SetValue(robot, new Position { x = initX, y = initY });
            orientationField.SetValue(robot, (Orientation)orientation);
            isPlacedProperty.SetValue(robot, true);

            //Act
            robot.Move();

            //Assert
            Assert.Equal((Orientation)orientation, orientationField.GetValue(robot));

            //TODO: Implement an overload of the Equals method to simplify these assertions.
            Assert.NotNull(positionField.GetValue(robot));
            Assert.IsType<Position>(positionField.GetValue(robot));
            Assert.Equal(expectedX, (positionField.GetValue(robot) as Position).x);
            Assert.Equal(expectedY, (positionField.GetValue(robot) as Position).y);
        }

        [Theory]
        [InlineData(0, 0, 2)]//Here I'm testing the movement from points around the table edge, pointing outwards,
        [InlineData(0, 0, 3)]//and if the Move method is working correctly, the final position coordinates
        [InlineData(4, 4, 0)]//should remain the same.
        [InlineData(4, 4, 1)]
        public void Move_ShouldDoNothing_WhenInvalidMovement(int initX, int initY, int orientation)
        {
            //Arrange
            var mockTable = new Mock<RectangularTable>(5, 5);
            IRobot robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);
            var positionField = robot.GetType().GetField("_position", BindingFlags.NonPublic | BindingFlags.Instance);
            var orientationField = robot.GetType().GetField("_orientation", BindingFlags.NonPublic | BindingFlags.Instance);

            var initialPosition = new Position { x = initX, y = initY };
            positionField.SetValue(robot, initialPosition);
            orientationField.SetValue(robot, (Orientation)orientation);

            //Act
            robot.Move();

            //Assert
            Assert.Equal((Orientation)orientation, orientationField.GetValue(robot));
            Assert.NotNull(positionField.GetValue(robot));
            Assert.IsType<Position>(positionField.GetValue(robot));
            Assert.Equal(initX, (positionField.GetValue(robot) as Position).x);
            Assert.Equal(initY, (positionField.GetValue(robot) as Position).y);
        }

        [Fact]
        public void Move_ShouldDoNothing_IfNotYetPlaced()
        {
            //Arrange
            var mockTable = new Mock<RectangularTable>(5, 5);
            IRobot robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);
            var positionField = robot.GetType().GetField("_position", BindingFlags.NonPublic | BindingFlags.Instance);
            var orientationField = robot.GetType().GetField("_orientation", BindingFlags.NonPublic | BindingFlags.Instance);

            //Act
            robot.Move();

            //Assert
            Assert.Null(orientationField.GetValue(robot));
            Assert.Null(positionField.GetValue(robot));
        }
        #endregion


        #region GetPosition tests

        [Theory]
        [InlineData(1, 2)]
        [InlineData(0, 4)]
        [InlineData(3, 1)]
        public void GetPosition_ShouldReturnPosition(int x, int y)
        {
            //Arrange
            var mockTable = new Mock<RectangularTable>(5, 5);
            IRobot robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);
            var positionField = robot.GetType().GetField("_position", BindingFlags.NonPublic | BindingFlags.Instance);
            var isPlacedProperty = robot.GetType().GetProperty("IsPlaced", BindingFlags.Public | BindingFlags.Instance);
            positionField.SetValue(robot, new Position(x, y));
            isPlacedProperty.SetValue(robot, true);

            //Act
            var result = robot.GetPosition();

            //Assert
            Assert.IsType<Position>(result);
            Assert.Equal(x, result.x);
            Assert.Equal(y, result.y);
        }

        [Fact]
        public void GetPosition_ShouldReturnNullIfNotPlaced()
        {
            //Arrange
            var mockTable = new Mock<RectangularTable>(5, 5);
            IRobot robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);

            //Act
            var result = robot.GetPosition();

            //Assert
            Assert.Null(result);
        }

        #endregion

        #region GetOrientation tests
        [Theory]
        [InlineData(Orientation.NORTH)]
        [InlineData(Orientation.EAST)]
        [InlineData(Orientation.SOUTH)]
        [InlineData(Orientation.WEST)]
        public void GetOrientation_ShouldReturnOrientation(Orientation orientation)
        {
            //Arrange
            var mockTable = new Mock<RectangularTable>(5, 5);
            IRobot robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);
            var orientationField = robot.GetType().GetField("_orientation", BindingFlags.NonPublic | BindingFlags.Instance);
            var isPlacedProperty = robot.GetType().GetProperty("IsPlaced", BindingFlags.Public | BindingFlags.Instance);
            orientationField.SetValue(robot, orientation);
            isPlacedProperty.SetValue(robot, true);

            //Act
            var result = robot.GetOrientation();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Orientation>(result);
            Assert.Equal(orientation, result);
        }

        [Fact]
        public void GetOrientation_ShouldReturnNullIfNotPlaced()
        {
            //Arrange
            var mockTable = new Mock<RectangularTable>(5, 5);
            IRobot robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);

            //Act
            var result = robot.GetOrientation();

            //Assert
            Assert.Null(result);
        }

        #endregion

        //#region Report tests

        //[Fact]
        //public void Report_ShouldDoNothing_UntilRobotIsPlaced()
        //{
        //    //Arrange
        //    var mockTable = new Mock<RectangularTable>(5, 5);
        //    IRobot robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);
        //    var positionField = robot.GetType().GetField("_position", BindingFlags.NonPublic | BindingFlags.Instance);
        //    var orientationField = robot.GetType().GetField("_orientation", BindingFlags.NonPublic | BindingFlags.Instance);

        //    //Act
        //    robot.Report();

        //    //Assert
        //    Assert.Null(orientationField.GetValue(robot));
        //    Assert.Null(positionField.GetValue(robot));

        //}

        ////[Theory]
        ////[1, 2, 0]
        ////Report_Should
        //#endregion
    }
}
