﻿using System;
using System.Collections.Generic;
using UnityEngine;
using AI.Scholars.Actions.Operations;

namespace AI.Scholars.Actions
{
    class ScholarActionsExecuter
    {
        public event Action OnActionDone;
        public bool IsPaused { get; private set; }
        public bool IsExecuting { get; private set; }


        private ScholarAction _action;
        private IScholarOperation _operation;
        private int _actionIndex;



        public ScholarActionsExecuter() { }

        public void Reset()
        {
            if(IsExecuting)
                Stop();

            IsExecuting = false;
            _action = default;
            _operation = null;
        }


        public void Execute(ScholarAction action)
        {
            if (IsExecuting)
                Stop();

            IsExecuting = true;
            SetAction(action);
            ExecuteOperation();
        }

        public void Continue()
        {
            if (!IsPaused || !IsExecuting)
                throw new Exception();

            IsPaused = false;
            ExecuteOperation();
        }

        public void Pause()
        {
            if (IsPaused || !IsExecuting)
                throw new Exception();

            IsPaused = true;
            Stop();
        }


        private void SetAction(ScholarAction action)
        {
            _action = action;
            _actionIndex = 0;
            _operation = _action.Operations[_actionIndex];
            IsPaused = false;
        }

        private void ExecuteOperation()
        {
            _operation.OnOperationDone += OperationDone;
            _operation.Execute();
        }

        private void NextOperation()
        {
            _actionIndex++;

            if (_actionIndex != _action.Operations.Count)
            {
                _operation = _action.Operations[_actionIndex];
                ExecuteOperation();
            }
            else
            {
                ActionDone();
            }
        }

        private void Stop()
        {
            Debug.Log("Stop!");

            _operation.OnOperationDone -= OperationDone;
            _operation.Stop();
        }

        private void OperationDone()
        {
            Debug.Log("Operation Done!");

            _operation.OnOperationDone -= OperationDone;
            NextOperation();
        }

        private void ActionDone()
        {
            IsExecuting = false;
            OnActionDone.Invoke();
        }
    }
}