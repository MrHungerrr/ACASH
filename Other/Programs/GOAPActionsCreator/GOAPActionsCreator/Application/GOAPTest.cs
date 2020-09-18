using GOAP;
using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Application
{
    public static class GOAPTest
    {
        public static void Construct(string goalKey)
        {
            var context = new GOAPStateContext(new GOAPStateLocal(), new GOAPStateGlobal());
            var comparer = new BaseCostComparer();

            var planer = new GOAPPlanner(context, comparer);

            List<List<GOAPAction>> plans;
            var plan = new List<GOAPAction>();


            if (!planer.TryGetAllPlans(GOAPGoalsManager.Instance.Goals[goalKey], out plans))
                throw new Exception("Мы проебались!");

            if (!planer.TryGetBestPlan(GOAPGoalsManager.Instance.Goals[goalKey], out plan))
                throw new Exception("Мы проебались!");




            foreach(var p in plans)
            {
                GOAPConsoleWriter.WritePlan(p);
            }

            Console.ForegroundColor = ConsoleColor.Green;
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
