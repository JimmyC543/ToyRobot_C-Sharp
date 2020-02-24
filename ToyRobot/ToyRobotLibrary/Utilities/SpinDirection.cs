using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotLibrary.Utilities
{
	/// <summary>
	/// Set left and right to be -1 and 1 respectively. This way we can use the Direction values to iterate through the Orientations.
	/// </summary>
	public enum SpinDirection
	{
		Left = -1,	//I.e. Anti-clockwise (looking down)
		Right = 1	//I.e. Clockwise (looking down)
	}
}
