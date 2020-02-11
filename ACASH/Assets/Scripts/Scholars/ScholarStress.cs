using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarStress
{

    private Scholar Scholar;

    //Стресс и настроение
    public int value;
    public int threshold_1 = 33;
    public int threshold_2 = 66;
    private byte moodType;
    public float[] moodType_time = new float[3];
    private string[] moodType_string = new string[]
    {
        "chill",
        "normal",
        "panic"
    };

    public ScholarStress(Scholar s)
    {
        Scholar = s;
        Set(0);
    }

    private void Set(int v)
    {
        value = v;
        if (value > 100)
            value = 100;
        if (value < 0)
            value = 0;

        Scholar.TextBox.StressLevel(value);
        ChangeMoodType();
    }

    public void Change(int v)
    {
        value += v;
        if (value > 100)
            value = 100;
        if (value < 0)
            value = 0;

        Scholar.TextBox.StressLevel(value);
        ChangeMoodType();
    }

    public void ChangeMoodType()
    {
        if (value < threshold_1)
            moodType = 0;
        else if (value < threshold_2)
            moodType = 1;
        else
            moodType = 2;
    }



    public string GetMoodType()
    {
        return moodType_string[moodType];
    }


    public void MoodTypeTimeUpdate()
    {
        moodType_time[moodType] += Time.fixedDeltaTime;
    }

    public void ZeroingMoodTypeTime()
    {
        for (int i = 0; i < 3; i++)
        {
            moodType_time[i] = 0;
        }
    }

    public int GetMoodTypeTime()
    {
        float buf_time = 0;

        for (int i = 0; i < 3; i++)
        {
            buf_time += moodType_time[i];
        }

        buf_time *= UnityEngine.Random.value;
        moodType_time[1] += moodType_time[0];


        if (buf_time <= moodType_time[0])
        {
            return 0;
        }
        else if (buf_time < moodType_time[1])
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
}
