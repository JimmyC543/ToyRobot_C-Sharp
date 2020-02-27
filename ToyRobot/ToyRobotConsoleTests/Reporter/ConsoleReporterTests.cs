using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ToyRobotConsole.Reporter;
using ToyRobotLibrary.Utilities;
using Xunit;

namespace ToyRobotConsoleTests.Reporter
{
    public class ConsoleReporterTests
    {

        [Theory]
        [InlineData(0, 1, Orientation.North, "0,1,NORTH")]
        [InlineData(0, 0, Orientation.West, "0,0,WEST")]
        [InlineData(3, 3, Orientation.North, "3,3,NORTH")]
        public void ConsoleReporter_ShouldFormatOutputCorrectly(int x, int y, Orientation orientation, string expected)
        {
            //Arrange
            var position = new Position(x, y);

            //Change the output of the Console so we can analyse the result of Console.WriteLine
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            IReporter reporter = new ConsoleReporter();

            //Act
            reporter.Report(position, orientation);

            //Assert
            Assert.Equal(expected + Environment.NewLine, consoleOutput.ToString());
        }
    }
}
