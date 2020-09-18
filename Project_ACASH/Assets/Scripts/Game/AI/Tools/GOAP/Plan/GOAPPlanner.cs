using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GOAP.Cost;
using Vkimow.Structures.Trees.Decision;
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

        public bool TryGetBestPlan(KeyValuePair<string, GOAPState> goal, out List<GOAPAction> plan)
        {
            TreeDecision<GOAPAction> treePlans;
            plan = null;

            if (!_builder.TryBuildPlans(goal, out treePlans))
                return false;

            plan = GetBestPlan(treePlans);

            if(plan.Count == 0)
                return false;

            return true;
        }

        public bool TryGetAllPlans(KeyValuePair<string, GOAPState> goal, out List<List<GOAPAction>> plans)
        {
            TreeDecision<GOAPAction> treePlans;
            plans = null;

            if (!_builder.TryBuildPlans(goal, out treePlans))
                return false;

            plans = GetAllPlans(treePlans);

            if (plans.Count == 0)
                return false;

            return true;
        }


        private List<List<GOAPAction>> GetAllPlans(TreeDecision<GOAPAction> plans)
        {
            var stack = new Stack<GOAPAction>();
            var list = new List<List<GOAPAction>>();

            foreach (var child in plans.Root.Children)
            {
                Iterate(child);
            }

            void Iterate(TreeElement<GOAPAction> element)
            {
                stack.Push(element.Content);

                if (!element.HasChildren)
                {
                    list.Add(stack.ToList());
                    stack.Pop();
                    return;
                }

                foreach (var child in element.Children)
                {
                    Iterate(child);
                }          

                while (stack.Pop() != element.Content) ;
            }

            return list;
        }

        private List<GOAPAction> GetBestPlan(TreeDecision<GOAPAction> plans)
        {
            var bestCost = _comparer.BadCost;
            var bestPlan = new List<GOAPAction>();
            var stack = new Stack<GOAPAction>();

            foreach (var child in plans.Root.Children)
            {
                Iterate(_comparer.ZeroCost, child);
            }

            void Iterate(IGOAPCost previusCost, TreeElement<GOAPAction> element)
            {
                var newCost = previusCost.GetSumWith(element.Content.Cost);
                stack.Push(element.Content);

                if (!element.HasChildren)
                {
                    int value = _comparer.Compare(bestCost, newCost);

                    if (value < 0 || (value == 0 && BaseMath.Probability(0.5f)))
                    {
                        bestCost = newCost;
                        bestPlan = stack.Where(x => !x.IsConnector).ToList();
                    }

                    stack.Pop();
                    return;
                }

                foreach (var child in element.Children)
                {
                    Iterate(newCost, child);
                }

                while (stack.Pop() != element.Content);
            }

            return bestPlan;
        }
    }
}
