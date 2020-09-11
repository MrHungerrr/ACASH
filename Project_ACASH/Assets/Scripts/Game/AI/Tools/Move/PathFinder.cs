using Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI.Tools.Move
{
    public class PathFinder
    {
        public event Action OnPathCalculated;
        public bool IsPathCalculated { get; private set; }
        public bool ReachedEndOfPath
        {
            get
            {
                if (_path != null)
                    return _currentWaypoint >= _path.vectorPath.Count;
                else
                    throw new Exception("Путь еще не готов!");
            }
        }


        private int _currentWaypoint;
        private Vector2 _destination;
        private Rigidbody2D _rb;
        private Seeker _seeker;
        private Path _path;
 


        public PathFinder(Rigidbody2D host, Seeker seeker)
        {
            _rb = host;
            _seeker = seeker;
            Reset();
        }


        public void Reset()
        {
            IsPathCalculated = false;
            _currentWaypoint = 0;
            _path = null;
        }

        public void SetDestination(in Vector2 destination)
        {
            _destination = destination;
            FindPath();
        }

        public Vector2 GetNextWaypoint()
        {
            if (ReachedEndOfPath)
                throw new Exception("Конец пути уже достигнут!");

            Vector2 waypoint = _path.vectorPath[_currentWaypoint];
            _currentWaypoint++;

            return waypoint;
        }

        private void FindPath()
        {
            _currentWaypoint = 0;
            _seeker.StartPath(_rb.position, _destination, SetPath);
        }

        private void SetPath(Path path)
        {
            if (path.error)
                throw new Exception("Хуевая сетка у А*");

            _path = path;
            OnPathCalculated?.Invoke();
            IsPathCalculated = true;
        }
    }
}
