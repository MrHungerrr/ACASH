﻿using System.Collections;
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


    public OperationsManager(Scholar scholar, ScholarActions action)
    {
        Action = action;
        Executer = scholar.transform.Find("Script Holder").GetComponent<OperationsExecuter>();
        Executer.SetOperationsExecuter(scholar, this);

        Reset();
    }


    public void SetAction(string key)
    {
        Stop();
        done = false;
        action = key;
        operation_num = 0;
        try
        {
            operations = OperationsList.operations[key];
            DoOperation();
        }
        catch
        {
            Debug.LogError("Несуществующий ключ - " + key);
        }
    }


    public void Reset()
    {
        done = true;
        action = "";
        operation_num = 0;
        operations = null;
    }


    private void DoOperation()
    {

        if (operations != null && operation_num < operations.Length)
        {
            operations[operation_num].Do(Executer);
        }
        else
        {
            ActionEnd();
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

    public void OperationDone()
    {
        Debug.Log("Я сделал операцию - " + operations[operation_num].Show());
        operation_num++;
        DoOperation();
    }


    public void ActionEnd()
    {
        Action.ActionDone();
    }







}
