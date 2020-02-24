using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotLibrary.Utilities
{
    /// <summary>
    /// The report should contain the position of the robot, plus the direction it's facing (orientation)
    /// </summary>
    public interface IReport
    {
        public Position Position { get; set; }
        public Orientation Orientation { get; set; }
        public string ProduceReport();
    }
}
