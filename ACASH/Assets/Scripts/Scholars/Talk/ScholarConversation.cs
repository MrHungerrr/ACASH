using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarConverastion
{

    private Scholar Scholar;


    public ScholarConverastion(Scholar Scholar)
    {
        this.Scholar = Scholar;
    }




    //========================================================================================================
    //Наезд на ученика

    public void HearTeacherTalking(bool strong)
    {
        Scholar.Emotions.ChangeEmotion("suprised");

        if (strong)
        {
            //Плохая реакция
        }
        else
        {
            //Хорошая реакция
        }
    }


    public void Listening(KeyWord key, bool strong)
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



    public void Answer(KeyWord key)
    {
        key.Answer(IsTeacherBullingRight());

        Scholar.Talk.Say(key);
    }



    //========================================================================================================
    //Прав ли учитель?

    public bool IsTeacherBullingRight()
    {
        switch (Scholar.View.GetView())
        {
            case "Cheating":
                {
                    if (Scholar.Cheat.cheating)
                        return true;
                    else
                        return false;
                }
            case "Talking":
                {
                    if (Scholar.Talk.talking)
                        return true;
                    else
                        return false;
                }
            case "Walking":
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
