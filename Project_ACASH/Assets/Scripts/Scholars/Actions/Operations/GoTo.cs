using UnityEngine;
using System.Collections;
using Places;

namespace Operations
{
    public class GoTo: Operation
    {
        protected readonly Place place;


        public GoTo(Place place): base(GetO.operation.Go_To)
        {
            this.place = place;
        }

        public override void Do(OperationsExecuter executer)
        {
            executer.GoTo(place);
        }

        public override string Show()
        {
            return "Go to " + place.ToString();
        }

    }
}