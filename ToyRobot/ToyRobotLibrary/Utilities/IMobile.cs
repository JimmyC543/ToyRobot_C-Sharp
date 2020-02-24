using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotLibrary.Utilities
{
    /// <summary>
    /// We'll want our ToyRobot to move. But maybe we want other things to move too? ¯\_(ツ)_/¯
    /// </summary>
    public interface IMobile
    {
        public void Move();
    }
}
