using System;
using AI.Scholars.Items;
using GameTime;
using GameTime.Action;
using UnityEngine;

namespace AI.Scholars.Actions.Operations.Types
{
    public class Put : ScholarOperation
    {
        public Put(Scholar scholar): base(scholar)
        {
        }

        public override void Execute()
        {
            _scholar.Items.Put();
            OperationDone();
        }

        public override void Stop()
        {
        }

        public override string ToString()
        {
            return $"Put some item";
        }
    }
}
