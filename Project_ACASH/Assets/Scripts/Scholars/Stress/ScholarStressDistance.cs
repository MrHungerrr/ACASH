using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScholarOptions;

public static class ScholarStressDistance
{

    //Stress Distance
    private static float distance_1 = 0.4f;
    private static float distance_2 = 2f;


    public static void Update(Scholar Scholar)
    {
        StressDistance(Scholar);
    }


    private static void StressDistance(Scholar Scholar)
    {
        if (Scholar.Senses.T_here && InputManager.GameType != InputManager.GameplayType.Computer)
        {

            if (Scholar.Senses.Teacher.distance <= distance_1)
            {
                StressBehavior_1(Scholar);
            }
            else if (Scholar.Senses.Teacher.distance <= distance_2)
            {
                StressBehavior_2(Scholar);
            }
            else
            {
                StressBehavior_3(Scholar);
            }
        }
        else
        {
            StressBehavior_3(Scholar);
        }

    }




    private static void StressBehavior_1(Scholar Scholar)
    {
        if(Scholar.Senses.T_look_near_at_us)
        {
            StressRaise_High(Scholar);
        }
        else
        {
            StressDecrease_Low(Scholar);
        }
    }


    private static void StressBehavior_2(Scholar Scholar)
    {
        if (Scholar.Senses.T_look_at_us)
        {
            StressRaise_Low(Scholar);
        }
        else
        {
            StressDecrease_Low(Scholar);
        }
    }

    private static void StressBehavior_3(Scholar Scholar)
    {
        if (Scholar.Senses.T_look_at_us && Scholar.Senses.T_here && InputManager.GameType != InputManager.GameplayType.Computer)
        {
            StressRaise_Low(Scholar);
        }
        else
        {
            StressDecrease_Low(Scholar);
        }
    }



    private static void StressRaise_High(Scholar Scholar)
    {
        float res = (10f-12f*Scholar.Senses.Teacher.distance);


        if (res < 0)
        {
            res = 0;
        }

        //Debug.Log("Raise: " + res);

        Scholar.Stress.Change(res * Time.deltaTime);
    }


    private static void StressRaise_Low(Scholar Scholar)
    {
        float res = (-4f / 3f) * (Scholar.Senses.Teacher.distance - 2f)*2;

        if (res < 0)
        {
            res = 0;
        }

        //Debug.Log("Raise: " + res);

        Scholar.Stress.Change(res * Time.deltaTime);
    }


    private static void StressDecrease_High(Scholar Scholar)
    {
        float res;

        if (Scholar.Senses.Teacher.distance < 2)
            res = -2;
        else
            res = -Mathf.Pow(2 * (Scholar.Senses.Teacher.distance - 2), 2) - 2;


        if (res < -6)
        {
            res = -6;
        }

        //Debug.Log("Decrease: " + res);

        Scholar.Stress.Change(res * Time.deltaTime);
    }


    private static void StressDecrease_Low(Scholar Scholar)
    {
        float res = -Scholar.Senses.Teacher.distance + 0.5f;

        //Debug.Log("Decrease: " + res);

        Scholar.Stress.Change(res * Time.deltaTime);
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
