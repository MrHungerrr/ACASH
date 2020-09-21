using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using GOAP;


namespace Application
{
    public static class GOAPConsoleWriter
    {
        public static void WriteActions()
        {
            Console.WriteLine("\n\n\n  ACTIONS\n");
            var table = new ConsoleTable("Action", "Cost", "Effect", "Precondition");

            foreach (var action in GOAPActionsManager.Instance.Actions)
            {
                bool firstWrite = true;

                foreach (var precondition in action.Preconditions)
                {
                    if (firstWrite)
                    {
                        table.AddRow(action.Name.ToString(), action.Cost.ToString(), action.Effect.ToString(), $"\"{precondition.Key}\" = {precondition.Value.Value}");
                        firstWrite = false;
                    }
                    else
                        table.AddRow(string.Empty, string.Empty, string.Empty, $"\"{precondition.Key}\" = {precondition.Value.Value}");
                }
            }

            table.Write();
        }

        public static void WriteActionsEffects()
        {
            Console.WriteLine("\n\n\n  ACTIONS with EFFECTS\n");
            var table = new ConsoleTable("Action", "Effect", "Cost");

            foreach (var action in GOAPActionsManager.Instance.Actions)
            {
                table.AddRow(action.Name.ToString(), action.Effect.ToString(), action.Cost.ToString());
            }

            table.Write();
        }

        public static void WriteActionsPreconditions()
        {
            Console.WriteLine("\n\n\n  ACTIONS\n");
            var table = new ConsoleTable("Action", "State Type","State", "Cost");

            foreach (var action in GOAPActionsManager.Instance.Actions)
            {
                table.AddRow(action.Name.ToString(), "Effect", action.Effect.ToString(), action.Cost.ToString());

                foreach (var precondition in action.Preconditions)
                {
                    table.AddRow(action.Name.ToString(), "Precondition", $"\"{precondition.Key}\" = {precondition.Value.Value}", action.Cost.ToString());
                }
            }

            table.Write();
        }

        public static void WriteBlanks()
        {
            Console.WriteLine("\n\n\n  BLANKS\n");
            var table = new ConsoleTable("Blank Key", "Type");

            foreach (var blank in GOAPBlanksManager.Instance.Blanks)
            {
                table.AddRow(blank.Key, blank.Value);
            }

            table.Write();
        }


        public static void WriteBlanksUsingBy()
        {
            Console.WriteLine("\n\n\n  BLANKS IS USING BY\n");

            var table = new ConsoleTable("Blank Key", "Value Type", "Using Type", "Used By");

            foreach (var blank in GOAPBlanksManager.Instance.Blanks)
            {

                var effectsWithBlank = new List<IGOAPReadOnlyAction>();
                var preconditionsWithBlank = new List<IGOAPReadOnlyAction>();

                foreach (var action in GOAPActionsManager.Instance.Actions)
                {
                    if (action.Effect.Contains(blank.Key))
                    {
                        effectsWithBlank.Add(action);
                    }

                    if (action.Preconditions.Contains(blank.Key))
                    {
                        preconditionsWithBlank.Add(action);
                    }
                }

                bool firstWrite = true;

                foreach (var action in effectsWithBlank)
                {
                    if (firstWrite)
                    {
                        table.AddRow(blank.Key, blank.Value, "Effect", action.Name);
                        firstWrite = false;
                    }
                    else
                        table.AddRow(string.Empty, string.Empty, "Effect", action.Name);
                }

                foreach (var action in preconditionsWithBlank)
                {
                    if (firstWrite)
                    {
                        table.AddRow(blank.Key, blank.Value, "Precondition", action.Name);
                        firstWrite = false;
                    }
                    else
                        table.AddRow(string.Empty, string.Empty, "Precondition", action.Name);
                }
            }

            table.Write();
        }



        public static void WritePlan(List<IGOAPReadOnlyAction> plan)
        {
            Console.WriteLine();
            foreach (var action in plan)
            {
                Console.WriteLine($"|{action.Cost}|\t{action}");
            }
        }

        public static void WritePlans(List<List<IGOAPReadOnlyAction>> plans)
        {
            Console.WriteLine();
            foreach (var plan in plans)
            {
                WritePlan(plan);
            }
        }

        public static void WritePlansWithBest(List<List<IGOAPReadOnlyAction>> plans, IGOAPCostComparer comparer)
        {
            Console.WriteLine();

            var bestCost = comparer.BadCost;

            foreach (var plan in plans)
            {
                IGOAPCost fullCost = comparer.ZeroCost;

                foreach (var action in plan)
                {
                    fullCost = fullCost.GetSumWith(action.Cost);
                }

                int value = comparer.Compare(bestCost, fullCost);

                if (value < 1)
                {
                    bestCost = fullCost;
                }
            }

            foreach (var plan in plans)
            {
                IGOAPCost fullCost = comparer.ZeroCost;

                foreach (var action in plan)
                {
                    fullCost = fullCost.GetSumWith(action.Cost);
                }

                int value = comparer.Compare(bestCost, fullCost);

                if (value > 0)
                {
                    WritePlan(plan);
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var plan in plans)
            {
                IGOAPCost fullCost = comparer.ZeroCost;

                foreach (var action in plan)
                {
                    fullCost = fullCost.GetSumWith(action.Cost);
                }

                int value = comparer.Compare(bestCost, fullCost);

                if (value == 0)
                {
                    WritePlan(plan);
                }
            }
            Console.ResetColor();
        }
    }
}
