using Objects.Organization.ClassRoom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.UIElements;
using UnityEngine;
using Vkimow.Unity.Tools.Search;

namespace Objects.Organization.Places
{

    [RequireComponent(typeof(ClassAgent))]
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
                        _class = GetComponent<ClassAgent>();
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
            List<Place> toilets = new List<Place>();
            List<Place> sinks = new List<Place>();
            List<Place> hallway = new List<Place>();


            foreach (var place in places)
            {
                place.Initializate();

                switch (place.tag)
                {
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
                    case "Hallway":
                        {
                            hallway.Add(place);
                            break;
                        }
                }
            }

            _toilets = toilets.ToArray();
            _sinks = sinks.ToArray();
            _hallway = hallway.ToArray();
        }

#endif
        #endregion

        public event Action<PlaceManager.Place> OnPlaceBusyChanged;
        public IReadOnlyDictionary<PlaceManager.Place, Place[]> Places => _places;

        private Dictionary<PlaceManager.Place, Place[]> _places;

        [SerializeField] private Place[] _toilets;
        [SerializeField] private Place[] _sinks;
        [SerializeField] private Place[] _hallway;
        [SerializeField] private ClassAgent _class;



        public void SetLevel()
        {
            ArrayToDictionary();

            for(int i = 0; i < _toilets.Length; i++)
            {
                _toilets[i].OnBusyChanged += () => OnPlaceBusyChanged(PlaceManager.Place.Toilet);
            }

            for (int i = 0; i < _sinks.Length; i++)
            {
                _sinks[i].OnBusyChanged += () => OnPlaceBusyChanged(PlaceManager.Place.Sink);
            }

            for (int i = 0; i < _hallway.Length; i++)
            {
                _hallway[i].OnBusyChanged += () => OnPlaceBusyChanged(PlaceManager.Place.Hallway);
            }
        }


        private void ArrayToDictionary()
        {
            _places = new Dictionary<PlaceManager.Place, Place[]>();

            _places.Add(PlaceManager.Place.Toilet, _toilets);
            _places.Add(PlaceManager.Place.Sink, _sinks);
            _places.Add(PlaceManager.Place.Hallway, _hallway);
        }


        public Place GetPlace(PlaceManager.Place placeType, int index)
        {
            try
            {
                return _places[placeType][index];
            }
            catch
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public Place GetRandomFreePlace(string placeType)
        {
            PlaceManager.Place place;

            if (!Enum.TryParse<PlaceManager.Place>(placeType, out place))
                throw new InvalidEnumArgumentException($"Неверное место - \"{placeType}\"");

            return GetRandomFreePlace(place);
        }

        public Place GetRandomFreePlace(PlaceManager.Place placeType)
        {
            return _places[placeType].GetRandomFreePlace();
        }

        public bool HasFreePlace(PlaceManager.Place placeType)
        {
            return _places[placeType].HasFreePlace();
        }
    }
}