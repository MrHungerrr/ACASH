using System.Collections;
using UnityEngine;

public class ScholarActions
{
    private OperationsManager Operations { get; }
    private ActionsSimple Simple { get; }
    private ActionsQueue Queue { get; }


    [HideInInspector]
    public string key_action;



    public ScholarActions(Scholar scholar)
    {
        Operations = new OperationsManager(scholar, this);
        Simple = new ActionsSimple();
        Queue = new ActionsQueue();
        key_action = null;
    }




    public void Reset()
    {
        Queue.Reset();
        NextAction();
    }

    public void Reset(string next_action)
    {
        Queue.Reset();
        Queue.Add(next_action);
        NextAction();
    }

    private void NextAction()
    {
        key_action = Queue.GetAction();

        if(key_action != null)
        {
            Debug.Log("Пошло действие - " + key_action);
            DoAction(key_action);
        }
        else
        {
            AddAction(Simple.GetActions());
            NextAction();
        }
    }


    public void AddAction(string action)
    {
        Queue.Add(action);
    }


    public void DoAction(string action)
    {
        Operations.SetAction(action);
    }


    public void Continue()
    {
        Operations.Continue();
    }


    public void ActionDone()
    {
        Debug.Log("Опа, закончилось - " + key_action);
        NextAction();
    }
    

    public void Stop()
    {
        Operations.Stop();
    }

}

