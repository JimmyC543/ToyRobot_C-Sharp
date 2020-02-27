using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotLibrary.Instructions
{
    public class PlaceInstructionArguments : IInstructionArguments<PlaceArguments>
    {
        public PlaceArguments Arguments { get; set; }
    }
}
