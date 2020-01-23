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
            Debug.Log("Учитель наезжает");
            Scholar.Stress.Change(10);
            Scholar.Emotions.ChangeEmotion("upset", "ussual", 4f);
        }
        else
        {
            Debug.Log("Учитель прикалывается");
            Scholar.Emotions.ChangeEmotion("happy", "smile", 4f);
        }

        Scholar.Answer(key, 1, 1);
    }



    //Наезд учителя за какие-то предметы

    public void BullingForSubjects(string key, string obj)
    {
        Debug.Log("Учитель наезжает");
        Scholar.Stress.Change(10);
        Scholar.Emotions.ChangeEmotion("upset", "ussual", 4f);
        Scholar.Answer(key, obj, 0, 1);
    }

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
        Scholar.StartWrite();
    }

    public void Writing()
    {
        Scholar.WritingTest(UnityEngine.Random.value * 100);
    }

    public void CheatNeed()
    {
        if (CheatProbability())
        {
            Scholar.cheatNeed = true;
            ScholarManager.get.cheating_count++;
            CheatingSelection();
            Debug.Log("Я хочу списать");
        }
        else
        {
            Debug.Log("Я не хочу списывать");
        }
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


    public void CanCheat()
    {
        switch (cheat_check)
        {
            case "Outside":
                {
                    int buf = UnityEngine.Random.Range(0, 1);

                    switch (buf)
                    {
                        case 0:
                            {
                                Scholar.Do("Cheating_Check_1");
                                break;
                            }
                    }
                    break;
                }
        }

    }


    public void CheatingSelection()
    {
        int buf = UnityEngine.Random.Range(0, 1);

        switch (buf)
        {
            case 0:
                {
                    Scholar.Action.cheat_string = "Cheating_1";
                    cheat_check = "Outside";
                    Scholar.cheat_finish_type = 1;
                    break;
                }
        }
    }

    public void RandomSimpleAction()
    {
        int buf = UnityEngine.Random.Range(0, 1);

        switch (buf)
        {
            case 0:
                {
                    Scholar.StartWrite();
                    break;
                }
        }
    }

    public void RandomSpecialAction()
    {
        int buf = UnityEngine.Random.Range(0, 3);
        Debug.Log("Рандомное Специальное действие");

        switch (buf)
        {
            case 0:
                {
                    Scholar.Do("Toilet_1");
                    break;
                }
            case 1:
                {
                    Scholar.Do("Sink_1");
                    break;
                }
            case 2:
                {
                    Scholar.Do("Air_1");
                    break;
                }
        }
    }
}
