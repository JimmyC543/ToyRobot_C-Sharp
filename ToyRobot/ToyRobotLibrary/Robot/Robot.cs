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
        private Position _position;
        private Orientation? _orientation;

        public bool IsPlaced { get; internal set; }

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

        public Orientation? GetOrientation()
        {
            return IsPlaced ? _orientation : null; //TODO: Better to return null, or to throw exception to be handled elsewhere?
        }

        public Position GetPosition()
        {
            return IsPlaced ? _position : null; //TODO: Better to return null, or to throw exception to be handled elsewhere?
        }

        public void Move()
        {
            if (!IsPlaced) return; //If the robot hasn't been placed, the Move command should do nothing.

            //TODO: This feels a little funny. Consider refactoring later.
            Position updatedPosition = new Position(_position);

            switch (_orientation)
            {
                case Orientation.NORTH:
                    updatedPosition.y++;
                    break;
                case Orientation.EAST:
                    updatedPosition.x++;
                    break;
                case Orientation.SOUTH:
                    updatedPosition.y--;
                    break;
                case Orientation.WEST:
                    updatedPosition.x--;
                    break;
            }

            if (_table.IsValidPosition(updatedPosition))
            {
                _position = updatedPosition;
            }
        }
        public void Place(Position position, Orientation orientation)
        {
            if (_table.IsValidPosition(position) && Enum.IsDefined(typeof(Orientation), orientation))
            {
                _position = position;
                _orientation = orientation;
                IsPlaced = true;
            }
        }

        public void Rotate(SpinDirection rotationDirection)
        {
            if(IsPlaced)//I.e.if the robot has been placed on a table
            {
                //Futures: If any more orientations are added e.g. North-East,
                //"Enum.GetValues(typeof(Orientation)).Length" will give the number of options.
                int numOrientations = 4;
                _orientation = (Orientation)(((int)_orientation.Value + (int)rotationDirection + numOrientations) % numOrientations);
            }
        }

    }
}
