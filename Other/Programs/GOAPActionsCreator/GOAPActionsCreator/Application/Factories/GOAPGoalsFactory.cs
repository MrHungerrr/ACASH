using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;

namespace Application
{
    public static class GOAPGoalsFactory
    {
        public static void Create()
        {
            GOAPGoalsManager.Instance.Add("Cheat", "Cheat", true);
            GOAPGoalsManager.Instance.Add("Basic", "Basic", "Any");
            GOAPGoalsManager.Instance.Add("End", "End", true);
            GOAPGoalsManager.Instance.Add("Idle", "Idle", true);

            GOAPGoalsManager.Instance.Add("Distraction", "Distraction", "Any");
            GOAPGoalsManager.Instance.Add("Distraction_Rest", "Distraction", "Rest");
            GOAPGoalsManager.Instance.Add("Distraction_Computer", "Distraction", "Computer");
            GOAPGoalsManager.Instance.Add("Distraction_Special", "Distraction", "Special");
        }
    }
}
