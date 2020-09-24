using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI.Scholars
{
    public class ScholarSightController : MonoBehaviour
    {
        public float Angle => _sight.Angle;


        private bool _active;
        private bool moving;
        private Scholar _scholar;

        [Header("Sight Object")]
        [SerializeField]private ScholarSight _sight;

        public void Setup(Scholar scholar)
        {
            _scholar = scholar;
            _sight.SetAngularSpeed(180);
        }



        public void SetSightGoal(Vector2 position)
        {
            var direction = position - _scholar.Move.Position;
            _sight.SetSightDirection(direction);
        }



        public void MyUpdate()
        {
            if(_scholar.Move.IsMoving)
            {
                _sight.SetSightDirection(_scholar.Move.Direction);
            }

            _sight.MyUpdate();
        }

        public void SetRotation(float angle)
        {
            _sight.SetRotation(angle);
        }

        public void LookAt(Vector2 position)
        {
            var direction = position - _scholar.Move.Position;
            var angle = Vector2.Angle(Vector2.up, direction);

            if (direction.x > 0)
                angle = 360 - angle;

            _sight.SetRotation(angle);
        }
    }
}
