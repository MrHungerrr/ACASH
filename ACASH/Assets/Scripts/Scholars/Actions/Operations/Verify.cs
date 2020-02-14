using UnityEngine;
using System.Collections;

namespace Operations
{
    public class Verify : Operation
    {
        private string verify;
        private bool need_condition { get; }

        public Verify(GetO.verify verify, bool need_condition) : base(GetO.operation.Verify)
        {
            this.verify = verify.ToString();
            this.need_condition = need_condition;
        }


        public override void Do(OperationsExecuter executer)
        {
            switch (verify)
            {
                case "Teacher_Is_Here":
                    {
                        executer.Verify(executer.VerifyTeacherIsHere, need_condition);
                        break;
                    }
                case "Answer":
                    {
                        executer.Verify(executer.VerifyAnswer, need_condition);
                        break;
                    }
            }
        }

        public override string Show()
        {
            return base.Show() + " that " + verify + " is " + need_condition;
        }

    }
}