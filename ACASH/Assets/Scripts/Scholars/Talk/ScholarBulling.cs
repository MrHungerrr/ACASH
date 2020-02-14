using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarBulling
{

    private Scholar Scholar;


    public ScholarBulling(Scholar Scholar)
    {
        this.Scholar = Scholar;
    }


    //========================================================================================================
    //Наезд на ученика

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

        Answer(key);
    }



    public void Answer(string key)
    {
        if (IsTeacherBullingRight())
        {
            Scholar.Talk.Say(key + "_Yes");
        }
        else
        {
            Scholar.Talk.Say(key + "_No");
        }
    }



    //========================================================================================================
    //Прав ли учитель?

    public bool IsTeacherBullingRight()
    {
        switch (Scholar.View.GetView())
        {
            case "Cheating_":
                {
                    if (Scholar.Cheat.cheating)
                        return true;
                    else
                        return false;
                }
            case "Talking_":
                {
                    if (Scholar.Talk.talking)
                        return true;
                    else
                        return false;
                }
            case "Walking_":
                {
                    if (Scholar.Move.walking) /*или на улице)*/
                        return true;
                    else
                        return false;
                }
        }
        return false;
    }



    public void Shout()
    {

    }

}
