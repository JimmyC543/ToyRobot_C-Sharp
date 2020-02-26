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
    }
}
