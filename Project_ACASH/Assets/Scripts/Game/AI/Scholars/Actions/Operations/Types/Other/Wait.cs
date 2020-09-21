using System;
using GameTime;
using GameTime.Action;
using UnityEngine;

namespace AI.Scholars.Actions.Operations.Types
{
    public class Wait : ScholarOperation
    {
        protected readonly float _timeToWait;
        private float _timeLeft;

        public Wait(Scholar scholar, float timeToWait): base(scholar)
        {
            _timeToWait = timeToWait;
        }

        public override void Execute()
        {
            _timeLeft = _timeToWait;
            UpdateManager.Instance.OnUpdate += Update;
        }


        private void Update()
        {
            _timeLeft -= Time.deltaTime;

            if (_timeLeft <= 0)
                OperationDone();
        }

        protected override void OperationDone()
        {
            UpdateManager.Instance.OnUpdate -= Update;
            base.OperationDone();
        }

        public override void Stop()
        {
            UpdateManager.Instance.OnUpdate -= Update;
        }

        public override string ToString()
        {
            return $"Wait For {_timeToWait}";
        }
    }
}
