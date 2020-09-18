using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameTime.Action
{
    public class ActionPerTimeWithCounter: ActionPerTime
    {
        private int _numberOfActions;

        public ActionPerTimeWithCounter(float perTime, System.Action action, int numberOfActions): base(perTime, action)
        {
            if (numberOfActions <= 0)
                throw new ArgumentException();

            _numberOfActions = numberOfActions;
        }

        public override void Update(in float deltaTime)
        {
            _timePassed += deltaTime;

            if (_timePassed > TimeToPass)
                Invoke();
        }

        private void Invoke()
        {
            _action.Invoke();
            _timePassed = 0;
            _numberOfActions--;

            if (_numberOfActions <= 0)
            {
                Disable();
            }
        }
    }
}
