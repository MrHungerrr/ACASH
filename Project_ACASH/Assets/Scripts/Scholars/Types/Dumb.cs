using UnityEngine;
using System;
using ScholarOptions;



public class Dumb
{
    [HideInInspector]
    public Scholar Scholar;

    public Dumb(Scholar p)
    {
        Scholar = p;
    }


    public bool CheatProbability()
    {
        switch (Scholar.Stress.GetMoodType())
        {
            case GetS.mood.Chill:
                {
                    if (BaseMath.Probability(0.75))
                        return true;
                    break;
                }
            case GetS.mood.Normal:
                {
                    if (BaseMath.Probability(0.5))
                        return true;
                    break;
                }
            case GetS.mood.Panic:
                {
                    break;
                }
        }

        return false;
    }
}
