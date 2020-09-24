using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI.Scholars.Actions
{
    public sealed class ScholarActionsController
    {
        public ScholarActionsPlanner Planner => _planner;


        private ScholarActionsQueue _actions;
        private readonly ScholarActionsPlanner _planner;
        private readonly ScholarActionsExecuter _executer;


        public ScholarActionsController(Scholar scholar)
        {
            _planner = new ScholarActionsPlanner(scholar);
            _executer = new ScholarActionsExecuter();
            _executer.OnActionDone += ActionDone;

            Reset();
        }

        public void Reset()
        {
            _executer.Reset();
            _planner.Reset();
            _actions = new ScholarActionsQueue();
            ActionDone();
        }


        public void AddToQueue(ScholarAction action)
        {
            _actions.Add(action);

            if (!_executer.IsExecuting)
                NextAction();
        }


        public void Execute(ScholarAction action)
        {
            _executer.Execute(action);
        }


        public void Skip()
        {
            ActionDone();
        }

        public void Continue()
        {
            _executer.Continue();
        }

        public void Pause()
        {
            _executer.Pause();
        }


        private void NextAction()
        {
            var action = _actions.GetNext();
            Execute(action);
        }


        private void ActionDone()
        {
            if (_actions.IsEmpty)
            {
                var plan = _planner.GetNextPlan();
                _actions.Add(plan);
            }

            if (_actions.IsEmpty)
                throw new Exception("Пустая очередь действий");

            NextAction();
        }
    }
}
