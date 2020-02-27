using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotConsole.Reader
{
    public class ConsoleReader : IReader
    {
        public string ReadInstruction()
        {
            return Console.ReadLine();
        }
    }
}
