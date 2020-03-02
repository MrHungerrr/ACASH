using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class ScholarActionTime : Singleton<ScholarActionTime>
{
    const int length_of_points = 8;

    int[] time_points = new int[length_of_points];
    int index;




    public void Setup()
    {
        for(int i = 0; i < length_of_points/2; i++)
        {
            time_points[i] = Random.Range(15, 45);
            time_points[i + 4] = 60 - time_points[i];
        }

        time_points[length_of_points/2] += 30;

        for(int i = 1; i < length_of_points; i++)
        {
            time_points[i] += time_points[i - 1];
        }
    }


    private void Update()
    {
        if(ExamManager.get.exam)
        {
            Count();   
        }
    }


    private void Count()
    {
        if(index < length_of_points)
        {
            if(TimeManager.get.time_passed >= time_points[index])
            {
                Action(index / 2);
                index++;
            }
        }
    }



    private void Action(int wave)
    {
        Debug.Log("Пошел Экшон!");

        ActionsGiver.GiveActions(GetActionsCount.Wave(wave));
    }

}