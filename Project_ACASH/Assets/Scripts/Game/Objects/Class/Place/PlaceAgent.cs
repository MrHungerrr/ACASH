using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Searching;

namespace Places
{
    [RequireComponent(typeof(SortAgent))]
    public class PlaceAgent: MonoBehaviour
    #region IInitialization
#if UNITY_EDITOR
    , IInitialization
#endif
    #endregion
    {

        #region Initializator
#if UNITY_EDITOR

        public bool AutoInitializate => false;

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
                        var places = SIC<Place>.ComponentsDown(transform);
                        SortPlaces(places);
                        ArrayToDictionary();
                        break;
                    }
                case FillType.Manual:
                    {

                        break;
                    }
            }

        }


        private void SortPlaces(Place[] places)
        {
            List<Place> desks = new List<Place>();
            List<Place> toilets = new List<Place>();
            List<Place> sinks = new List<Place>();
            List<Place> outside = new List<Place>();
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
                    case "Toilet":
                        {
                            toilets.Add(place);
                            break;
                        }
                    case "Sink":
                        {
                            sinks.Add(place);
                            break;
                        }
                    case "Outside":
                        {
                            outside.Add(place);
                            break;
                        }
                    case "Dock Station":
                        {
                            dockStations.Add(place);
                            break;
                        }
                }
            }

            _desks = desks.ToArray();
            _toilets = toilets.ToArray();
            _sinks = sinks.ToArray();
            _outside = outside.ToArray();
            _dockStations = dockStations.ToArray();

            var sortAgent = GetComponent<SortAgent>();
            sortAgent.Sort(_desks);
            sortAgent.Sort(_dockStations);
        }

#endif
        #endregion

        public IReadOnlyDictionary<PlaceManager.place, Place[]> Places => _places;


        private Dictionary<PlaceManager.place, Place[]> _places;

        [SerializeField] private Place[] _desks;

        [SerializeField] private Place[] _toilets;

        [SerializeField] private Place[] _sinks;

        [SerializeField] private Place[] _outside;

        [SerializeField] private Place[] _dockStations;



        public void SetLevel()
        {
            ArrayToDictionary();
        }


        private void ArrayToDictionary()
        {
            _places = new Dictionary<PlaceManager.place, Place[]>();

            _places.Add(PlaceManager.place.Desk, _desks);
            _places.Add(PlaceManager.place.Toilet, _toilets);
            _places.Add(PlaceManager.place.Sink, _sinks);
            _places.Add(PlaceManager.place.Outside, _outside);
            _places.Add(PlaceManager.place.DockStation, _dockStations);
        }




        public Place GetPlace(PlaceManager.place placeType, int index)
        {
            try
            {
                return _places[placeType][index];
            }
            catch
            {
                throw new Exception("Выход за границы массива");
            }
        }

        public Place GetRandomFreePlace(PlaceManager.place placeType)
        {
            return _places[placeType].GetRandomFreePlace();
        }

        public bool HasFreePlace(PlaceManager.place placeType)
        {
            return _places[placeType].HasFreePlace();
        }
    }
}