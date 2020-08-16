using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameTime.Action
{
    public class ActionPerTimeByNumber: ActionPerTime
    {
        private int _numberOfActions;

        public ActionPerTimeByNumber(float perTime, System.Action action, int numberOfActions): base(perTime, action)
        {
            _numberOfActions = numberOfActions;
        }

        public override void Update(in float deltaTime)
        {
            _timePassed += deltaTime;

            if (_timePassed > TimeToPass)
            {
                Action();
                _timePassed = 0;
                _numberOfActions--;

                if(_numberOfActions <= 0)
                {
                    Disable();
                }
            }
        }
    }
}
