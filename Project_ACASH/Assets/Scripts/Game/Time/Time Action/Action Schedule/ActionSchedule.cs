using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

namespace GameTime.Action
{
    public class ActionSchedule : Singleton<ActionSchedule>
    {

        private List<ActionScheduleItem> _actions;

        public void SetLevel()
        {
            _actions = new List<ActionScheduleItem>();
            TimeManager.Instance.OnSecondDone += TryInvokeAction;
        }


        public void AddActionInTime(int inTime, System.Action action)
        {
            int time = TimeManager.Instance.TimeInSec + inTime;
            var newItem = new ActionScheduleItem(action, inTime);
            _actions.Add(newItem);
        }


        public void TryInvokeAction()
        {
            int time = TimeManager.Instance.TimeInSec;

            var elementsToDelete = new List<int>();

            for(int i = 0; i < _actions.Count; i++)
            {
                _actions[i].TimePassing();

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







