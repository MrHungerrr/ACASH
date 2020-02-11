using UnityEngine;
using System;

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
        switch (Scholar.Stress.GetMoodTypeTime())
        {
            case 0:
                {
                    if (BaseMath.Probability(0.75))
                        return true;
                    break;
                }
            case 1:
                {
                    if (BaseMath.Probability(0.5))
                        return true;
                    break;
                }
            case 2:
                {
                    break;
                }
            default:
                {
                    Debug.Log("<color=#ff00ff>Ошибка настроения ученика</color>");
                    break;
                }
        }

        return false;
    }
}
