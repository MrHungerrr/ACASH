using UnityEngine;
using System.Collections;

namespace Operations
{
    public class Special : Operation
    {
        public int index { get; }


        public Special(GetO.special operation, int option) : base(operation)
        {
            this.index = option;
        }

        public Special(GetO.special operation) : base(operation)
        {
            this.index = 1;
        }


        public override void Do(OperationsExecuter executer)
        {
            executer.Do(operation, index);
        }

        public override string Show()
        {
            return base.Show() + "(" + index + ")";
        }

    }
}