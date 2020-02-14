using System;
using System.Collections.Generic;
using UnityEngine;
using Animations;

public class ScholarAnim
{

    private Animator Anim;


    [HideInInspector]
    public Dictionary<GetA.animations, int> animations = new Dictionary<GetA.animations, int>()
    {
        { GetA.animations.Nothing, 0},
        { GetA.animations.Walking, 1},
        { GetA.animations.Writing, 2},
        { GetA.animations.Thinking, 3},
        { GetA.animations.Thinking_Aloud, 4},
        { GetA.animations.Thinking_Outside, 5},
        { GetA.animations.Washing_Hands, 6},
    };


    public ScholarAnim(Animator a)
    {
        Anim = a;
        SetAnimation(GetA.animations.Nothing);
    }


    public void SetAnimation(GetA.animations type)
    {
        try
        {
            Debug.Log("Играет анимация - <color=#003366>" + type + "</color>");
            Anim.SetInteger(GetA.anim, animations[type]);
        }
        catch
        {
            Debug.Log("<color=#ff00ff>Отсутсвует анимация - " + type + "</color>");
        }
        
    }
}
