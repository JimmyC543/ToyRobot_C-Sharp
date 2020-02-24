using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Utilities;

namespace ToyRobotLibrary.Robot
{
	/// <summary>
	/// The interface for any robots our app may deal with.
	/// </summary>
	public interface IRobot : IRotatable, IMobile, IPlaceableOrientable
	{
		public IRobot Robot();
		//	public IPosition GetPosition();
		//	public IOrientation GetOrientation();
		//	private Orientation _orientation; //TODO: Generalise this; IOrientation would be ideal to better handle more general directions. Also, make nullable!

		//private bool ValidateMovement();

		//I'm not sure what the IReport would contain, but It makes sense to have some method which returns the appropriate info.
		public IReport Report();
	}
}
