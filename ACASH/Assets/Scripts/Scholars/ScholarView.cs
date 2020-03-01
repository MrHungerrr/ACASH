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

    public Dictionary<ScholarCheat.reason, bool> remarks = new Dictionary<ScholarCheat.reason, bool>()
    {
        { ScholarCheat.reason.Walking, false },
        { ScholarCheat.reason.Cheating, false },
        { ScholarCheat.reason.Talking, false },
    };









    //=================================================================================================================================================
    //Как выглядит то что делает ученик

    public ScholarCheat.reason GetView()
    {
        if (Scholar.Talk.talking)
        {
            return ScholarCheat.reason.Talking;
        }
        else if (Scholar.Move.walking)
        {
            return ScholarCheat.reason.Walking;
        }
        else
        {
            return ScholarCheat.reason.Cheating;
        }
    }


    public bool GetRemarksOnView()
    {
        bool answer = remarks[GetView()];
        remarks[GetView()] = true;
        return answer;
    }
}
