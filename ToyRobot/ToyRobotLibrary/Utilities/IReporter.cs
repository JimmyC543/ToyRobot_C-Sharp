using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotLibrary.Utilities
{
    public interface IReporter
    {
        public void Report(Position position, Orientation orientation);
    }
}
