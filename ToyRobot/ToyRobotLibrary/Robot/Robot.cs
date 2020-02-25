using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Utilities;

namespace ToyRobotLibrary.Robot
{
    public class Robot : IRobot
    {
        public void Move()
        {
            throw new NotImplementedException();
        }

        public void Place(Position position, Orientation orientation)
        {
            throw new NotImplementedException();
        }

        public IReport Report()
        {
            throw new NotImplementedException();
        }

        public void Rotate(SpinDirection rotationDirection)
        {
            throw new NotImplementedException();
        }

        IRobot IRobot.Robot()
        {
            throw new NotImplementedException();
        }
    }
}
