using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Robot;
using ToyRobotLibrary.Table;
using Xunit;

namespace ToyRobotLibraryTests.Robot
{
    public class RobotTests
    {
        #region Constructor tests

        [Fact]
        public void Constructor_ShouldSucceed_WhenPassedValidTable()
        {
            var mockTable = new Mock<RectangularTable>(1, 1);
            IRobot robot = new ToyRobotLibrary.Robot.Robot(mockTable.Object);
            Assert.NotNull(robot);
            Assert.IsAssignableFrom<IRobot>(robot);
            Assert.IsType<ToyRobotLibrary.Robot.Robot>(robot);
        }
        [Fact]
        public void Constructor_ShouldThrowException_WhenPassedInvalidTable()
        {
            IRobot robot;
            Exception ex = Record.Exception(() => robot = new ToyRobotLibrary.Robot.Robot(null));
            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Contains("Can't use a robot without a table instance.", ex.Message);
        }

        #endregion

    }
}
