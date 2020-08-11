using UnityEngine;
using System.Collections;

namespace Operations
{
    public class Computer : Operation
    {
        private string program { get; }
        private int option;


        public Computer(GetO.computer program) : base(GetO.operation.Computer)
        {
            this.program = program.ToString();
            this.option = 0;
        }

        public Computer(GetO.computer_spec program, int option) : base(GetO.operation.Computer)
        {
            this.program = program.ToString();
            this.option = option;
        }


        public override void Do(OperationsExecuter executer)
        {
            if (option == 0)
                executer.Do(operation + '_' + program);
            else
                executer.Do(operation + '_' + program, option);
        }


        public override string Show()
        {
            return "Use Computer Program \"" + program +"\"";
        }

    }
}