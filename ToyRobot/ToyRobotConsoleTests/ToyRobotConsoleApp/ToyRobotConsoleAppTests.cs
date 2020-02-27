using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ToyRobotConsole.Reader;
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
        [InlineData("Left", "Right")]
        [InlineData("Place 1,2,East", "Move", "Right", "Report")]
        [InlineData("PLACE 0,0,NORTH", "Move", "Report")]
        public void Execute_ReadsInstructions_PassesToRobotOperator_WhenValid(params string[] inputs)
        {
            //Arrange
            IEnumerable<string> inputSequence = inputs.AsEnumerable();
            Mock<IRobotOperator> robotOperator = new Mock<IRobotOperator>();
            var reader = new Mock<IReader>();

            //Setup the mock response of "ReadInstruction": each time it's called, iterate through the inputs to mimic a set of user instructions.
            var succession = reader.SetupSequence(r => r.ReadInstruction());
            foreach (var instruction in inputs)
            {
                succession = succession.Returns(instruction);
            }
            var app = new ToyRobotConsole.ToyRobotConsoleApp(robotOperator.Object, reader.Object);

            //Because Execute runs an infine loop, I'm testing on the logic inside each loop.
            var method = app.GetType().GetMethod("ObeyNextInstruction", BindingFlags.NonPublic | BindingFlags.Instance);

            //Act
            for (var i = 0; i < inputs.Length; i++)
            {
                method.Invoke(app, null);
            }

            //Assert
            robotOperator.Verify(ro => ro.InterpretInstruction(It.IsAny<Instruction>(), It.IsAny<string[]>()), Times.Exactly(inputs.Length));
        }
        #endregion
    }
}
