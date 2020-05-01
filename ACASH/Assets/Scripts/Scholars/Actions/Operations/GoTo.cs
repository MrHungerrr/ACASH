using UnityEngine;
using System.Collections;

namespace Operations
{
    public class GoTo: Operation
    {
        protected  PlaceManager.place place { get; }
        protected int index { get; }


        public GoTo(PlaceManager.place place, int index): base(GetO.operation.Go_To)
        {
            this.place = place;
            this.index = index;
        }

        public GoTo(PlaceManager.place place) : base(GetO.operation.Go_To)
        {
            this.place = place;
            this.index = -1;
        }

        public override void Do(OperationsExecuter executer)
        {
            if (index != -1)
                executer.GoTo(place, index);
            else
                executer.GoTo(place);
        }

        public override string Show()
        {
            return "Go to " + place.ToString() + " " + index;
        }

    }
}