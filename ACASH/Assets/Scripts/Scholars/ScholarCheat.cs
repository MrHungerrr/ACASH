using UnityEngine;
using System;
using System.Collections.Generic;
using ScholarOptions;

public class ScholarCheat
{

    private Scholar Scholar;
    public bool cheating { get; private set; }


    public enum reason
    {
        Walking,
        Cheating,
        Talking
    }


    //Список причин, по которым можно удалить ученика

    public Dictionary<reason, bool> reasons = new Dictionary<reason, bool>()
    {
        { reason.Walking, false },
        { reason.Cheating, false },
        { reason.Talking, false },
    };

    public Dictionary<reason, bool> cheated = new Dictionary<reason, bool>()
    {
        { reason.Walking, false },
        { reason.Cheating, false },
        { reason.Talking, false },
    };


    public ScholarCheat(Scholar s)
    {
        Scholar = s;
    }


    public void Reset()
    {
        for(int i = 0; i < Enum.GetNames(typeof(reason)).Length; i++)
        {
            reason r = (reason)i;
            reasons[r] = false;
            cheated[r] = false;
        }

        cheating = false;
    }



    public void StartCheat()
    {
        cheating = true;
        reasons[reason.Cheating] = true;
    }


    public void EndCheat()
    {
        cheating = false;
        cheated[reason.Cheating] = true;
    }


    public bool IsTryToCheat()
    {
        foreach (KeyValuePair<reason, bool> pair in reasons)
        {
            if (pair.Value)
                return true;
        }

        return false;
    }

    public bool IsCheated()
    {
        foreach(KeyValuePair<reason, bool> pair in cheated)
        {
            if (pair.Value)
                return true;
        }

        return false;
    }




    public bool Probability()
    {
        switch(Scholar.Stress.GetMoodType())
        {
            case GetS.mood.Chill:
                {
                    return BaseMath.Probability(0.75);
                }
            case GetS.mood.Normal:
                {
                    return BaseMath.Probability(0.25);
                }
            case GetS.mood.Panic:
                {
                    return false;
                }
        }

        return false;
    }



    /*public bool Probability()
    {
        if (BaseMath.Probability((70f - Scholar.Stress.value)/100f))
            return true;
        else
            return false;
    }*/
}
