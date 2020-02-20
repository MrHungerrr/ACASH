using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarView
{
    private Scholar Scholar;

    public enum view
    {
        Walking,
        Cheating,
        Talking
    }

    private view now_view;


    public ScholarView(Scholar Scholar)
    {
        this.Scholar = Scholar;
    }

    //Список замечаний, которые уже были сделаны.

    public Dictionary<string, bool> remarks = new Dictionary<string, bool>()
    {
        { "Talking", false },
        { "Cheating", false },
        { "Walking", false },
    };


    //Список причин, по которым можно удалить ученика

    public Dictionary<string, bool> reason = new Dictionary<string, bool>()
    {
        { "Walking", false },
        { "Talking", false },
        { "Cheating", false },
    };







    //=================================================================================================================================================
    //Как выглядит то что делает ученик

    public string GetView()
    {
        if (Scholar.Talk.talking)
        {
            return "Talking";
        }
        else if (Scholar.Move.walking)
        {
            return "Walking";
        }
        else
        {
            return "Cheating";
        }
    }


    public bool GetRemarksOnView()
    {
        bool answer = remarks[GetView()];
        remarks[GetView()] = true;
        return answer;
    }
}
