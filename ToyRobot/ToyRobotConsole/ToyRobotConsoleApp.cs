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
		private readonly IRobotOperator _robotOperator;
		private readonly IReader _reader;

		public ToyRobotConsoleApp(IRobotOperator robotOperator, IReader reader)
		{
			_robotOperator = robotOperator ?? throw new ArgumentNullException(nameof(robotOperator));
			_reader = reader ?? throw new ArgumentNullException(nameof(reader));
		}

		/// <summary>
		/// Reads the user's input and parses it.
		/// </summary>
		/// <param name="instruction">Out parameter: if successful, it will be one of the Instruction enum values.</param>
		/// <param name="args">Possible additional arguments provided by the user.</param>
		/// <returns>True if the instruction provided by the use matches a valid instruction, and the parsing was successful. Else, false.</returns>
		private bool TryParseInput(out Instruction instruction, out object[] args)
		{
			var textInput = _reader.ReadInstruction();
			var distinctWords = textInput.Trim().Split(' ', ',');
			var maybeInstruction = distinctWords[0];
			args = distinctWords.Length > 1 ? distinctWords.Skip(1).ToArray() : null;

			//Enum.TryParse will not only accept strings, but the underlying enum values.
			//Since we don't want to parse e.g. "0" as "PLACE", check that the text matches one of the Instruction enum Names
			//as well as trying to parse it.
			return Enum.TryParse(maybeInstruction, ignoreCase: true, out instruction)
				&& Enum.GetNames(typeof(Instruction)).Contains(maybeInstruction.ToUpper());
		}


		/// <summary>
		/// I've stripped out the logic from the main loop into it's own function to make testing slightly easier.
		/// </summary>
		private void ObeyNextInstruction()
		{
			bool isRecognisedInstruction = TryParseInput(out Instruction instruction, out object[] args);

			if (isRecognisedInstruction)
			{
				//It's not clear what should happen in the event of an invalid instruction or argument
				//I'm going to let the app crash
				_robotOperator.InterpretInstruction(instruction, args);
			}
		}

		/// <summary>
		/// Runs an infinite loop, listening for user input and acting accordingly (if valid).
		/// </summary>
		public void Execute()
		{
			while (true)
			{
				ObeyNextInstruction();
			}
		}
	}
}
