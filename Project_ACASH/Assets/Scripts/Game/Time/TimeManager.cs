using System;
using GameTime.Action;
using System.Collections.Generic;
using UnityEngine;
using Single;



namespace GameTime
{
    public class TimeManager : Singleton<TimeManager>
    {
        public System.Action OnSecondDone { get; set; }
        public int TimeInSec => _timeInSec;
        public Timer Timer => _timer;


        private float _time;
        private int _timeInSec;
        private Timer _timer;

        public void SetLevel()
        {
            OnSecondDone = null;
            _timer = null;
            _time = 0;
            _timeInSec = 0;

            ActionPerTimeManager.Instance.SetLevel();
            ActionSchedule.Instance.SetLevel();
            TimerShower.Instance.SetLevel();
        }

        public void Update()
        {
            TimeUpdate();
        }

        public void SetTimer(int time)
        {
            _timer = new Timer(time);
        }

        private void TimeUpdate()
        {
            float deltaTime = UnityEngine.Time.deltaTime;
            _time += deltaTime;

            if (_timeInSec != (int)_time)
            {
                SecondDone();
            }
        }

        private void SecondDone()
        {

            Debug.Log("Second Done!");
            _timeInSec = (int)_time;
            OnSecondDone?.Invoke();
        }
    }

}





