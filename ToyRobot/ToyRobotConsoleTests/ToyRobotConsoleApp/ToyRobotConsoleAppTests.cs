using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToyRobotConsole.Reader;
using ToyRobotLibrary.Robot;
using ToyRobotLibrary.RobotOperator;
using ToyRobotLibrary.ToyRobotApp;
using ToyRobotLibrary.Utilities;
using Xunit;

namespace ToyRobotConsoleTests.ToyRobotConsoleApp
{
    public class ToyRobotConsoleTests
    {
        #region Constructor tests
        [Fact]
        public void Constructor_ShouldSucceed_WhenPassedValidArguments()
        {
            //Arrange
            Mock<IRobotOperator> robotOperator = new Mock<IRobotOperator>();
            Mock<IReader> reader = new Mock<IReader>();
            Mock<IReporter> reporter = new Mock<IReporter>();

            //Act
            var app = new ToyRobotConsole.ToyRobotConsoleApp(robotOperator.Object, reader.Object);

            //Assert
            Assert.NotNull(app);
            Assert.IsAssignableFrom<IToyRobotApp>(app);
            Assert.IsType<ToyRobotConsole.ToyRobotConsoleApp>(app);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenPassedNoRobotOperator()
        {
            //Arrange
            Mock<IReader> reader = new Mock<IReader>();
            Mock<IReporter> reporter = new Mock<IReporter>();
            IToyRobotApp app;
            
            //Act
            Exception ex = Record.Exception(() => app = new ToyRobotConsole.ToyRobotConsoleApp(robotOperator: null, reader.Object));

            //Assert
            Assert.IsType<ArgumentNullException>(ex);
        }
        [Fact]
        public void Constructor_ShouldThrowException_WhenPassedNoReader()
        {
            //Arrange
            Mock<IRobotOperator> robotOperator = new Mock<IRobotOperator>();
            Mock<IReporter> reporter = new Mock<IReporter>();
            IToyRobotApp app;
            
            //Act
            Exception ex = Record.Exception(() => app = new ToyRobotConsole.ToyRobotConsoleApp(robotOperator.Object, reader: null));

            //Assert
            Assert.IsType<ArgumentNullException>(ex);
        }
        #endregion


        #region Execute tests

        [Theory]
        [InlineData("Move")]
        public void Execute_ReadsInstructions_PassesToRobotOperator_WhenValid(params string[] inputs)
        {
            //Arrange
            IEnumerable<string> inputSequence = inputs.AsEnumerable();
            Mock<IRobotOperator> robotOperator = new Mock<IRobotOperator>();
            var reader = new Mock<IReader>();
            var succession = reader.SetupSequence(r => r.ReadInstruction());
            foreach (var instruction in inputs)
            {
                succession = succession.Returns(instruction);
            }
            var app = new ToyRobotConsole.ToyRobotConsoleApp(robotOperator.Object, reader.Object);

            //Act
            app.Execute();

            //Assert
            robotOperator.Verify(ro => ro.InterpretInstruction(It.IsAny<Instruction>(), null), Times.Exactly(inputs.Length));
        }
        #endregion
    }
}
