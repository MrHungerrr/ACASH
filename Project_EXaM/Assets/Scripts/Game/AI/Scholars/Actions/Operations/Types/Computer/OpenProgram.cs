using System;
using AI.Scholars.Computer;
using AI.Scholars.Items;
using GameTime;
using GameTime.Action;
using UnityEngine;

namespace AI.Scholars.Actions.Operations.Types
{
    public class OpenProgram : ScholarOperation
    {
        private readonly ScholarComputer.Program _program;

        public OpenProgram(Scholar scholar, ScholarComputer.Program program): base(scholar)
        {
            _program = program;
        }

        public override void Execute()
        {
            _scholar.Computer.SetProgram(_program);
            OperationDone();
        }

        public override void Stop() { }

        public override string ToString()
        {
            return $"Open Program {_program}";
        }
    }
}
