using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;

namespace AI.Scholars.Actions.GOAP
{
    class ScholarGOAPContext : GOAPStateStorage
    {
        public ScholarGOAPContext()
        {
            Add("Items_Have_Note", true);
            Add("Items_Have_Phone", true);
            Add("Items_Have_Calculator", true);
            Add("Location", "Desk");
            Add("Want_Pee", true);
            Add("Want_Wash_Hands", true);
            Add("Want_Rest", true);
        }
    }
}
