using Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace AI.Tools.Move
{
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class AIMoveController : MonoBehaviour
    {
        public bool IsMoving => _active;
        public event Action OnDestinationReached;
        public Vector2 Position => _move.RB.position;
        public Vector2 Direction => _move.Direction;

        private bool _active;
        private AIMove _move;
        private PathFinder _pathFinder;
        private CancellationTokenSource _cancel;

        [SerializeField] float _pickNextWaypointDistance;
        [SerializeField] float _stopDistance;
        [SerializeField] float _speed;


        protected void Setup()
        {
            var rb = GetComponent<Rigidbody2D>();
            _pathFinder = new PathFinder(rb, GetComponent<Seeker>());
            _cancel = new CancellationTokenSource();
            _move = new AIMove(rb);

            _move.SetSpeed(_speed);

            _pathFinder.OnPathCalculated += PathCalculated;
        }

        public void FixUpdate()
        {
            if (_active)
            {
                _move.FixUpdate();
            }
        }

        public void MyUpdate()
        {
            _move.Update();

            if (_active)
            {
                MoveCalculate();
            }
        }

        public void SetDestination(in Vector2 destination)
        {
            _cancel.Cancel();
            _cancel = new CancellationTokenSource();
            _pathFinder.SetDestination(destination, _cancel.Token);
        }

        public void SetPosition(in Vector2 position)
        {
            _move.Teleport(position);
        }

        public void Stop()
        {
            _active = false;
            _move.SlowDown();
        }

        private void MoveCalculate()
        {
            if (!_pathFinder.ReachedEndOfPath)
            {
                ShouldIMoveToNext();
            }
            else
            {
                ShouldIStop();
            }
        }

        private void ShouldIMoveToNext()
        {
            if (_move.Distance <= _pickNextWaypointDistance)
            {
                NextPoint();
            }
        }

        private void ShouldIStop()
        {
            if (_move.Distance <= _stopDistance)
            {
                DestinationReached();
            }
        }


        private void PathCalculated()
        {
            _active = true;
            NextPoint();
        }


        private void NextPoint()
        {
            var nextPoint = _pathFinder.GetNextWaypoint();
            _move.SetDestination(nextPoint);
        }

        private void DestinationReached()
        {
            _active = false;
            _move.SlowDown();
            _pathFinder.Reset();
            OnDestinationReached?.Invoke();
        }


        #region Gizmos
#if UNITY_EDITOR
        [SerializeField] bool _showGizmos;

        public void OnDrawGizmos()
        {
            if (_showGizmos)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(transform.position, _pickNextWaypointDistance);

                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.position, _stopDistance);
            }
        }
#endif
        #endregion
    }
}
