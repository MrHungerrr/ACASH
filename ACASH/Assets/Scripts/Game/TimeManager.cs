using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;

public class TimeManager : Singleton<TimeManager>
{

    private bool active;
    private float time_passed;
    private float time_left;
    private float time;
    private string time_string;
    private float time_sec = 0;
    private float time_sec_previous = 0;
    private Timer[] timers;

    [HideInInspector]
    public string time_name;



    private string minutes;
    private string seconds;

    private float[] times = new float[]
    {
        30f,
        60f,
        600f
    };

    private string[] time_names = new string[]
    {
        "Time before exam",
        "Exam need start in",
        "Exam will end in"
    };


    private void Awake()
    {
        active = false;
        time = 60 * 10;
        time_left = 0;
        time_passed = time;
    }

    private void Update()
    {
        if(active)
        {
            Time();
        }
    }

    public void Enable()
    {
        time_left = time;
        time_passed = 0;
        active = true;
    }

    public void SetTime(int index)
    {
        time = times[index];
        time_left = time;
        time_passed = 0;

        time_sec = (int)time_left % 60;
        time_sec_previous = time_sec;

        ShowTime();
        active = true;

        time_name = time_names[index];
        HUDController.get.TimeHeader(time_name);
    }


    public void SetTimers()
    {
        timers = FindObjectsOfType<Timer>();
        Debug.Log(timers.Length);
    }

    private void Time()
    {
        if (time_left > 0)
        {
            time_passed += UnityEngine.Time.deltaTime;
            time_left = time - time_passed;

            time_sec = (int)time_left % 60;

            if(time_sec != time_sec_previous)
            {
                time_sec_previous = time_sec;
                ShowTime();
            }
        }
        else
        {
            time_left = 0;
            time_passed = time;
            active = false;
            ShowTime();
            ExamManager.get.TimeDone();
        }
    }




    private void ShowTime()
    {
        Debug.Log(timers.Length);

        time_string = StringTime(time_left);
        HUDController.get.Time(time_string);

        for(int i = 0; i< timers.Length; i++)
        {
            timers[i].Time(time_string);
        }
    }

    private string StringTime(float t)
    {
        if ((((int)t / 60) / 10) > 0)
        {
            minutes = ((int)t / 60).ToString();
        }
        else
        {
            minutes = "0" + ((int)t / 60);
        }

        if ((((int)t % 60) / 10) > 0)
        {
            seconds = ((int)t % 60).ToString();
        }
        else
        {
            seconds = "0" + ((int)t % 60);
        }

        return (minutes + ":" + seconds);
    }
}
