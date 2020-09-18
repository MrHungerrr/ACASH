
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace GameTime.Action
{
    public abstract class ActionPerTime : ITimeAction
    {
        protected readonly float TimeToPass;
        protected readonly System.Action _action;

        protected double _timePassed;

        protected ActionPerTime(float perTime, System.Action action)
        {
            TimeToPass = perTime;
            _action = action;
        }


        public static void Create(float perTime, System.Action action, CancellationToken cancellationToken)
        {
            Func<bool> disableReason = () => cancellationToken.IsCancellationRequested;
            var actionPerTime = new ActionPerTimeWithDisable(perTime, action, disableReason);
            TimeActionManager.Instance.Add(actionPerTime);
        }

        public static void Create(float perTime, System.Action action, System.Func<bool> disableReason)
        {
            var actionPerTime = new ActionPerTimeWithDisable(perTime, action, disableReason);
            TimeActionManager.Instance.Add(actionPerTime);
        }

        public static void Create(float perTime, System.Action action, int numberOfActions)
        {
            var actionPerTime = new ActionPerTimeWithCounter(perTime, action, numberOfActions);
            TimeActionManager.Instance.Add(actionPerTime);
        }

        public virtual void Update(in float deltaTime)
        {
            _timePassed += deltaTime;

            if (_timePassed > TimeToPass)
                Invoke();
        }

        private void Invoke()
        {
            _action();
            _timePassed = 0;
        }

        public void Disable()
        {
            TimeActionManager.Instance.Remove(this);
        }
    }
}
