using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Robot;
using ToyRobotLibrary.Utilities;

namespace ToyRobotLibrary.RobotOperator
{
    public class RobotOperator : IRobotOperator
    {
        private readonly IRobot _robot;
        private readonly IReporter _reporter;
        public RobotOperator(IRobot robot, IReporter reporter)
        {
            _robot = robot ?? throw new ArgumentNullException(nameof(robot), "Must provide or inject object implementing IRobot");
			_reporter = reporter ?? throw new ArgumentNullException(nameof(reporter), "Must provide or inject object implementing IReporter");
        }

		//TODO: the "object[] args" parameter feels super gross and wrong. Needs to be changed at some point.
		//Perhaps declaring some sort of IInstructionArguments (generic?) would make more sense and simplify
		//the code in this method.
		//REFACTOR: It works for now, though, so I'll (hopefully) have time to come back to this.
		public void InterpretInstruction(Instruction instruction, object[] args)
		{
			switch (instruction)
			{
				case Instruction.PLACE:

					if (args == null)
						throw new ArgumentNullException(nameof(args), "Place command requires 3 arguments.");
					if (args.Length != 3)
						throw new ArgumentException($"Expected 3 arguments; instead received {args.Length}", nameof(args));

					if (!int.TryParse(args[0] as string, out var xArg) || !int.TryParse(args[1] as string, out var yArg))
						throw new ArgumentException($"Invalid position arguments.");
					if ((args[2] as string) == null)
						throw new ArgumentException($"Expected a string for argument 3.");

					//TODO: I'm not a fan of seing the "new" keyword like this. Consider creating a PositionFactory to create positions (or IPositions?) for us
					_robot.Place(new Position(xArg, yArg), Enum.Parse<Orientation>((string)args[2], ignoreCase: true));
					break;
				case Instruction.MOVE:
					if (_robot.IsPlaced)
					{
						_robot.Move();
					}
					break;
				case Instruction.LEFT:
					if (_robot.IsPlaced)
					{
						_robot.Rotate(SpinDirection.LEFT);
					}
					break;
				case Instruction.RIGHT:
					if (_robot.IsPlaced)
					{
						_robot.Rotate(SpinDirection.RIGHT);
					}
					break;
				case Instruction.REPORT:
					if (_robot.IsPlaced)
					{
						_reporter.Report(_robot.GetPosition(), _robot.GetOrientation().Value);
					}
					break;
				default:
					return;
			}
		}
	}
}
