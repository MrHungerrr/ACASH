using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class TimeManager : Singleton<TimeManager>
{

    private bool active = false;
    [HideInInspector]
    public float time_passed { get; private set; }
    private float time_left;
    private float time;
    private string time_string;
    private float time_sec = 0;
    private float time_sec_previous = 0;
    private Timer[] timers;

    private string time_name;



    private string minutes;
    private string seconds;

    private Dictionary<ExamManager.part, TimeStorage> times = new Dictionary<ExamManager.part, TimeStorage>()
    {
        {ExamManager.part.Chill, new TimeStorage(5, "Time before exam") },
        {ExamManager.part.Prepare, new TimeStorage(15, "Exam need start in") },
        {ExamManager.part.Exam, new TimeStorage(300, "Exam will end in") },
        {ExamManager.part.Afterhours, new TimeStorage(0, "Exam is OVER") },
    };





    public void Setup()
    {
        active = false;
        timers = FindObjectsOfType<Timer>();
    }

    public void Reset()
    {
        SetTime(ExamManager.part.Chill);
    }

    private void Update()
    {
        if(active)
        {
            Time();
        }
    }

    public void Enable(bool option)
    {
        active = option;
    }

    public void SetTime(ExamManager.part part)
    {
        SetTime(times[part]);
    }


    private void SetTime(TimeStorage storage)
    {
        this.time = storage.time;
        time_left = this.time;
        time_passed = 0;

        time_sec = (int)time_left % 60;
        time_sec_previous = time_sec;

        time_name = storage.topic;
        HUDController.get.TimeHeader(time_name);

        ShowTime();
        active = true;
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
            TimeDone();
        }
    }


    private void TimeDone()
    {
        active = false;

        time_left = 0;
        time_passed = time;
        ShowTime();

        ExamManager.get.TimeDone();
    }




    private void ShowTime()
    {
        time_string = StringTime(time_left);
        HUDController.get.Time(time_string);

        foreach (Timer t in timers)
        {
            t.Time(time_string);
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







