using AI.Tools.Move;
using Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace AI.Scholars
{
    public class ScholarMove_ASTAR: MonoBehaviour
    {
        public UnityEvent OnDestinationReached => _AIMove.OnDestinationReached;


        [SerializeField] private AIMove_ASTAR _AIMove;


        public void GoTo(in Vector2 destination)
        {
            _AIMove.SetDestination(destination);
        }
    }
}
