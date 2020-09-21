using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Scholars.Actions.Operations.Types
{
    public class UseItem : Wait
    {

        public UseItem(Scholar scholar, float timeToHold) : base(scholar, timeToHold)
        {

        }


        public override void Stop()
        {
            _scholar.Items.Put();
            base.Stop();
        }


        public override string ToString()
        {
            return $"Hold Item For {_timeToWait}";
        }
    }
}
