using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;
using Objects.Organization.Places;
using UnityEngine;

namespace Objects.Organization.ClassRoom.GOAP
{
    public class ClassGOAPContext : GOAPStateStorageList
    {
        private readonly ClassAgent _classRoom;

        public ClassGOAPContext(ClassAgent classRoom)
        {
            _classRoom = classRoom;

            Add("Places_Toilet_Are_Busy", !_classRoom.PlaceAgent.HasFreePlace(PlaceManager.Place.Toilet));
            Add("Places_Sink_Are_Busy", !_classRoom.PlaceAgent.HasFreePlace(PlaceManager.Place.Sink));
            Add("Places_Hallway_Are_Busy", !_classRoom.PlaceAgent.HasFreePlace(PlaceManager.Place.Hallway));

            _classRoom.PlaceAgent.OnPlaceBusyChanged += PlaceBusyChanged;
        }

        public void PlaceBusyChanged(PlaceManager.Place place)
        {
            Debug.Log($"{place}s поменяли свое состояние на {!_classRoom.PlaceAgent.HasFreePlace(place)}");
            Set($"Places_{place.ToString()}_Are_Busy", !_classRoom.PlaceAgent.HasFreePlace(place));

            foreach(var state in this)
            {
                Debug.Log($"Key = {state.Key}, Value = {state.Value}");
            }
        }
    }
}

