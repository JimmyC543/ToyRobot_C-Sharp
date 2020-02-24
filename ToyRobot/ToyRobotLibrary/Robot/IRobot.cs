using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Utilities;

namespace ToyRobotLibrary.Robot
{
	/// <summary>
	/// The interface for any robots our app may deal with.
	/// </summary>
	public interface IRobot : IRotatable, IMobile
	{
		public IRobot Robot();
		//	public IPosition GetPosition();
		//	public IOrientation GetOrientation();
		//	private Orientation _orientation; //TODO: Generalise this; IOrientation would be ideal to better handle more general directions. Also, make nullable!
		public void Rotate(int rotationDirection);
		public void Move();
	}
}
