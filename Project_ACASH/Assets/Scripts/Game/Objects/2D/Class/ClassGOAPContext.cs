using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOAP;
using Objects._2D.Places;
using UnityEngine;

namespace Objects._2D.ClassRoom.GOAP
{
    public class ClassGOAPContext : GOAPStateStorageList
    {
        private readonly ClassAgent _classRoom;

        public ClassGOAPContext(ClassAgent classRoom)
        {
            _classRoom = classRoom;

            Add("Program_Calculator_Allowed", false);
            Add("Program_Dictionary_Allowed", true);
            Add("Program_Browser_Allowed", false);
            Add("Program_Rules_Allowed", true);
            Add("Program_Test_Allowed", true);
            Add("Program_Text_Allowed", true);
            Add("Program_Code_Allowed", false);

            Add("Item_Phone_Allowed", false);

            Add("Place_Toilet_All_Busy", !_classRoom.PlaceAgent.HasFreePlace(PlaceManager.Place.Toilet));
            Add("Place_Sink_All_Busy", !_classRoom.PlaceAgent.HasFreePlace(PlaceManager.Place.Sink));
            Add("Place_Hallway_All_Busy", !_classRoom.PlaceAgent.HasFreePlace(PlaceManager.Place.Hallway));

            Add("Place_Toilet_Allowed", true);
            Add("Place_Sink_Allowed", true);
            Add("Place_Hallway_Allowed", true);

            _classRoom.PlaceAgent.OnPlaceBusyChanged += PlaceBusyChanged;
        }

        public void PlaceBusyChanged(PlaceManager.Place place)
        {
            //Debug.Log($"{place}s поменяли свое состояние на {!_classRoom.PlaceAgent.HasFreePlace(place)}");
            Set($"Place_{place.ToString()}_All_Busy", !_classRoom.PlaceAgent.HasFreePlace(place));
        }
    }
}

