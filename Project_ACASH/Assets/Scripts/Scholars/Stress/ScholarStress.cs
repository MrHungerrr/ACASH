using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScholarOptions;

public class ScholarStress
{


    private Scholar Scholar;

    //Стресс и настроение

    public float value { get; private set; }
    public int value_show { get; private set; }
    private const float change_time = 0.075f;
    private float time;


    public int threshold_1 { get; private set; } = 33;
    public int threshold_2 { get; private set; } = 66;
    private GetS.mood mood;



    //Stress Distance
    private float distance_1 = 0.4f;
    private float distance_2 = 2f;


    public ScholarStress(Scholar s)
    {
        Scholar = s;
        Reset();
    }


    public void Update()
    {
        if (!Scholar.disabled)
            ScholarStressDistance.Update(Scholar);

        Changing();
    }


    public void Reset()
    {
        Set(0f);
    }


    private void Set(float value)
    {
        this.value = value;
        if (this.value > 100f)
            this.value = 100f;
        if (this.value < 0f)
            this.value = 0f;

        Scholar.TextBox.StressLevel((int) this.value);
        ChangeMood();
    }

    public void Change(float value)
    {
        Set(this.value + value);
    }




    private void Changing()
    {
        if((int) value != value_show)
        {
            if(time > 0)
            {
                time -= Time.deltaTime;
            }
            {
                time = change_time + time;

                if (value > value_show)
                    value_show++;
                else
                    value_show--;
            }
        }
    }





    public void ChangeMood()
    {
        if (value < threshold_1)
            mood = GetS.mood.Chill;
        else if (value < threshold_2)
            mood = GetS.mood.Normal;
        else
            mood = GetS.mood.Panic;
    }



    public GetS.mood GetMoodType()
    {
        return mood;
    }

    /*

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

        */
}
