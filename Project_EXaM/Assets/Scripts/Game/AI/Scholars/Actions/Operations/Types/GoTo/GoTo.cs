using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objects._2D.Places;

namespace AI.Scholars.Actions.Operations.Types
{
    public class GoTo : ScholarOperation
    {
        private readonly Place _place;

        public GoTo(Scholar scholar, Place place) : base(scholar)
        {
            _place = place;
        }

        public override void Execute()
        {
            _scholar.Location.GoTo(_place);
            _scholar.Move.OnDestinationReached += OperationDone;
        }

        public override void Stop()
        {
            _scholar.Move.OnDestinationReached -= OperationDone;
            _scholar.Move.Stop();
        }

        protected override void OperationDone()
        {
            _scholar.Move.OnDestinationReached -= OperationDone;
            _scholar.Sight.SetSightGoal(_place.SightGoal);
            base.OperationDone();
        }

        public override string ToString()
        {
            return $"Go To {_place}";
        }
    }
}
