using UnityEngine;
using System.Collections;
using Questions;

namespace Operations
{
    public class Question : Operation
    {
        private string question { get; }


        public Question(GetQ.questions question) : base(GetO.operation.Question)
        {
            this.question = question.ToString();
        }


        public override void Do(OperationsExecuter executer)
        {
            executer.Do(operation, question);
        }

    }
}