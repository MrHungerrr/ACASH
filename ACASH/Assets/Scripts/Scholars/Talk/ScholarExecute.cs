using UnityEngine;
using System.Collections;

public class ScholarExecute
{

    private Scholar Scholar;
    public bool executed { get; private set; }


    public ScholarExecute(Scholar Scholar)
    {
        this.Scholar = Scholar;
    }


    public void Execute(KeyWord key)
    {
        executed = true;
        Scholar.Select.Selectable(false);
        Scholar.Talk.Say(key);
        Scholar.Action.DoAction("Execute");
    }

}