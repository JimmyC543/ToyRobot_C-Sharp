using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotLibrary.Utilities
{
    /// <summary>
    /// I've been struggling to decide whether or not to make this super generic.
    /// For the foreseeable future we'll just be dealing with a Cartesian plane, but
    /// I can imagine a scenario where we'd want to allow for polar (or even cylindrical/spherical!)
    /// coordinate systems.
    /// </summary>
    public class Position
    {
        public int x;
        public int y;
    }

}
