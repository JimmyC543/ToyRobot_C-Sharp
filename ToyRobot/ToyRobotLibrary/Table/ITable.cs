using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Utilities;

namespace ToyRobotLibrary.Table
{
    /// <summary>
    /// I'm using an interface here because it's conceivable that the ToyRobot could be used on different table shapes.
    /// For this project we're starting out with a 5x5 cartesian grid, however we may want to expand on this and handle
    /// other dimensions of Cartesian grids, or a different shape/coordinate system entirely (e.g. a flat circular tabletop)  
    /// </summary>
    public interface ITable
    {
        public bool IsValidPosition(Position position);
    }
}
