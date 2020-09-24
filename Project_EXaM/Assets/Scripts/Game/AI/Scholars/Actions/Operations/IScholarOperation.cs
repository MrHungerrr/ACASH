using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Scholars.Actions.Operations
{
    public interface IScholarOperation
    {
        event Action OnOperationDone;

        Scholar Scholar { get; }

        void Execute();

        void Stop();
    }
}
