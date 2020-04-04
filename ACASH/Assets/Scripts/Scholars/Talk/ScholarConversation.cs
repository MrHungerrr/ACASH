using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScholarOptions;

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
        Scholar.Emotions.Change(GetS.faces.Suprised);

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
            Scholar.Stress.Change(30);
            Scholar.Emotions.Change(GetS.faces.Upset, GetS.faces.Ussual, 4f);
        }
        else
        {
            Scholar.Stress.Change(10);
            Scholar.Emotions.Change(GetS.faces.Happy, GetS.faces.Smile, 4f);
        }

        Answer(key);
    }



    public void Answer(KeyWord key)
    {
        bool teacherIsRight = IsTeacherBullingRight();

        if(teacherIsRight)
            Scholar.Action.Skip();

        key.Answer(teacherIsRight);

        Scholar.Talk.Say(key);
    }



    //========================================================================================================
    //Прав ли учитель?

    public bool IsTeacherBullingRight()
    {
        switch (Scholar.View.GetView())
        {
            case ScholarCheat.reason.Cheating:
                {
                    if (Scholar.Cheat.cheating)
                        return true;
                    else
                        return false;
                }
            case ScholarCheat.reason.Talking:
                {
                   /* if (Scholar.Talk.talking)
                        return true;
                    else
                    */
                        return false;
                }
            case ScholarCheat.reason.Walking:
                {
                    /*
                    if (Scholar.Move.walking) или на улице)
                    return true;
                    else
                    */
                        return false;
                }
        }
        return false;
    }



    public void Shout()
    {
        Scholar.Stress.Change(20);
        Scholar.Emotions.Change(GetS.faces.Suprised, GetS.faces.Upset, GetS.faces.Ussual, 2f);
    }

}
