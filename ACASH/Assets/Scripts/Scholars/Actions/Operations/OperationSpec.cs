using UnityEngine;
using System.Collections;

public class OperationSpec : Operation
{
    public string option { get; }


    public OperationSpec(string operation, string option) : base(operation)
    {
        this.option = option;
    }


    public override void Do(OperationsExecuter list)
    {
        list.Do(operation, option);
    }

}