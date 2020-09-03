﻿using System;
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
        public Vector2 Destination => _destination.position;
        public Vector2 SightGoal => _sightGoal.position;
        public bool Busy => _busy;


        [SerializeField] private Transform _destination;

        [SerializeField] private Transform _sightGoal;

        private bool _busy = false;


        #region Initializator
#if UNITY_EDITOR

        public bool AutoInitializate => false;

        public void Initializate()
        {
            if (_destination == null)
                throw new ArgumentException();

            if (_sightGoal == null)
                throw new ArgumentException();
        }
#endif
        #endregion

        public void SetBusy(bool option)
        {
            _busy = option;
        }
    }
}