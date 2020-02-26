using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Robot;
using ToyRobotLibrary.RobotOperator;
using ToyRobotLibrary.Table;
using ToyRobotLibrary.Utilities;
using Xunit;

namespace ToyRobotLibraryTests.RobotOperator
{
    public class RobotOperatorTests
    {

        #region Constructor tests

        [Fact]
        public void RobotOperator_ShouldSucceed_WhenPassedValidArguments()
        {
            //Arrange
            Mock<IRobot> mockRobot = new Mock<IRobot>();
            Mock<IReporter> mockReporter = new Mock<IReporter>();

            //Act
            IRobotOperator robotOperator = new ToyRobotLibrary.RobotOperator.RobotOperator(mockRobot.Object, mockReporter.Object);

            //Assert
            Assert.NotNull(robotOperator);
            Assert.IsAssignableFrom<IRobotOperator>(robotOperator);
            Assert.IsType<ToyRobotLibrary.RobotOperator.RobotOperator>(robotOperator);
        }

        [Fact]
        public void RobotOperator_ShouldThrowException_WhenPassedNullRobot()
        {
            //Arrange
            IRobotOperator robotOperator;
            Mock<IReporter> mockReporter = new Mock<IReporter>();
            //Act
            var ex = Record.Exception(() => robotOperator = new ToyRobotLibrary.RobotOperator.RobotOperator(null, mockReporter.Object));

            //Assert
            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Contains("Must provide or inject object implementing IRobot", ex.Message);
        }

        [Fact]
        public void RobotOperator_ShouldThrowException_WhenPassedNullReporter()
        {
            //Arrange
            IRobotOperator robotOperator;
            Mock<IRobot> mockRobot = new Mock<IRobot>();
            //Act
            var ex = Record.Exception(() => robotOperator = new ToyRobotLibrary.RobotOperator.RobotOperator(mockRobot.Object, null));

            //Assert
            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Contains("Must provide or inject object implementing IReporter", ex.Message);
        }
        #endregion


        #region InterpretInstruction tests - MOVE
        [Fact]
        public void InterpretInstruction_MOVE_ShouldBeCalled_WhenRobotIsPlaced()
        {
            //Arrange
            Instruction instruction = Instruction.MOVE;
            Mock<IRobot> mockRobot = new Mock<IRobot>();
            mockRobot.Setup(robot => robot.IsPlaced).Returns(true);
            Mock<IReporter> mockReporter = new Mock<IReporter>();
            IRobotOperator robotOperator = new ToyRobotLibrary.RobotOperator.RobotOperator(mockRobot.Object, mockReporter.Object);

            //Act
            robotOperator.InterpretInstruction(instruction);

            //Assert
            mockRobot.Verify(robot => robot.Move(), Times.Once());

        }
        [Fact]
        public void InterpretInstruction_MOVE_ShouldNotBeCalled_WhenRobotIsNotPlaced()
        {
            //Arrange
            Instruction instruction = Instruction.MOVE;
            Mock<IRobot> mockRobot = new Mock<IRobot>();
            mockRobot.Setup(robot => robot.IsPlaced).Returns(false);
            Mock<IReporter> mockReporter = new Mock<IReporter>();
            IRobotOperator robotOperator = new ToyRobotLibrary.RobotOperator.RobotOperator(mockRobot.Object, mockReporter.Object);

            //Act
            robotOperator.InterpretInstruction(instruction);

            //Assert
            mockRobot.Verify(robot => robot.Move(), Times.Never());
        }
        #endregion

        #region InterpretInstruction tests - LEFT
        [Fact]
        public void InterpretInstruction_LEFT_ShouldBeCalled_WhenRobotIsPlaced()
        {
            //Arrange
            Instruction instruction = Instruction.LEFT;
            Mock<IRobot> mockRobot = new Mock<IRobot>();
            mockRobot.Setup(robot => robot.IsPlaced).Returns(true);
            Mock<IReporter> mockReporter = new Mock<IReporter>();
            IRobotOperator robotOperator = new ToyRobotLibrary.RobotOperator.RobotOperator(mockRobot.Object, mockReporter.Object);

            //Act
            robotOperator.InterpretInstruction(instruction);

            //Assert
            mockRobot.Verify(robot => robot.IsPlaced, Times.Once());
            mockRobot.Verify(robot => robot.Rotate(SpinDirection.Left), Times.Once());

        }
        [Fact]
        public void InterpretInstruction_LEFT_ShouldNotBeCalled_WhenRobotIsNotPlaced()
        {
            //Arrange
            Instruction instruction = Instruction.LEFT;
            Mock<IRobot> mockRobot = new Mock<IRobot>();
            mockRobot.Setup(robot => robot.IsPlaced).Returns(false);
            Mock<IReporter> mockReporter = new Mock<IReporter>();
            IRobotOperator robotOperator = new ToyRobotLibrary.RobotOperator.RobotOperator(mockRobot.Object, mockReporter.Object);

            //Act
            robotOperator.InterpretInstruction(instruction);

            //Assert
            mockRobot.Verify(robot => robot.IsPlaced, Times.Once());
            mockRobot.Verify(robot => robot.Rotate(It.IsAny<SpinDirection>()), Times.Never());
        }
        #endregion

        #region InterpretInstruction tests - RIGHT
        [Fact]
        public void InterpretInstruction_RIGHT_ShouldBeCalled_WhenRobotIsPlaced()
        {
            //Arrange
            Instruction instruction = Instruction.RIGHT;
            Mock<IRobot> mockRobot = new Mock<IRobot>();
            mockRobot.Setup(robot => robot.IsPlaced).Returns(true);
            Mock<IReporter> mockReporter = new Mock<IReporter>();
            IRobotOperator robotOperator = new ToyRobotLibrary.RobotOperator.RobotOperator(mockRobot.Object, mockReporter.Object);

            //Act
            robotOperator.InterpretInstruction(instruction);

            //Assert
            mockRobot.Verify(robot => robot.IsPlaced, Times.Once());
            mockRobot.Verify(robot => robot.Rotate(SpinDirection.Right), Times.Once());

        }
        [Fact]
        public void InterpretInstruction_RIGHT_ShouldNotBeCalled_WhenRobotIsNotPlaced()
        {
            //Arrange
            Instruction instruction = Instruction.RIGHT;
            Mock<IRobot> mockRobot = new Mock<IRobot>();
            mockRobot.Setup(robot => robot.IsPlaced).Returns(false);
            Mock<IReporter> mockReporter = new Mock<IReporter>();
            IRobotOperator robotOperator = new ToyRobotLibrary.RobotOperator.RobotOperator(mockRobot.Object, mockReporter.Object);

            //Act
            robotOperator.InterpretInstruction(instruction);

            //Assert
            mockRobot.Verify(robot => robot.IsPlaced, Times.Once());
            mockRobot.Verify(robot => robot.Rotate(It.IsAny<SpinDirection>()), Times.Never());
        }
        #endregion

        #region InterpretInstruction tests - REPORT
        [Fact]
        public void InterpretInstruction_REPORT_ShouldBeCalled_WhenRobotIsPlaced()
        {
            //Arrange
            Instruction instruction = Instruction.REPORT;
            Mock<IRobot> mockRobot = new Mock<IRobot>();
            mockRobot.Setup(robot => robot.GetOrientation()).Returns(Orientation.North);
            mockRobot.Setup(robot => robot.IsPlaced).Returns(true);
            Mock<IReporter> mockReporter = new Mock<IReporter>();
            IRobotOperator robotOperator = new ToyRobotLibrary.RobotOperator.RobotOperator(mockRobot.Object, mockReporter.Object);

            //Act
            robotOperator.InterpretInstruction(instruction);

            //Assert
            mockRobot.Verify(robot => robot.IsPlaced, Times.Once());
            mockRobot.Verify(robot => robot.GetPosition(), Times.Once());
            mockRobot.Verify(robot => robot.GetOrientation(), Times.Once());
            mockReporter.Verify(reporter => reporter.Report(It.IsAny<Position>(), It.IsAny<Orientation>()), Times.Once());
        }
        [Fact]
        public void InterpretInstruction_REPORT_ShouldNotBeCalled_WhenRobotIsNotPlaced()
        {
            //Arrange
            Instruction instruction = Instruction.RIGHT;
            Mock<IRobot> mockRobot = new Mock<IRobot>();
            mockRobot.Setup(robot => robot.IsPlaced).Returns(false);
            Mock<IReporter> mockReporter = new Mock<IReporter>();
            IRobotOperator robotOperator = new ToyRobotLibrary.RobotOperator.RobotOperator(mockRobot.Object, mockReporter.Object);

            //Act
            robotOperator.InterpretInstruction(instruction);

            //Assert
            mockRobot.Verify(robot => robot.Rotate(SpinDirection.Right), Times.Never());
        }
        #endregion


        #region InterpretInstruction tests - PLACE

        [Theory]
        [InlineData(0, 0, Orientation.North)]
        [InlineData(2, 3, Orientation.East)]
        [InlineData(-1, -1, Orientation.East)]//Even invalid Positions work
        [InlineData(-1, -1, (Orientation)7)]//Even invalid Orientations work
        public void InterpretInstruction_Place_ShouldBeCalled_WithCorrectNumberOfArgs(int x, int y, Orientation orientation)
        {
            //Arrange
            Instruction instruction = Instruction.PLACE;
            Mock<IRobot> mockRobot = new Mock<IRobot>();
            mockRobot.Setup(robot => robot.IsPlaced).Returns(true);
            Mock<IReporter> mockReporter = new Mock<IReporter>();
            IRobotOperator robotOperator = new ToyRobotLibrary.RobotOperator.RobotOperator(mockRobot.Object, mockReporter.Object);
            string orientationString = orientation.ToString();
            //Act
            robotOperator.InterpretInstruction(instruction, new object[] { x, y, orientation.ToString() });

            //Assert
            mockRobot.Verify(robot => robot.Place(It.IsAny<Position>(), orientation), Times.Once());
        }

        [Fact]
        public void InterpretInstruction_Place_ThrowsException_WithNoArgs()
        {
            //Arrange
            Instruction instruction = Instruction.PLACE;
            Mock<IRobot> mockRobot = new Mock<IRobot>();
            mockRobot.Setup(robot => robot.IsPlaced).Returns(true);
            Mock<IReporter> mockReporter = new Mock<IReporter>();
            IRobotOperator robotOperator = new ToyRobotLibrary.RobotOperator.RobotOperator(mockRobot.Object, mockReporter.Object);
            //Act
            Exception ex = Record.Exception(() => robotOperator.InterpretInstruction(instruction));

            //Assert
            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Contains("Place command requires 3 arguments.", ex.Message);
        }

        [Fact]
        public void InterpretInstruction_Place_ThrowsException_WithIncorrectNumberOfArgs()
        {
            //Arrange
            Instruction instruction = Instruction.PLACE;
            Mock<IRobot> mockRobot = new Mock<IRobot>();
            mockRobot.Setup(robot => robot.IsPlaced).Returns(true);
            Mock<IReporter> mockReporter = new Mock<IReporter>();
            IRobotOperator robotOperator = new ToyRobotLibrary.RobotOperator.RobotOperator(mockRobot.Object, mockReporter.Object);
            //Act
            Exception ex = Record.Exception(() => robotOperator.InterpretInstruction(instruction, new object[] { "Just the one argument here, buddy." }));

            //Assert
            Assert.NotNull(ex);
            Assert.IsType<ArgumentException>(ex);
            Assert.Contains("Expected 3 arguments; instead received 1", ex.Message);
        }
        #endregion
    }
}
