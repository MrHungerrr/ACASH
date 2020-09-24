using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vkimow.Tools.Single;

namespace GameTime.Action
{
    public class ActionSchedule : IAddOnlyActionSchedule
    {
        private List<ActionScheduleItem> _actions;
        private Timer _timer;


        public ActionSchedule(Timer timer)
        {
            _timer = timer;
            _actions = new List<ActionScheduleItem>();
        }


        public void AddActionInTime(int inTime, System.Action action)
        {
            if (_timer.TimeLeftInSec < inTime)
                throw new ArgumentOutOfRangeException();

            var newItem = new ActionScheduleItem(action, inTime);
            _actions.Add(newItem);
        }

        public void AddActionAtTime(int atTime, System.Action action)
        {
            if (_timer.TimePassedInSec >= atTime)
                throw new ArgumentOutOfRangeException();

            if(_timer.TimeGeneral < atTime)
                throw new ArgumentOutOfRangeException();

            var newItem = new ActionScheduleItem(action, atTime - _timer.TimePassedInSec);
            _actions.Add(newItem);
        }


        public void SecondDone()
        {
            TryInvokeAction();
        }


        private void TryInvokeAction()
        {
            var elementsToDelete = new List<int>();

            for(int i = 0; i < _actions.Count; i++)
            {
                _actions[i].SecondDone();

                if (_actions[i].TimeToInvoke <= 0)
                {
                    _actions[i].Invoke();
                    elementsToDelete.Add(i);
                }
            }

            for(int i = 0; i < elementsToDelete.Count; i++)
            {
                _actions.RemoveAt(elementsToDelete[i]);
            }
        }
    }
}







