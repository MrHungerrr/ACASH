using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI.Scholars
{
    public class ScholarSight : MonoBehaviour
    {
        public float Angle => _sightTransform.rotation.eulerAngles.z;

        private bool _active;
        private float _sightGoal;
        private float _anglePerSecond;

        [SerializeField] private Transform _sightTransform;


        public void SetAngularSpeed(float anglePerSecond)
        {
            _anglePerSecond = anglePerSecond;
        }

        public void SetSightDirection(in Vector2 direction)
        {
            var angle = Vector2.Angle(Vector2.up, direction);

            if (direction.x > 0)
                angle = 360 - angle;

            SetSightAngle(angle);
        }

        public void SetSightAngle(float angle)
        {
            _sightGoal = angle;
            _active = true;
        }

        public void SetRotation(float angle)
        {
            _sightTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        public void MyUpdate()
        {
            if (_active)
            {
                Rotating();
            }
        }


        private void Rotating()
        {
            float deltaAngle = _anglePerSecond * Time.deltaTime;
            var difference = _sightGoal - Angle;

            if (Mathf.Abs(difference) > 180)
            {
                difference -= 360 * Math.Sign(difference);
            }

            if (deltaAngle > Math.Abs(difference))
            {
                AddAngle(difference);
                _active = false;
                return;
            }

            deltaAngle *= Math.Sign(difference);
            AddAngle(deltaAngle);
        }


        private void AddAngle(float deltaAngle)
        {
            SetRotation(Angle + deltaAngle);
        }
    }
}
