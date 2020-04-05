using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
///  Скрипт не может принимать разные скрипты одного и того же типа. НАДО ЭТО РЕШИИТЬ!!!!!!!!!!!!!!!!!
/// </summary>
public class Reasons
{
    List<Type> reasons = new List<Type>();

    public Reasons()
    {
        Reset();
    }

    public void Reset()
    {
        reasons = new List<Type>();
    }

    public void Add(Type type)
    {
        if (!reasons.Contains(type))
        {
            reasons.Add(type);

            Debug.LogError("+1 Причина - " + type);
        }
    }

    public void Remove(Type type)
    {
        if (reasons.Contains(type))
        {
            reasons.Remove(type);

            Debug.LogError("-1 Причина - " + type);
        }
    }


    public bool GiveMeChance()
    {
        if (reasons.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
