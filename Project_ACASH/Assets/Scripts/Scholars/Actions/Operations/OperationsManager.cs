using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Operations;

public class OperationsManager
{
    public Action OnOperationsEnd { get; set; }
    public bool Done { get; private set; }


    private OperationsExecuter _executer;
    private string _action;
    private int _operationNum;
    private Operation[] _operations;



    public OperationsManager(Scholar scholar)
    {
        _executer = scholar.transform.Find("Script Holder").GetComponent<OperationsExecuter>();
        _executer.SetOperationsExecuter(scholar, this);

        Reset();
    }


    public void SetAction(string key)
    {
        Stop();
        Done = false;
        _action = key;
        _operationNum = 0;
        try
        {
            _operations = OperationsList.operations[key];
            DoOperation();
        }
        catch
        {
            Debug.LogError("Несуществующий ключ - " + key);
            OperationsEnd();
        }
    }


    public void Reset()
    {
        Done = true;
        _action = "";
        _operationNum = 0;
        _operations = null;
    }


    private void DoOperation()
    {

        if (_operations != null && _operationNum < _operations.Length)
        {
            _operations[_operationNum].Do(_executer);
        }
        else
        {
            OperationsEnd();
            Done = true;
        }
    }

    public void Continue()
    {
        DoOperation();
    }

    public void Stop()
    {
        _executer.Stop();
    }

    public void OperationDone()
    {
        Debug.Log("Я сделал операцию - " + _operations[_operationNum].Show());
        _operationNum++;
        DoOperation();
    }


    public void OperationsEnd()
    {
        OnOperationsEnd?.Invoke();
    }







}
