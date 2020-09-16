using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vkimow.Tools.Single;



namespace GameTime
{
    public class TimerShower : Singleton<TimerShower>
    {
        public void SetLevel()
        {
            ShowTime(0);
        }

        public void SecondDone()
        {
            ShowTime();
        }


        private void ShowTime()
        {
            ShowTime(TimeManager.Instance.Timer.TimeLeftInSec);
        }

        private void ShowTime(int timeInSeconds)
        {
            var timeString = StringTime(timeInSeconds);
        }

        private string StringTime(int t)
        {
            int minutes = (t / 60);
            string strMinutes;

            if ((minutes / 10) > 0)
            {
                strMinutes = minutes.ToString();
            }
            else
            {
                strMinutes = "0" + minutes;
            }

            int seconds = (t % 60);
            string strSeconds;

            if ((seconds / 10) > 0)
            {
                strSeconds = seconds.ToString();
            }
            else
            {
                strSeconds = "0" + seconds;
            }

            return (strMinutes + ":" + strSeconds);
        }
    }
}






