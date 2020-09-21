using System;
using GameTime.Action;
using System.Collections.Generic;
using UnityEngine;
using Vkimow.Tools.Single;



namespace GameTime
{
    public class TimeManager : Singleton<TimeManager>
    {
        public event System.Action OnSecondDone;
        public int TimeInSec => _timeInSec;


        private float _time;
        private int _timeInSec;

        public void SetLevel()
        {
            OnSecondDone = null;
            _time = 0;
            _timeInSec = 0;

            TimeActionManager.Instance.SetLevel();
            TimerShower.Instance.SetLevel();
        }

        public void Update()
        {
            TimeUpdate();
        }

        private void TimeUpdate()
        {
            _time += UnityEngine.Time.deltaTime;

            if (_timeInSec != (int)_time)
            {
                SecondDone();
            }
        }

        private void SecondDone()
        {
            //Debug.Log("Second Done!");
            _timeInSec = (int)_time;
            OnSecondDone?.Invoke();
        }
    }
}