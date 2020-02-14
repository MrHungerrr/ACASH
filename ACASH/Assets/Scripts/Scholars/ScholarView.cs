using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarView
{
    private Scholar Scholar;


    public ScholarView(Scholar Scholar)
    {
        this.Scholar = Scholar;
    }

    //Список замечаний, которые уже были сделаны.

    public Dictionary<string, bool> remarks = new Dictionary<string, bool>()
    {
        { "Talking_", false },
        { "Cheating_", false },
        { "Walking_", false },
    };


    //Список причин, по которым можно удалить ученика

    public Dictionary<string, bool> reason = new Dictionary<string, bool>()
    {
        { "Walking_", false },
        { "Talking_", false },
        { "Cheating_", false },
    };







    //=================================================================================================================================================
    //Как выглядит то что делает ученик

    public string GetView()
    {
        if (Scholar.Talk.talking)
        {
            return "Talking_";
        }
        else if (Scholar.Move.walking)
        {
            return "Walking_";
        }
        else
        {
            return "Cheating_";
        }
    }
}
