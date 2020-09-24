using System;
using System.Collections.Generic;
using Vkimow.Tools.Single;
using UnityEngine;

namespace GameTime.Action
{
    public class TimeActionManager : Singleton<TimeActionManager>
    {
        private List<ITimeAction> _actions;

        public void SetupSchool()
        {
            _actions = new List<ITimeAction>();
            UpdateManager.Instance.OnUpdate += UpdateTime;
        }

        private void UpdateTime()
        {
            for(int i = 0; i < _actions.Count; i++)
            {
                _actions[i].Update(UnityEngine.Time.deltaTime);
            }
        }

        public void Add(ITimeAction action)
        {
            _actions.Add(action);
        }

        public void Remove(ITimeAction action)
        {
            _actions.Remove(action);
        }
    }
}
