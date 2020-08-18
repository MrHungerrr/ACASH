using System;
using System.Collections.Generic;
using Single;
using UnityEngine;

namespace GameTime.Action
{
    public class ActionPerTimeManager : Singleton<ActionPerTimeManager>
    {
        private List<ActionPerTime> _actions;

        public void SetLevel()
        {
            _actions = new List<ActionPerTime>();
            TimeManager.Instance.TimeUpdater += UpdateTime;
        }

        private void UpdateTime(float deltaTime)
        {
            for(int i = 0; i < _actions.Count; i++)
            {
                _actions[i].Update(deltaTime);
            }
        }

        public void Add(ActionPerTime action)
        {
            _actions.Add(action);
        }

        public void Remove(ActionPerTime action)
        {
            _actions.Remove(action);
        }
    }
}
