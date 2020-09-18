using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects.Organization.Places
{
    public static class PlaceManager
    {
        public enum Place
        {
            Toilet,
            Sink,
            Hallway,
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



        public static Places.Place GetRandomFreePlace(this Places.Place[] places)
        {
            int index = GetRandomPlaceIndex(places);
            index = GetFreePlaceIndex(places, index);
            return places[index];
        }


        private static int GetRandomPlaceIndex(Places.Place[] places)
        {
            return UnityEngine.Random.Range(0, places.Length);
        }


        private static int GetFreePlaceIndex(Places.Place[] places, int startIndex)
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

            throw new Exception("Нет пустых мест!");
        }

        public static bool HasFreePlace(this Places.Place[] places)
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