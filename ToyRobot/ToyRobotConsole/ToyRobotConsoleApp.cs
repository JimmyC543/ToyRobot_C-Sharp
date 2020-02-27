using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Robot;
using ToyRobotLibrary.ToyRobotApp;
using ToyRobotLibrary.Utilities;

namespace ToyRobotConsole
{
	/// <summary>
	/// This class will interface with the robot via the console/command line.
	/// </summary>
	public class ToyRobotConsoleApp : IToyRobotApp
	{
		private readonly IRobot _robot;
		private readonly IReporter _reporter;

		public ToyRobotConsoleApp(IRobot robot, IReporter reporter)
		{
			_robot = robot ?? throw new ArgumentNullException(nameof(robot));
			_reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
		}

		public void Execute()
		{
			throw new NotImplementedException();
		}
	}
}
