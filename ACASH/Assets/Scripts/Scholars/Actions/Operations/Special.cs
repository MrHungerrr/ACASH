using UnityEngine;
using System.Collections;

namespace Operations
{
    public class Special : Operation
    {
        public int option { get; }


        public Special(GetO.operation operation, int option) : base(operation)
        {
            this.option = option;
        }


        public override void Do(OperationsExecuter executer)
        {
            executer.Do(operation, option);
        }

    }
}