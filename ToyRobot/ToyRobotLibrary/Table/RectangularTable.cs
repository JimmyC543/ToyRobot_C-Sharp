using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Utilities;

namespace ToyRobotLibrary.Table
{
	/// <summary>
	/// Although the project asks for a 5x5 dimension table, it seems worth creating a more general table which doesn't necessitate equal side lengths.
	/// I'll probably extend from this to create a SquareTable.
	/// </summary>
	public class RectangularTable : ITable
	{
		private int _numRows;
		private int _numCols;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="numRows">the upper bound of the y axis</param>
		/// <param name="numCols">the upper bound of the x axis</param>
		public RectangularTable(int numRows, int numCols)
		{
			if (numRows <= 0 || numCols <= 0)
				throw new ArgumentException();
			this._numRows = numRows;
			this._numCols = numCols;
		}

		public bool IsValidPosition(Position position)
		{
			throw new NotImplementedException();
		}
	}
}
