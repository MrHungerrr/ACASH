using UnityEngine;
using System;

public class Dumb
{
    [HideInInspector]
    public Scholar scholar;

    public Dumb(Scholar p)
    {
        scholar = p;
    }



    //Преждевременная реакция на услышынное

    public void HearBulling(bool strong)
    {
        if (strong)
        {
            scholar.Emotions.ChangeEmotion("suprised");
        }
        else
        {
            scholar.Emotions.ChangeEmotion("suprised");
        }
    }



    //Наезд учителя за списывание/поведение

    public void Bulling(string key, bool strong)
    {
        if (strong)
        {
            Debug.Log("Учитель наезжает");
            scholar.Stress(10);
            scholar.Emotions.ChangeEmotion("upset", "ussual", 4f);
        }
        else
        {
            Debug.Log("Учитель прикалывается");
            scholar.Emotions.ChangeEmotion("happy", "smile", 4f);
        }

        scholar.Answer(key, 1, 1);
    }



    //Наезд учителя за какие-то предметы

    public void BullingForSubjects(string key, string obj)
    {
        Debug.Log("Учитель наезжает");
        scholar.Stress(10);
        scholar.Emotions.ChangeEmotion("upset", "ussual", 4f);
        scholar.Answer(key, obj, 0, 1);
    }




    public void TeacherPermission(string key, bool answer)
    {
        if (answer)
        {
            scholar.SayWithoutContinue(key);
            scholar.Emotions.ChangeEmotion("happy", "smile", 4f);
        }
        else
        {
            scholar.SayWithoutContinue(key);
            scholar.Emotions.ChangeEmotion("upset", "ussual", 4f);
        }
    }

    public void TeacherAnswer(string key, bool answer)
    {
        if (answer)
        {
            scholar.SayWithoutContinue(key);
            scholar.Emotions.ChangeEmotion("happy", "smile", 4f);
        }
        else
        {
            scholar.SayWithoutContinue(key);
            scholar.Emotions.ChangeEmotion("upset", "ussual", 4f);
        }
    }

    public void Writing()
    {
        scholar.WritingTest(UnityEngine.Random.value * 100);
    }

    public void CheatNeed()
    {
        scholar.cheatNeed = true;
    }

    public void CanCheat(int buf)
    {
        switch (buf)
        {
            case 0:
                {
                    break;
                }
            case 1:
                {
                    break;
                }
            case 2:
                {
                    break;
                }
        }
    }

    public void CheatingSelection(int buf)
    {
        switch (buf)
        {
            case 0:
                {
                    break;
                }
            case 1:
                {
                    break;
                }
            case 2:
                {
                    break;
                }
            case 3:
                {
                    break;
                }
            case 4:
                {
                    break;
                }
            case 5:
                {
                    break;
                }
        }
    }

    public void RandomAction(int buf)
    {
        switch (buf)
        {
            case 0:
                {
                    break;
                }
            case 1:
                {
                    break;
                }
            case 2:
                {
                    break;
                }
            case 3:
                {
                    break;
                }
            case 4:
                {
                    break;
                }
            case 5:
                {
                    break;
                }
        }
    }
}
