using System;
using System.Collections.Generic;
using UnityEngine;
using Searching;

namespace Places
{
    public sealed class Place : MonoBehaviour
    #region IInitialization
#if UNITY_EDITOR
    , IInitialization
#endif
    #endregion
    {
        public Vector3 Destination => _destination;
        public Vector3 SightGoal => _sightGoal;
        public bool Busy => _busy;


        [SerializeField]
        private Vector3 _destination;

        [SerializeField]
        private Vector3 _sightGoal;

        private bool _busy;


        #region Initializator
#if UNITY_EDITOR

        public bool AutoInitializate => false;

        public void Initializate()
        {
            _destination = SIC.Component(this.transform, "Destination").position;
            _sightGoal = SIC.Component(this.transform, "Sight Goal").position;
        }
#endif
        #endregion



        public void SetBusy(bool option)
        {
            _busy = option;
        }
    }
}
