using System.Collections;
using UnityEngine;

public class ScholarActions
{
    private OperationsCaller
    private OperationsExecuter Executer;
    private ActionsWriting Writing;
    public ActionsQueue Queue { get; private set; }



    [HideInInspector]
    public string key_action;
    [HideInInspector]
    public string last_key_action;


    [HideInInspector]
    public Vector3 home;
    [HideInInspector]
    public Vector3 desk;



    public ScholarActions(Scholar scholar)
    {
        Executer = scholar.GetComponent<OperationsExecuter>();
        Executer.SetOperationsExecuter(scholar);
        Queue = new ActionsQueue();
        key_action = null;
    }



    private void NextAction()
    {
        key_action = Queue.GetAction();

        if(key_action != null)
        {
            StartDoing();
        }
        else
        {
            Writing.GetWritingActions();
            NextAction();
        }
    }


    private void StartDoing()
    {
        Executer.Do(key_action);
    }

    public void Continue()
    {
        if (last_key_action != null)
            Executer.Continue(last_key_action);
        else
            NextAction();
    }


    public void EndOfDoing()
    {
        key_action = "Nothing";
        last_key_action = null;
        NextAction();
    }
    

    public void Stop()
    {
        Executer.Stop();

        if (key_action != "Nothing")
            last_key_action = key_action;
        else
            last_key_action = null;

        key_action = "Nothing";
    }

}

