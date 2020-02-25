using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Table;
using ToyRobotLibrary.Utilities;

namespace ToyRobotLibrary.Robot
{
    public class Robot : IRobot
    {
        private readonly ITable _table;

        /// <summary>
        /// The Robot which we're going to be playing with.
        /// </summary>
        /// <param name="table">The table the robot is operating on. Using IoC will make it easier for Dependency Injection later on.</param>
        public Robot(ITable table)
        {
            //Futures: It may be foreseeable to allow for "picking up" the robot and placing it on different tables.
            //In this case, the constructor may need to be refactored, and another way for setting the table will need to
            //be devised.
            _table = table ?? throw new ArgumentNullException(nameof(table), "Can't use a robot without a table instance.");
        }

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

    }
}
