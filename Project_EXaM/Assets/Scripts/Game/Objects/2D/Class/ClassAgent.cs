using System;
using System.Collections.Generic;
using Objects._2D.Places;
using UnityEngine;
using Vkimow.Unity.Tools.Search;
using AI.Scholars;
using Objects._2D.ClassRoom.GOAP;
using Objects._2D.Tools;
using GOAP;

namespace Objects._2D.ClassRoom
{
    [RequireComponent(typeof(PlaceAgent))]
    [RequireComponent(typeof(SortAgent2D))]
    public class ClassAgent : MonoBehaviour
    #region IInitialization
#if UNITY_EDITOR
    , IInitialization
#endif
    #endregion
    {

        #region Initializator
#if UNITY_EDITOR

        public bool AutoInitializate => true;

        private enum FillType
        {
            Hierarchy,
            Manual
        }

        [SerializeField]
        private FillType fillType;


        public void Initializate()
        {
            switch (fillType)
            {
                case FillType.Hierarchy:
                    {
                        _placeAgent = GetComponent<PlaceAgent>();
                        _placeAgent.Initializate();

                        _scholars = SIC<Scholar>.ComponentsDown(this.transform);

                        var places = SIC<Place>.ComponentsDown(transform);
                        SortPlaces(places);

                        break;
                    }
                case FillType.Manual:
                    {

                        break;
                    }
            }

            //if (_scholars.Length != _placeAgent.Places[PlaceManager.place.Desk].Length)
            //    throw new Exception($"Количество учеников - {_scholars.Length}, Количество парт - {_placeAgent.Places[PlaceManager.place.Desk].Length}");

            if (_scholars.Length != _dockStations.Length)
                throw new Exception($"Количество учеников - {_scholars.Length}, Количество Док-станций - {_dockStations.Length}");

            if (_scholars.Length != _desks.Length)
                throw new Exception($"Количество учеников - {_scholars.Length}, Количество Док-станций - {_desks.Length}");

            ScholarInitialize();
        }


        private void SortPlaces(Place[] places)
        {
            List<Place> desks = new List<Place>();
            List<Place> dockStations = new List<Place>();


            foreach (var place in places)
            {
                place.Initializate();

                switch (place.tag)
                {
                    case "Desk":
                        {
                            desks.Add(place);
                            break;
                        }
                    case "DockStation":
                        {
                            dockStations.Add(place);
                            break;
                        }
                }
            }

            _desks = desks.ToArray();
            _dockStations = dockStations.ToArray();

            var sortAgent = GetComponent<SortAgent2D>();
            sortAgent.Sort(_desks);
            sortAgent.Sort(_dockStations);
        }

        private void ScholarInitialize()
        {
            for (int i = 0; i < _scholars.Length; i++)
            {
                _scholars[i].Move.transform.position = _dockStations[i].Destination;
            }
        }

#endif
        #endregion

        public PlaceAgent PlaceAgent => _placeAgent;
        public IGOAPStateReadOnlyStorageList GoapContext { get; private set; }

        [SerializeField] private PlaceAgent _placeAgent;
        [SerializeField] private Scholar[] _scholars;
        [SerializeField] private Place[] _desks;
        [SerializeField] private Place[] _dockStations;



        public void SetupSchool()
        {
            GoapContext = new ClassGOAPContext(this);
            ScholarSetup();
        }


        private void ScholarSetup()
        {
            var scholarNumberDelta = 0;

            for (int i = 0; i < ClassManager.Instance.Classes.Length; i++)
            {
                if (this != ClassManager.Instance.Classes[i])
                    scholarNumberDelta += ClassManager.Instance.Classes[i]._scholars.Length;
                else
                    break;
            }

            for (int i = 0; i < _scholars.Length; i++)
            {
                _scholars[i].Setup(this, scholarNumberDelta + i, i);
            }
        }


        public Place GetMyDesk(Scholar scholar)
        {
            return _desks[scholar.Info.LocalIndex];
        }

        public Place GetMyDockStation(Scholar scholar)
        {
            return _dockStations[scholar.Info.LocalIndex];
        }
    }
}