using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotLibrary.Instructions;
using ToyRobotLibrary.Utilities;

namespace ToyRobotLibrary.RobotOperator
{
    public interface IRobotOperator
    {
        //public void InterpretInstruction(Instruction instruction, object[] args = null);
        public void InterpretInstruction(Instruction instruction, IInstructionArguments<object> args = null);
    }
}
