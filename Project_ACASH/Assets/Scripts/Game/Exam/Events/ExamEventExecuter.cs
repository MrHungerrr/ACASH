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
    public class ExamEventExecuter : Singleton<ExamEventExecuter>
    {
        public void Execute(string goalKey)
        {
            var scholar = ScholarManager.Instance.GetRandomScholar();
            var plan = ScholarGOAPPlanner.Instance.GetPlan(scholar, goalKey);
            scholar.Actions.Execute(plan);
        }
    }
}
