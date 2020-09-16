using GOAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.AI.Scholars.Actions.GOAP
{
    class GlobalGOAPContext : GOAPStateStorage
    {
        public GlobalGOAPContext()
        {
            Add("Places_Toilets_Are_Busy", false);
            Add("Places_Sinks_Are_Busy", false);
            Add("Places_Outside_Are_Busy", false);
        }
    }
}
