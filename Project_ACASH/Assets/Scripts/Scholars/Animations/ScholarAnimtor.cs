using System;
using System.Collections.Generic;
using UnityEngine;
using Animations;

public class ScholarAnimtor
{
    public static IReadOnlyDictionary<Get.animations, int> Animations => ANIMATIONS;

    private static readonly Dictionary<Get.animations, int> ANIMATIONS = new Dictionary<Get.animations, int>()
    {
        { Get.animations.Nothing, 0},
        { Get.animations.Walking, 1},
        { Get.animations.Writing, 2},
        { Get.animations.Thinking, 3},
        { Get.animations.Take_Out, 4},
        { Get.animations.Take_Up, 5},
    };

    private Animator _animator;


    public ScholarAnimtor(Animator a)
    {
        _animator = a;
        SetAnimation(Get.animations.Nothing);
    }


    public void SetAnimation(Get.animations type)
    {
        try
        {
            //Debug.Log("Играет анимация - <color=#003366>" + type + "</color>");
            _animator.SetInteger(Get.anim, Animations[type]);
        }
        catch
        {
            Debug.Log("<color=#ff00ff>Отсутсвует анимация - " + type + "</color>");
        }
    }
}
