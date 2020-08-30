using Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace AI.Tools.Move
{
    public class AIMove_ASTAR : AIPath
    {
        public UnityEvent OnDestinationReached { get; private set; } = new UnityEvent();

        public void SetDestination(in Vector2 destination)
        {
            SearchPath(destination);
        }

        public override void OnTargetReached()
        {
            OnDestinationReached.Invoke();
        }


    }
}
