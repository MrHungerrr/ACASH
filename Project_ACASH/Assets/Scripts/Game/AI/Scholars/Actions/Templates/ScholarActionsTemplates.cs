using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Scholars.Actions.Operations;
using AI.Scholars.Actions.Operations.Types;

namespace  AI.Scholars.Actions
{
    public static class ScholarActionsTemplates
    {
        public static ScholarAction StartExam(Scholar scholar)
        {
            return new ScholarAction
            (
                new List<IScholarOperation>()
                {
                    new GoToDesk(scholar)
                }
            );
        }

        public static ScholarAction EndExam(Scholar scholar)
        {
            return new ScholarAction
            (
                new List<IScholarOperation>()
                {
                    new GoToDockStation(scholar)
                }
            );
        }

        public static ScholarAction WaitFor(Scholar scholar, float timeToWait)
        {
            return new ScholarAction
            (
                new List<IScholarOperation>()
                {
                    new Wait(scholar, timeToWait)
                }
            );
        }
    }
}
