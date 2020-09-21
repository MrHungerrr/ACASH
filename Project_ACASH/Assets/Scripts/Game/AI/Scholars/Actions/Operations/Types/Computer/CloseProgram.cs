using System;
using AI.Scholars.Computer;
using AI.Scholars.Items;
using GameTime;
using GameTime.Action;
using UnityEngine;

namespace AI.Scholars.Actions.Operations.Types
{
    public class CloseProgram : ScholarOperation
    {

        public CloseProgram(Scholar scholar): base(scholar)
        {
        }

        public override void Execute()
        {
            _scholar.Computer.ResetProgram();
            OperationDone();
        }

        public override void Stop() { }

        public override string ToString()
        {
            return $"Close Current Program";
        }
    }
}
