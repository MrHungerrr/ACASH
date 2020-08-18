using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTime
{
    public class Timer
    {
        public System.Action OnTimeDone { get; set; }
        public int TimePassedInSec => _timePassed;
        public int TimeLeftInSec => _timeGeneral - _timePassed;


        private readonly int _timeGeneral;
        private int _timePassed;


        public Timer(int time)
        {
            _timeGeneral = time;
            OnTimeDone = null;
            TimeManager.Instance.OnSecondDone += SecondDone;
        }

        public void SecondDone()
        {
            _timePassed++;

            TimerShower.Instance.SecondDone();

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
