using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ToyRobotConsole.Reader;
using Xunit;

namespace ToyRobotConsoleTests.Reader
{
    public class ConsoleReaderTests
    {
        [Theory]
        [InlineData("place 1 3 South")]
        [InlineData("MOVE")]
        [InlineData("Left")]
        public void ConsoleReader_ShouldReturnCorrectly(string inputText)
        {
            //Arrange

            //Change the input of the Console so we can analyse the result of Console.WriteLine
            var consoleInput = new StringReader(inputText);
            Console.SetIn(consoleInput);
            IReader reader = new ConsoleReader();

            //Act
            var result = reader.ReadInstruction();

            //Assert
            Assert.Equal(inputText, result);

        }
    }
}
