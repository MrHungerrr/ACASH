using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private bool active;
    private TextMeshPro text;
    private string string_time;
    private string minutes;
    private string seconds;


    private void Awake()
    {
        text = transform.GetComponentInChildren<TextMeshPro>();
        string_time = StringTime(TimeManager.get.time_test);
        ShowTime();
    }

    public void Enable()
    {
        active = true;
    }

    private void Update()
    {
        if (active)
        {
            TestTime();
        }
    }


    private void TestTime()
    {
        string_time = StringTime(TimeManager.get.time_left);
        ShowTime();
    }

    private string StringTime(float t)
    {
        if((((int)t/60)/10) > 0)
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

    private void ShowTime()
    {
        text.text = string_time;
        HUDController.get.Time(string_time);
    }
}
