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
    public class ScholarMove: AIMoveController
    {
        private Scholar _scholar;

        public void Setup(Scholar scholar)
        {
            base.Setup();
            _scholar = scholar;
        }
    }
}
