using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotLibrary.Utilities
{
    /// <summary>
    /// This won't be used by the ToyRobot, but could be handy in case we'd like to implement obsticles or other features in the future?
    /// </summary>
    public interface IPlaceableSansOrientation
    {
        public void Place(Position position);
    }
    /// <summary>
    /// To be used by our ToyRobot
    /// </summary>
    public interface IPlaceableOrientable
    {
        public void Place(Position position, Orientation orientation);
    }
}
