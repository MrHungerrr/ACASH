﻿using UnityEngine;
using System.Collections;

namespace Operations
{
    public class Operation
    {

        protected string operation { get; }

        public Operation(GetO.operation operation)
        {
            this.operation = operation.ToString();
        }

        protected Operation(GetO.special operation)
        {
            this.operation = operation.ToString();
        }

        public virtual void Do(OperationsExecuter executer)
        {
            executer.Do(operation);
        }

        public virtual string Show()
        {
            return operation;
        }

    }
}