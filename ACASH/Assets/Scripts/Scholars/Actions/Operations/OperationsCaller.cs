using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationsCaller
{
    private string action;
    private Operation[] operations;

    public void SetAction(string key)
    {
        action = key;
        operations = ActionsOperations.operations[key];
    }


}
