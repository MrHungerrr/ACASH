using System.Collections;
using UnityEngine;

public class ScholarActions
{
    private OperationsManager Operations { get; }
    private ActionsSimple Simple { get; }
    private ActionsQueue Queue { get; }


    [HideInInspector]
    public string key_action;
    private bool active;
    private bool skip;



    public ScholarActions(Scholar scholar)
    {
        Operations = new OperationsManager(scholar, this);
        Simple = new ActionsSimple();
        Queue = new ActionsQueue();
        key_action = null;
        active = false;
        skip = false;
    }



    public void Reset()
    {
        Queue.Reset();
        active = true;
        NextAction();
    }

    public void Reset(string next_action)
    {
        Queue.Reset();
        active = true;
        Queue.Add(next_action);
        NextAction();
    }

    public void ResetFirst()
    {
        skip = true;
    }

    private void NextAction()
    {
        if (active)
        {
            key_action = Queue.GetAction();

            if (key_action != null)
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
    }


    public void AddAction(string action)
    {
        Queue.Add(action);
    }


    public void DoAction (string action)
    {
        Operations.SetAction(action);
    }


    public void Continue()
    {
        active = true;

        if (skip)
        {
            skip = false;
            NextAction();
        }
        else
            Operations.Continue();
    }


    public void ActionDone()
    {
        Debug.Log("Опа, закончилось - " + key_action);
        NextAction();
    }


    public void Enable()
    {
        active = true;

        if(Operations.done)
            NextAction();
    }

    public void Disable()
    {
        active = false;
    }

    public void Pause()
    {
        Operations.Stop();
        Disable();
    }

}

