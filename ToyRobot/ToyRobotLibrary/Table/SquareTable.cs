using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Utilities;

namespace ToyRobotLibrary.Table
{
	/// <summary>
	/// This is more accurate than using a Rectangular table.
	/// </summary>
    public class SquareTable : RectangularTable
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sideLength">the size of the table's width and length, which we can feed into the base class' two constructor arguments.</param>
		public SquareTable(int sideLength) : base(sideLength, sideLength)
		{
		}
	}
}
