using GameTime.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTime
{
    public class Timer
    {
        public event System.Action OnTimeDone;
        public event System.Action OnTimeChanged;
        public int TimeGeneral => _timeGeneral;
        public int TimePassedInSec => _timePassed;
        public int TimeLeftInSec => _timeGeneral - _timePassed;
        public IAddOnlyActionSchedule Schedule => _schedule;


        private readonly int _timeGeneral;
        private int _timePassed;
        private ActionSchedule _schedule;


        public Timer(int time)
        {
            _timeGeneral = time;
            _schedule = new ActionSchedule(this);
        }

        public void Start()
        {
            TimeManager.Instance.OnSecondDone += SecondDone;
        }


        private void SecondDone()
        {
            ++_timePassed;

            _schedule.SecondDone();
            OnTimeChanged?.Invoke();

            if (_timePassed == _timeGeneral)
                TimeDone();
        }

        private void TimeDone()
        {
            OnTimeDone?.Invoke();
            OnTimeDone = null;
            TimeManager.Instance.OnSecondDone -= SecondDone;
        }
    }
}
