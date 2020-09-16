using AI.Scholars;
using GOAP;
using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vkimow.Tools.Single;

namespace AI.Scholars.GOAP
{
    public class ScholarGOAPPlanner : Singleton<ScholarGOAPPlanner>
    {
        private List<GOAPAction> GetGOAPPlan(Scholar scholar, string goalKey)
        {
            var context = new GOAPStateContext(scholar.GoapContext, scholar.ClassRoom.GoapContext);
            var comparer = new BaseCostComparer();
            var planner = new GOAPPlanner(context, comparer);

            List<GOAPAction> plan;

            if (!planner.TryGetBestPlan(GOAPGoalsManager.Instance.Goals[goalKey], out plan))
                throw new Exception($"{scholar} не нашел план для цели \"{goalKey}\"");

            return plan;
        }
    }
}
