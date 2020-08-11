using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;
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

        public IReadOnlyDictionary<PlaceManager.place, Place[]> Places => _places;


        [SerializeField]
        private Dictionary<PlaceManager.place, Place[]> _places;

        [SerializeField]
        private Place[] _desks;

        [SerializeField]
        private Place[] _toilets;

        [SerializeField]
        private Place[] _sinks;

        [SerializeField]
        private Place[] _outside;

        [SerializeField]
        private Place[] _dockStations;



        #region Initializator
#if UNITY_EDITOR
        private enum FillType
        {
            Hierarchy,
            Manual
        }

        [SerializeField]
        private FillType fillType;


        public bool TryInitializate()
        {
            switch (fillType)
            {
                case FillType.Hierarchy:
                    {
                        try
                        {
                            var places = SIC<Place>.ComponentsDown(transform);
                            SortPlaces(places);

                            _places = new Dictionary<PlaceManager.place, Place[]>();

                            _places.Add(PlaceManager.place.Desk, _desks);
                            _places.Add(PlaceManager.place.Toilet, _toilets);
                            _places.Add(PlaceManager.place.Sink, _sinks);
                            _places.Add(PlaceManager.place.Outside, _outside);
                            _places.Add(PlaceManager.place.DockStation, _dockStations);
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                case FillType.Manual:
                    {
                        return true;
                    }
            }

            return false;
        }


        private void SortPlaces(Place[] places)
        {
            List<Place> desks = new List<Place>();
            List<Place> toilets = new List<Place>();
            List<Place> sinks = new List<Place>();
            List<Place> outside = new List<Place>();
            List<Place> dockStations = new List<Place>();


            foreach(var place in places)
            {
                place.TryInitializate();

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