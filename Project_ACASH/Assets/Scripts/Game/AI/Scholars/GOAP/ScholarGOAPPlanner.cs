using AI.Scholars;
using AI.Scholars.Actions;
using AI.Scholars.Actions.Operations;
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
        public enum Goals
        {
            Cheat,
            Basic,
            End,
            Idle,
            Distraction
        }

        private List<IGOAPReadOnlyAction> GetGOAPPlan(Scholar scholar, string goalKey)
        {
            var context = new GOAPStateContext(scholar.GoapContext, scholar.ClassRoom.GoapContext);
            var comparer = new BaseCostComparer();
            var planner = new GOAPPlanner(context, comparer);

            List<IGOAPReadOnlyAction> plan;

            if (!planner.TryGetBestPlan(GOAPGoalsManager.Instance.Goals[goalKey], out plan))
                throw new Exception($"{scholar} не нашел план для цели \"{goalKey}\"");

            return plan;
        }


        public ScholarAction GetPlan(Scholar scholar, string goalKey)
        {
            var actionsConstructor = new ScholarActionsConstructor(scholar);
            var operations = new List<IScholarOperation>();
            var plan = GetGOAPPlan(scholar, goalKey);

            foreach(var action in plan)
            {
                operations.AddRange(actionsConstructor.Create(action.Name));
            }

            return new ScholarAction(operations);
        }
    }
}
