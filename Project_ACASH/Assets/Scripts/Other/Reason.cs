using System;
using System.Collections.Generic;

public class Reason
{
    List<Type> reasons;

    public bool GiveMeChance => reasons.Count == 0;


    public Reason()
    {
        reasons = new List<Type>();
    }

    public void Add(Type type)
    {
        reasons.Add(type);
    }

    public void Remove(Type type)
    {
        if(reasons.Contains(type))
        {
            reasons.Remove(type);
        }
    }

}
