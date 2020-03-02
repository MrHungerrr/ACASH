using UnityEngine;
using System.Collections;

public class ScholarExecute
{

    private Scholar Scholar;
    public bool executed { get; private set; }
    public KeyWord execute_key;


    public ScholarExecute(Scholar Scholar)
    {
        executed = false;
        this.Scholar = Scholar;
    }


    public void Execute(KeyWord key)
    {
        executed = true;
        execute_key = key;
        EndExam();
    }

    public void EndExam()
    {
        Scholar.Select.Selectable(false);
        Scholar.Disable();
        Scholar.Action.DoAction("Execute");
    }

}