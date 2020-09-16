using Objects.Organization.Places;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace AI.Scholars.Actions.Operations.Types
{
    public class LookAt : ScholarOperation
    {
        private readonly Vector2 _sightTarget;

        public LookAt(Scholar scholar, Vector2 sightTarget) : base(scholar)
        {
            _sightTarget = sightTarget;
        }


        public override void Execute()
        {
            _scholar.Sight.SetSightGoal(_sightTarget);
            OperationDone();
        }

        public override void Stop()
        {
        }

        public override string ToString()
        {
            return $"Look At";
        }
    }
}
