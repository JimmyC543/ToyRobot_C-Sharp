using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Utilities;

namespace ToyRobotConsole.Reporter
{
    public class ConsoleReporter : IReporter
    {
        public void Report(Position position, Orientation orientation)
        {
            Console.WriteLine($"{position.x},{position.y},{orientation}");
        }
    }
}
