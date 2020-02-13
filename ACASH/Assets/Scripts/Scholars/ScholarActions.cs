using System.Collections;
using UnityEngine;

public class ScholarActions
{
    private OperationsManager Operations { get; }
    private ActionsSimple Simple { get; }
    private ActionsQueue Queue { get;}


    [HideInInspector]
    public string key_action;



    public ScholarActions(Scholar scholar)
    {
        Operations = new OperationsManager(scholar);
        Simple = new ActionsSimple();
        Queue = new ActionsQueue();
        key_action = null;
    }



    private void NextAction()
    {
        key_action = Queue.GetAction();

        if(key_action != null)
        {
            DoAction(key_action);
        }
        else
        {
            Simple.GetActions();
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
        NextAction();
    }
    

    public void Stop()
    {
        Operations.Stop();
    }

}

