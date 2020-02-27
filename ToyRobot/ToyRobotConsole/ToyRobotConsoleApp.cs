using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToyRobotConsole.Reader;
using ToyRobotLibrary.Robot;
using ToyRobotLibrary.RobotOperator;
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
		private readonly IReader _reader;
		private readonly IReporter _reporter;

		public ToyRobotConsoleApp(IRobot robot, IReader reader, IReporter reporter)
		{
			_robot = robot ?? throw new ArgumentNullException(nameof(robot));
			_reader = reader ?? throw new ArgumentNullException(nameof(reader));
			_reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
		}

		public void Execute()
		{
			IRobotOperator robotOperator = new RobotOperator(_robot, _reporter);

			while (true)
			{
				//Let's start with dealing only through the console, then generalise
				var textInput = _reader.ReadInstruction();
				var distinctWords = textInput.Trim().Split(' ', ',');
				var maybeInstruction = distinctWords[0];
				IEnumerable<object> args = distinctWords.Length > 1 ? distinctWords.Skip(1) : null;

				//TODO AFTER CRICKET: RE-CODE THIS PROPERLY!

				//Enum.TryParse will not only accept strings, but the underlying enum values.
				//Since we don't want to parse e.g. "0" as "PLACE", check that the text matches one of the Instruction enum Names
				//as well as trying to parse it.
				bool isRecognisedInstruction = Enum.TryParse<Instruction>(maybeInstruction.ToUpper(), out Instruction instruction)
					&& Enum.GetNames(typeof(Instruction)).Contains(maybeInstruction.ToUpper());
				//Instruction instruction = Instruction.valueOf(inputSource.next().toLowerCase());

				if (isRecognisedInstruction)
				{
					robotOperator.InterpretInstruction(instruction, args?.ToArray());
				}
			}
		}
	}
}
