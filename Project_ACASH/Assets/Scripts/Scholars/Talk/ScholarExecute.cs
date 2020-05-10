﻿using UnityEngine;
using System.Collections;
using ScholarOptions;

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



    public void Reset()
    {
        executed = false;
    }

    public void Execute(KeyWord key)
    {
        executed = true;
        execute_key = key;
        EndExamForScholar();
    }

    public void LastWord()
    {
        Scholar.Talk.Stop();
        Scholar.Emotions.Change(GetS.faces.Dead);

        Scholar.Action.DoAction("Go_Home");
    }


    public void EndExamForScholar()
    {
        if (Scholar.active)
        {
            Scholar.Disable();
            Scholar.Action.DoAction("Execute");
        }
    }
}