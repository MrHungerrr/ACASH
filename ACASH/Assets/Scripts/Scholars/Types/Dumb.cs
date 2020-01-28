using UnityEngine;
using System;

public class Dumb
{
    [HideInInspector]
    public Scholar Scholar;
    private string cheat_check;

    public Dumb(Scholar p)
    {
        Scholar = p;
    }



    //Преждевременная реакция на услышынное

    public void HearBulling(bool strong)
    {
        if (strong)
        {
            Scholar.Emotions.ChangeEmotion("suprised");
        }
        else
        {
            Scholar.Emotions.ChangeEmotion("suprised");
        }
    }



    //Наезд учителя за списывание/поведение

    public void Bulling(string key, bool strong)
    {
        if (strong)
        {
            Scholar.Stress.Change(10);
            Scholar.Emotions.ChangeEmotion("upset", "ussual", 4f);
        }
        else
        {
            Scholar.Emotions.ChangeEmotion("happy", "smile", 4f);
        }

        Scholar.Answer(key, 1, 1);
    }



    //Наезд учителя за какие-то предметы

    public void TeacherAnswer(string key, bool answer)
    {
        if (answer)
        {
            Scholar.SayWithoutContinue(key);
            Scholar.Emotions.ChangeEmotion("happy", "smile", 4f);
        }
        else
        {
            Scholar.SayWithoutContinue(key);
            Scholar.Emotions.ChangeEmotion("upset", "ussual", 4f);
        }
    }

    public void StopQuestion()
    {
        Scholar.asking = false;
        Scholar.TextBox.Clear();
        Scholar.Emotions.ChangeEmotion("upset", "ussual", 4f);
        Scholar.Action.StartWriting();
    }


    public void Writing()
    {
        Scholar.Test.WritingTest(UnityEngine.Random.value * 100);
    }

    public void CheckForTeacher()
    {
        Scholar.Action.Doing("Dumb_Check_For_Teacher");
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
