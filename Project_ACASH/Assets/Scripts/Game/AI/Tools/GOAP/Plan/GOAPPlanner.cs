using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GOAP.Cost;
using Vkimow.Structures.Trees.Decision;
using Vkimow.Tools.Extensions;
using Vkimow.Tools.Math;


namespace GOAP
{
    public class GOAPPlanner
    {
        private readonly GOAPPlanBuilder _builder;
        private readonly IGOAPCostComparer _comparer;

        public GOAPPlanner(GOAPStateContext context, IGOAPCostComparer comparer)
        {
            _builder = new GOAPPlanBuilder(context);
            _comparer = comparer;
        }

        public bool TryGetBestPlan(KeyValuePair<string, GOAPState> goal, out List<IGOAPReadOnlyAction> plan)
        {
            TreeDecision<IGOAPReadOnlyAction> treePlans;
            plan = null;

            if (!_builder.TryBuildPlans(goal, out treePlans))
                return false;

            plan = GetSingleBestPlan(treePlans);

            if (plan == null || plan.Count == 0)
                return false;

            return true;
        }

        public bool TryGetAllBestPlans(KeyValuePair<string, GOAPState> goal, out List<List<IGOAPReadOnlyAction>> plans)
        {
            TreeDecision<IGOAPReadOnlyAction> treePlans;
            plans = null;

            if (!_builder.TryBuildPlans(goal, out treePlans))
                return false;

            plans = GetAllBestPlans(treePlans);

            if (plans.Count == 0)
                return false;

            return true;
        }

        public bool TryGetAllPlans(KeyValuePair<string, GOAPState> goal, out List<List<IGOAPReadOnlyAction>> plans)
        {
            TreeDecision<IGOAPReadOnlyAction> treePlans;
            plans = null;

            if (!_builder.TryBuildPlans(goal, out treePlans))
                return false;

            plans = GetAllPlans(treePlans);

            if (plans.Count == 0)
                return false;

            return true;
        }


        private List<List<IGOAPReadOnlyAction>> GetAllPlans(TreeDecision<IGOAPReadOnlyAction> plans)
        {
            var currentPlan = new List<IGOAPReadOnlyAction>();
            var allPlans = new List<List<IGOAPReadOnlyAction>>();

            foreach (var child in plans.Root.Children)
            {
                Iterate(child);
            }

            void Iterate(TreeElement<IGOAPReadOnlyAction> element)
            {
                currentPlan.Add(element.Content);

                if (!element.HasChildren)
                {
                    allPlans.Add(new List<IGOAPReadOnlyAction>(currentPlan));
                    currentPlan.RemoveAt(currentPlan.Count - 1);
                    return;
                }

                foreach (var child in element.Children)
                {
                    Iterate(child);
                }

                while (currentPlan[currentPlan.Count - 1] != element.Content)
                {
                    currentPlan.RemoveAt(currentPlan.Count - 1);
                }

                currentPlan.RemoveAt(currentPlan.Count - 1);
            }

            return allPlans;
        }

        private List<List<IGOAPReadOnlyAction>> GetAllBestPlans(TreeDecision<IGOAPReadOnlyAction> plans)
        {
            var bestCost = _comparer.BadCost;
            var currentPlan = new List<IGOAPReadOnlyAction>();
            var bestPlans = new List<List<IGOAPReadOnlyAction>>();

            foreach (var child in plans.Root.Children)
            {
                Iterate(_comparer.ZeroCost, child);
            }

            void Iterate(IGOAPCost previusCost, TreeElement<IGOAPReadOnlyAction> element)
            {
                var newCost = previusCost.GetSumWith(element.Content.Cost);
                currentPlan.Add(element.Content);

                if (!element.HasChildren)
                {
                    int value = _comparer.Compare(bestCost, newCost);

                    if (value <= 0)
                    {
                        if (value < 0)
                        {
                            bestCost = newCost;
                            bestPlans = new List<List<IGOAPReadOnlyAction>>();
                        }

                        bestPlans.Add(new List<IGOAPReadOnlyAction>(currentPlan.Where(x => !x.IsConnector)));
                    }

                    currentPlan.RemoveAt(currentPlan.Count - 1);
                    return;
                }

                foreach (var child in element.Children)
                {
                    Iterate(newCost, child);
                }

                while (currentPlan[currentPlan.Count - 1] != element.Content)
                {
                    currentPlan.RemoveAt(currentPlan.Count - 1);
                }

                currentPlan.RemoveAt(currentPlan.Count - 1);
            }

            return bestPlans;
        }

        private List<IGOAPReadOnlyAction> GetSingleBestPlan(TreeDecision<IGOAPReadOnlyAction> plans)
        {
            var bestPlans = GetAllBestPlans(plans);

            if (bestPlans.Count == 0)
                return null;

            return bestPlans.GetRandomElement();
        }
    }
}
