using UnityEngine;
using System;

public class Dumb
{
    [HideInInspector]
    public Scholar scholar;
    private string cheat_check;

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

    public void StopQuestion()
    {
        scholar.asking = false;
        scholar.TextBox.Clear();
        scholar.Emotions.ChangeEmotion("upset", "ussual", 4f);
        scholar.StartWrite();
    }

    public void Writing()
    {
        scholar.WritingTest(UnityEngine.Random.value * 100);
    }

    public void CheatNeed()
    {
        if (CheatProbability())
        {
            scholar.cheatNeed = true;
            scholar.ScholarMan.cheating_count++;
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
        switch (scholar.GetMoodTypeTime())
        {
            case 0:
                {
                    if (scholar.ScholarMan.Probability(0.75))
                        return true;
                    break;
                }
            case 1:
                {
                    if (scholar.ScholarMan.Probability(0.5))
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
                                scholar.Do("Cheating_Check_1");
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
                    scholar.Action.cheat_string = "Cheating_1";
                    cheat_check = "Outside";
                    scholar.cheat_finish_type = 1;
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
                    scholar.StartWrite();
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
                    scholar.Do("Toilet_1");
                    break;
                }
            case 1:
                {
                    scholar.Do("Sink_1");
                    break;
                }
            case 2:
                {
                    scholar.Do("Air_1");
                    break;
                }
        }
    }
}
