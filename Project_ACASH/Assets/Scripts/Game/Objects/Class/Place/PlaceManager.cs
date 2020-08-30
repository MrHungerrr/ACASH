using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;
using Searching;


namespace Places
{
    public static class PlaceManager
    {
        public enum place
        {
            Toilet,
            Sink,
            Outside,
        }


        private static PlaceAgent[] placeAgents;


        public static void SetLevel()
        {
            placeAgents = GameObject.FindObjectsOfType<PlaceAgent>();

            foreach (var placeAgent in placeAgents)
            {
                placeAgent.SetLevel();
            }
        }



        public static Place GetRandomFreePlace(this Place[] places)
        {
            int? index = GetRandomPlaceIndex(places);
            index = GetFreePlaceIndex(places, index.Value);

            if (index.HasValue)
            {
                return places[index.Value];
            }

            return null;
        }


        private static int GetRandomPlaceIndex(Place[] places)
        {
            return UnityEngine.Random.Range(0, places.Length);
        }


        private static int? GetFreePlaceIndex(Place[] places, int startIndex)
        {
            int index = startIndex % places.Length;

            for (int i = 0; i < places.Length; i++)
            {
                if (!places[index].Busy)
                {
                    return index;
                }

                index++;

                if (index >= places.Length)
                    index = 0;
            }

            return null;
        }

        public static bool HasFreePlace(this Place[] places)
        {

            for(int i = 0; i< places.Length; i++)
            {
                if (!places[i].Busy)
                    return true;
            }

            return false;
        }
    }
}