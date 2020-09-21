using GOAP;
using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vkimow.Tools.Extensions;

namespace Application
{
    public static class GOAPTest
    {
        public static void ConstructAll(string goalKey)
        {
            var context = new GOAPStateContext(GOAPConxtextFactory.ScholarContext, GOAPConxtextFactory.ClassContext);
            var comparer = new BaseCostComparer();

            var planer = new GOAPPlanner(context, comparer);


            if (!planer.TryGetAllPlans(GOAPGoalsManager.Instance.Goals[goalKey], out var allPlans))
                throw new Exception("Мы проебались!");


            GOAPConsoleWriter.WritePlansWithBest(allPlans, comparer);
        }


        public static void ConstructAllBest(string goalKey)
        {
            var context = new GOAPStateContext(GOAPConxtextFactory.ScholarContext, GOAPConxtextFactory.ClassContext);
            var comparer = new BaseCostComparer();

            var planer = new GOAPPlanner(context, comparer);


            if (!planer.TryGetAllBestPlans(GOAPGoalsManager.Instance.Goals[goalKey], out var plans))
                throw new Exception("Мы проебались!");


            GOAPConsoleWriter.WritePlans(plans);
        }

        public static void ConstructBest(string goalKey)
        {
            var context = new GOAPStateContext(GOAPConxtextFactory.ScholarContext, GOAPConxtextFactory.ClassContext);
            var comparer = new BaseCostComparer();

            var planer = new GOAPPlanner(context, comparer);

            if (!planer.TryGetBestPlan(GOAPGoalsManager.Instance.Goals[goalKey], out var plan))
                throw new Exception("Мы проебались!");

            Console.ForegroundColor = ConsoleColor.Magenta;
            GOAPConsoleWriter.WritePlan(plan);
            Console.ResetColor();
        }


        public static void SaveLoad()
        {
            string directory = Directory.GetCurrentDirectory();

            while (Path.GetFileName(directory) != "ACASH")
            {
                directory = Path.GetDirectoryName(directory);
            }

            directory = Path.Combine(directory, "Project_ACASH", "Assets", "Resources", "GOAP");

            GOAPIO.Instance.Save(directory);
            GOAPIO.Instance.Load(directory);
        }
    }
}
