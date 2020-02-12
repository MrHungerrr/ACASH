using UnityEngine;
using System.Collections;

public class Operation
{

    public string operation { get;}

    public Operation(string operation)
    {
        this.operation = operation;
    }

    public virtual void Do(OperationsExecuter executer)
    {
        executer.Do(operation);
    }

}