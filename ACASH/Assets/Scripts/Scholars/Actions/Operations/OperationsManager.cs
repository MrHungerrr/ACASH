using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Operations;

public class OperationsManager
{
    private ScholarActions Action { get; }
    public OperationsExecuter Executer { get; }

    private string action;
    private int operation_num;
    private Operation[] operations;
    public bool done { get; private set; }


    public OperationsManager(Scholar scholar)
    {
        Action = scholar.Action;
        Executer = scholar.GetComponent<OperationsExecuter>();
        Executer.SetOperationsExecuter(scholar, this);
    }


    public void SetAction(string key)
    {
        done = false;
        action = key;
        operation_num = 0;
        operations = ActionsOperations.operations[key];

        DoOperation();
    }


    private void DoOperation()
    {
        if (operation_num < operations.Length)
        {
            operations[operation_num].Do(Executer);
        }
        else
        {
            EndOfAction();
            done = true;
        }
    }

    public void Continue()
    {
        DoOperation();
    }

    public void Stop()
    {
        Executer.Stop();
    }

    public void OpeartionDone()
    {
        operation_num++;
        DoOperation();
    }


    public void EndOfAction()
    {
        Action.ActionDone();
    }







}
