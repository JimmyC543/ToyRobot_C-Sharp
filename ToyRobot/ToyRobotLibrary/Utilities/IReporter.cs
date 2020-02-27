using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotLibrary.Utilities
{
    /// <summary>
    /// Classes implementing this interface will be dealing with the output of our application,
    /// whether it's the console/command line, an output file, or even a web api.
    /// </summary>
    public interface IReporter
    {
        public void Report(Position position, Orientation orientation);
    }
}
