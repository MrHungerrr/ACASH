using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI.Scholars.Actions
{
    public class ScholarActionsController
    {
        public ScholarActionsUpdater Updater => _updater;

        private ScholarActionsUpdater _updater;
        private ScholarActionsQueue _actions;
        private readonly ScholarActionsExecuter _executer;


        public ScholarActionsController(Scholar scholar)
        {
            _updater = new ScholarActionsUpdater(scholar);
            _executer = new ScholarActionsExecuter();
            _executer.OnActionDone += ActionDone;
            Reset();
        }

        public void Reset()
        {
            _actions = new ScholarActionsQueue();
            _updater.Enable(false);
            _executer.Reset();
        }


        public void AddToQueue(ScholarAction action)
        {
            _actions.Add(action);

            if (!_executer.IsExecuting)
                NextAction();
        }


        public void Execute(ScholarAction action)
        {
            Debug.Log("I'm Executing!");
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
            if (_updater.IsActive)
            {
                if (_actions.IsEmpty)
                {
                    var action = _updater.GetSomeAction();
                    _actions.Add(action);
                }
            }

            if (!_actions.IsEmpty)
            {
                NextAction();
            }
        }
    }
}
