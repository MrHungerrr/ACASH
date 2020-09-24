using AI.Scholars.GOAP;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Scholars.Actions
{
    public sealed class ScholarActionsPlanner
    {
        public event Action OnGoalChanged;

        private readonly Scholar _scholar;
        private ScholarGOAPPlanner.Goals _mainGoal;

        public ScholarActionsPlanner(Scholar scholar)
        {
            _scholar = scholar;
            Reset();
        }

        public void SetGoal(string goal)
        {
            ScholarGOAPPlanner.Goals item;

            if (!Enum.TryParse<ScholarGOAPPlanner.Goals>(goal, out item))
                throw new Exception($"Неизвествная цель: \"{goal}\"");

            SetGoal(item);
        }

        public void SetGoal(ScholarGOAPPlanner.Goals goal)
        {
            Debug.Log($"Goal Changed To \"{goal}\"");
            _mainGoal = goal;
            OnGoalChanged?.Invoke();
        }

        public void Reset()
        {
            SetGoal(ScholarGOAPPlanner.Goals.Idle);
        }

        public ScholarAction GetNextPlan()
        {
            return ScholarGOAPPlanner.Instance.GetPlan(_scholar, _mainGoal.ToString());
        }
    }
}
