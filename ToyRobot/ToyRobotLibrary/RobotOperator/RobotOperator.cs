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

		public void InterpretInstruction(Instruction instruction, object[] args)
		{
			switch (instruction)
			{
				case Instruction.PLACE:
				case Instruction.MOVE:
				case Instruction.LEFT:
				case Instruction.RIGHT:
				case Instruction.REPORT:
					throw new NotImplementedException();
				default:
					return;
			}
		}
	}
}
