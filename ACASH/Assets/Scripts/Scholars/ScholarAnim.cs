using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarAnim
{

    private Animator Anim;
    [HideInInspector]
    public string anim = "AnimNumber";

    [HideInInspector]
    public Dictionary<string, int> animations = new Dictionary<string, int>()
    {
        { "Nothing", 0},
        { "Walking", 1},
        { "Writing", 2},
        { "Cheating", 3},
    };


    public ScholarAnim(Animator a)
    {
        Anim = a;
    }


    public void SetAnimation(string type)
    {
        /*
        try
        {
            Anim.SetInteger(anim, animations[type]);
        }
        catch
        {
            Debug.Log("Отсутсвует анимация - " + type);
        }
        */
    }
}
