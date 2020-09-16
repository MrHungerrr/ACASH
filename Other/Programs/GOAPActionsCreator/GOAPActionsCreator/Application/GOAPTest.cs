using GOAP;
using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.IO;

namespace Application
{
    public static class GOAPTest
    {
        public static void Construct(string goalKey)
        {
            var context = new GOAPStateContext(new GOAPStateLocal(), new GOAPStateGlobal());
            var comparer = new BaseCostComparer(10);

            var planer = new GOAPPlanner(context, comparer);
            var plan = new List<GOAPAction>();

            if (!planer.TryGetPlan(GOAPGoalsManager.Instance.Goals[goalKey], out plan))
                throw new Exception("Мы проебались!");

            foreach (var action in plan)
            {
                Console.WriteLine(action);
            }
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
