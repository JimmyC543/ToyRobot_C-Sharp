using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotLibrary.Instructions
{
    public interface IInstructionArguments<TArgument> where TArgument : class
    {
        public TArgument Arguments { get; set; }
    }
}
