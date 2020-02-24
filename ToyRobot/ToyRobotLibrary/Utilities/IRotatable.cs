using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotLibrary.Utilities
{
    /// <summary>
    /// We'll want our ToyRobot to be able to rotate.
    /// </summary>
    public interface IRotatable
    {
        public void Rotate(int rotationDirection);
    }
}
