using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class TimeShower : Singleton<TimeShower>
{


    public void SetLevel()
    {
        TimeManager.Instance.OnSecondDone += ShowTime;
        TimeManager.Instance.OnTimeDone += ShowTime;
    }


    private void ShowTime()
    {
        ShowTime(TimeManager.Instance.TimeLeftInSec);
    }

    private void ShowTime(int timeInSeconds)
    {
        var timeString = StringTime(timeInSeconds);
        HUDController.Instance.Time(timeString);
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







