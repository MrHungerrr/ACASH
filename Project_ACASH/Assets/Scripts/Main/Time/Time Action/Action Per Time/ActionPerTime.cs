
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace GameTime.Action
{
    public abstract class ActionPerTime
    {
        protected readonly float TimeToPass;
        protected readonly System.Action Action;

        protected double _timePassed;

        protected ActionPerTime(float perTime, System.Action action)
        {
            TimeToPass = perTime;
            Action = action;
        }


        public static void Create(float perTime, System.Action action, CancellationToken cancellationToken)
        {
            Func<bool> disableReason = () => cancellationToken.IsCancellationRequested;
            var actionPerTime = new ActionPerTimeWithDisable(perTime, action, disableReason);
            ActionPerTimeManager.Instance.Add(actionPerTime);
        }

        public static void Create(float perTime, System.Action action, System.Func<bool> disableReason)
        {
            var actionPerTime = new ActionPerTimeWithDisable(perTime, action, disableReason);
            ActionPerTimeManager.Instance.Add(actionPerTime);
        }

        public static void Create(float perTime, System.Action action, int numberOfActions)
        {
            var actionPerTime = new ActionPerTimeWithCounter(perTime, action, numberOfActions);
            ActionPerTimeManager.Instance.Add(actionPerTime);
        }

        public virtual void Update(in float deltaTime)
        {
            _timePassed += deltaTime;

            if (_timePassed > TimeToPass)
            {
                Action();
                _timePassed = 0;
            }
        }

        protected void Disable()
        {
            ActionPerTimeManager.Instance.Remove(this);
        }
    }
}
