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
        public bool IsPlaced { get; }//TODO: Perhaps add another small interface to remove duplication between here and IPlaceableOrientable
    }
    /// <summary>
    /// To be used by our ToyRobot
    /// </summary>
    public interface IPlaceableOrientable
    {
        public void Place(Position position, Orientation orientation);

        //It's conceivable that in the future we might want to remove/re-place the robot onto a different table.
        //With a publically accessible property it makes it easer to determine the state of the robot.
        public bool IsPlaced { get; }//TODO: Perhaps add another small interface to remove duplication between here and IPlaceableSansOrientation
    }
}
