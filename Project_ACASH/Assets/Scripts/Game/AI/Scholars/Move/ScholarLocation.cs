using Places;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameTime.Action;
using System.Threading.Tasks;

namespace AI.Scholars
{
    public class ScholarLocation
    {
        private Scholar _scholar;
        private Place _currentPlace;


        public ScholarLocation(Scholar scholar)
        {
            _scholar = scholar;

            Action LookAtPlace = () =>
            {
                _scholar.Sight.SetSightGoal(_currentPlace.SightGoal);
            };

            _scholar.Move.OnDestinationReached += LookAtPlace;

            Teleport(_scholar.ClassRoom.GetMyDockStation(_scholar));
        }

        public void GoTo(Place place)
        {
            if (place.Busy)
                throw new Exception("Место уже занято");

            _currentPlace?.SetBusy(false);

            _scholar.Move.SetDestination(place.Destination);

            Debug.Log("Fuck");

            _currentPlace = place;
            place.SetBusy(true);
            return;
        }

        public void GoToDesk()
        {
            var place = _scholar.ClassRoom.GetMyDesk(_scholar);
            GoTo(place);
        }

        public void GoToDockStation()
        {
            var place = _scholar.ClassRoom.GetMyDockStation(_scholar);
            GoTo(place);
        }

        public void Teleport(Place place)
        {
            if (place.Busy)
                throw new ArgumentException();

            _currentPlace?.SetBusy(false);

            _scholar.Move.SetPosition(place.Destination);
            _scholar.Sight.LookAt(place.SightGoal);

            _currentPlace = place;
            place.SetBusy(true);
        }
    }
}
