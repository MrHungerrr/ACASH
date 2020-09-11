using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Places;

namespace AI.Scholars.Actions.Operations.Types
{
    public class GoToDockStation : GoTo
    {
        public GoToDockStation(Scholar scholar) : base(scholar, scholar.Location.MyDockStation) { }
    }
}
