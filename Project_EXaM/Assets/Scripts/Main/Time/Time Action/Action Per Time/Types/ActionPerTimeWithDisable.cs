using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameTime.Action
{
    public class ActionPerTimeWithDisable: ActionPerTime
    {
        private readonly System.Func<bool> DoINeedDisable;

        public ActionPerTimeWithDisable(float perTime, System.Action action, System.Func<bool> disableReason) : base(perTime, action)
        {
            DoINeedDisable = disableReason;
        }

        public override void Update(in float deltaTime)
        {
            if (DoINeedDisable())
            {
                Disable();
                return;
            }

            base.Update(deltaTime);
        }
    }
}
