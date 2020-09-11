using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Scholars.Actions
{
    class ScholarActionsQueue
    {
        public bool IsEmpty => _actions.Count == 0;

        private Queue<ScholarAction> _actions;
        

        public ScholarActionsQueue()
        {
            _actions = new Queue<ScholarAction>();
        }


        public void Add(ScholarAction action)
        {
            _actions.Enqueue(action);
        }

        public ScholarAction GetNext()
        {
            return _actions.Dequeue();
        }
    }
}
