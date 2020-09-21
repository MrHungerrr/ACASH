using AI.Scholars;
using AI.Scholars.GOAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vkimow.Tools.Single;

namespace Exam.Events
{
    public static class ExamEventExecuter
    {
        public static void Cheat()
        {
            var scholar = ScholarManager.Instance.GetRandomScholar();
            scholar.Actions.Planner.SetGoal(ScholarGOAPPlanner.Goals.Cheat);
        }

        public static void Distraction()
        {
            var scholar = ScholarManager.Instance.GetRandomScholar();
            scholar.Actions.Planner.SetGoal(ScholarGOAPPlanner.Goals.Distraction);
        }
    }
}
