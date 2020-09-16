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
            var plans = new TreeDecision<GOAPAction>();
            plan = null;

            if (!_builder.TryBuildPlans(goal, out plans))
                return false;

            plan = GetBestPlan(plans);

            if(plan.Count == 0)
                return false;

            return true;
        }

        public bool TryGetAllPlans(KeyValuePair<string, GOAPState> goal, out IEnumerable<List<GOAPAction>> plans)
        {
            var treePlans = new TreeDecision<GOAPAction>();
            plans = null;

            if (!_builder.TryBuildPlans(goal, out treePlans))
                return false;

            plans = GetAllPlans(treePlans);

            if (plans == null || plans.Count() == 0)
                return false;

            return true;
        }


        private IEnumerable<List<GOAPAction>> GetAllPlans(TreeDecision<GOAPAction> plans)
        {
            var stack = new Stack<GOAPAction>();

            foreach (var child in plans.Root.Children)
            {
                foreach (var plan in Iterate(_comparer.ZeroCost, child))
                    yield return plan;
            }

            IEnumerable<List<GOAPAction>> Iterate(IGOAPCost previusCost, TreeElement<GOAPAction> element)
            {
                var newCost = previusCost.GetSumWith(element.Content.Cost);
                stack.Push(element.Content);

                if (!element.HasChildren)
                {
                    yield return stack.ToList();
                }

                foreach (var child in element.Children)
                {
                    Iterate(newCost, child);
                }

                while (stack.Pop() != element.Content) ;
            }
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
                    Console.WriteLine();

                    int value = _comparer.Compare(bestCost, newCost);

                    if (value < 0 || (value == 0 && BaseMath.Probability(0.5f)))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;

                        bestCost = newCost;
                        bestPlan = stack.Where(x => !x.IsConnector).ToList();
                    }

                    foreach (var action in stack)
                    {
                        Console.WriteLine($"|{action.Cost}|\t {action}");
                    }

                    Console.ResetColor();
                    return;
                }

                foreach (var child in element.Children)
                {
                    Iterate(newCost, child);
                }

                while (stack.Pop() != element.Content) ;
            }

            return bestPlan;
        }
    }
}
