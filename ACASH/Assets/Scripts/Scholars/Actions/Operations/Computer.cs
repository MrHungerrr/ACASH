using UnityEngine;
using System.Collections;
using Questions;

namespace Operations
{
    public class Computer : Operation
    {
        private string program { get; }


        public Computer(GetO.computer program) : base(GetO.operation.Computer)
        {
            this.program = program.ToString();
        }


        public override void Do(OperationsExecuter executer)
        {
            executer.Do(operation + '_' + program);
        }


        public override string Show()
        {
            return "Use Computer Program \"" + program +"\"";
        }

    }
}