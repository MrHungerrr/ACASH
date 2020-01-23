using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BaseMath
{
    public static bool Probability(double a)
    {
        double rnd = Random.value;

        if (a >= rnd)
            return true;
        else
            return false;
    }
}
