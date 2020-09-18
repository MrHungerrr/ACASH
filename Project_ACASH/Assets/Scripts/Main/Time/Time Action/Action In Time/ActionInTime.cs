using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTime.Action
{
    public class ActionInTime : ITimeAction
    {
        private readonly System.Action _action;
        private float _timeLeft;

        protected ActionInTime(float inTime, System.Action action)
        {
            _action = action;
            _timeLeft = inTime;
        }

        public static void Create(float inTime, System.Action action)
        {
            var actionInTime = new ActionInTime(inTime, action);
            TimeActionManager.Instance.Add(actionInTime);
        }

        public void Update(in float deltaTime)
        {
            _timeLeft -= deltaTime;

            if (_timeLeft <= 0)
                Invoke();
        }


        private void Invoke()
        {
            _action.Invoke();
            TimeActionManager.Instance.Remove(this);
        }
    }
}
