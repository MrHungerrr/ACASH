using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Scholars.Actions.Operations
{
    public abstract class ScholarOperation : IScholarOperation
    {
        public event Action OnOperationDone;
        Scholar IScholarOperation.Scholar => _scholar;


        protected readonly Scholar _scholar;

        public ScholarOperation(Scholar scholar)
        {
            _scholar = scholar;
        }

        protected virtual void OperationDone()
        {
            OnOperationDone.Invoke();
        }


        public abstract void Execute();

        public abstract void Stop();

        public override abstract string ToString();
    }
}
