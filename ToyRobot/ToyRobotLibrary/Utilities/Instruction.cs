using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotLibrary.Utilities
{
    /// <summary>
    /// These are the only possible instructions the ToyRobot operator can give, so it makes sense to store them in an enum.
    /// </summary>
    public enum Instruction
    {
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT
    }
}
