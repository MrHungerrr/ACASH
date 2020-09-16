using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;

namespace Objects.Organization.ClassRoom.GOAP
{
    public class ClassGOAPContext : GOAPStateStorageList
    {
        public ClassGOAPContext()
        {
            Add("Places_Toilets_Are_Busy", false);
            Add("Places_Sinks_Are_Busy", false);
            Add("Places_Outside_Are_Busy", false);
        }
    }
}

