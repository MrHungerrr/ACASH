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
    public class AIMove
    {
        public event Action OnDestinationReached;
        public Vector2 Direction => _direction;
        public float Distance => _distance;
        public Rigidbody2D RB => _rb;


        private Rigidbody2D _rb;
        private Vector2 _destination;
        private Vector2 _direction;
        private float _distance;
        private float _speed;
        private float _stopDistance;
        private float _regularLinearDrag;


        public AIMove(Rigidbody2D rb)
        {
            _rb = rb;
            _regularLinearDrag = _rb.drag;
        }

        public void FixUpdate()
        {
            Moving();
        }

        public void SetDestination(in Vector2 destination)
        {
            _destination = destination;

            var destinationVector = _destination - _rb.position;
            _distance = destinationVector.magnitude;
            _direction = destinationVector.normalized;

            _rb.drag = _regularLinearDrag;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void SetStopDistance(float stopDistance)
        {
            _stopDistance = stopDistance;
        }


        public void Teleport(in Vector2 position)
        {
            _rb.position = position;
        }


        public void Stop()
        {
            _rb.drag *=  5;
        }



        private void Moving()
        {
            var destinationVector = _destination - _rb.position;
            _distance = destinationVector.magnitude;
            _direction = destinationVector.normalized;

            if (_distance < _stopDistance)
            {
                DestinationReached();
                return;
            }

            var force = _direction * Time.fixedDeltaTime * _speed;
            _rb.AddForce(force);

            Debug.Log("Moving");
        }


        private void DestinationReached()
        {
            Stop();
            OnDestinationReached?.Invoke();
        }
    }
}
