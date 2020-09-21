using System;
using AI.Scholars.Computer;
using AI.Scholars.Items;
using GameTime;
using GameTime.Action;
using UnityEngine;

namespace AI.Scholars.Actions.Operations.Types
{
    public class UseProgram : Wait
    {
        public UseProgram(Scholar scholar, float timeToUse): base(scholar, timeToUse)
        {

        }

        public override void Stop() 
        {
            _scholar.Computer.ResetProgram();
            base.Stop();
        }

        public override string ToString()
        {
            return $"Using Current Program For {_timeToWait}";
        }
    }
}
