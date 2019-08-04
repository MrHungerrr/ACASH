using UnityEngine;
using System;

public class Dumb
{
    [HideInInspector]
    public Scholar parent;

    public Dumb(Scholar p)
    {
        parent = p;
    }

    //Преждевременная реакция на услышынное

    public void HearBulling(bool strong)
    {
        if (strong)
        {
            parent.Emotions.ChangeEmotion("suprised");
        }
        else
        {
            parent.Emotions.ChangeEmotion("suprised");
        }
    }


    //Наезд учителя за списывание/поведение

    public void Bulling(string key, bool strong)
    {
        if (strong)
        {
            Debug.Log("Учитель наезжает");
            parent.Stress(10);
            parent.Emotions.ChangeEmotion("upset", "ussual", 4f);
        }
        else
        {
            Debug.Log("Учитель прикалывается");
            parent.Emotions.ChangeEmotion("happy", "smile", 4f);
        }

        parent.Answer(key, 0, 1);
    }



    //Наезд учителя за какие-то предметы

    public void BullingForSubjects(string key, string obj)
    {
        Debug.Log("Учитель наезжает");
        parent.Stress(10);
        parent.Emotions.ChangeEmotion("upset", "ussual", 4f);
        parent.Answer(key, obj, 0, 1);
    }
}
