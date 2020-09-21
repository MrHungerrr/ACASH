using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objects._2D.Places;

namespace AI.Scholars.Actions.Operations.Types
{
    public class GoToDockStation : GoTo
    {
        public GoToDockStation(Scholar scholar) : base(scholar, scholar.Location.MyDockStation) { }
    }
}
