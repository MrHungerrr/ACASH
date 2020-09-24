using GameTime.Action;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Scholars.Actions.Operations.Types
{
    class Talk : ScholarOperation
    {
        private readonly float _timeToTalk;
        private float _timeLeft;

        public Talk(Scholar scholar, float timeToTalk) : base(scholar)
        {
            _timeToTalk = timeToTalk;
        }

        public override void Execute()
        {
            _timeLeft = _timeToTalk;
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
            Stop();
            base.OperationDone();
        }

        public override void Stop()
        {
            UpdateManager.Instance.OnUpdate -= Update;
        }

        public override string ToString()
        {
            return $"Talk With";
        }
    }
}
