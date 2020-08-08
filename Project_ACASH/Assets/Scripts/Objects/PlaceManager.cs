using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;
using Searching;

public class PlaceManager : MonoSingleton<PlaceManager>
{
    [HideInInspector]
    public enum place
    {
        Toilet,
        Sink,
        Outside,
        Teacher_Room,
        Desk,
        Home
    }


    [HideInInspector]
    public Dictionary<place, Transform[,]> places;

    [HideInInspector]
    public Dictionary<place, int> count;

    [HideInInspector]
    public Dictionary<place, bool[]> busy;

    public void SetLevel()
    {
        ResetLevel();

        //Ищутся все места на уровне и забиваются в базу
        int placeCount = Enum.GetNames(typeof(place)).Length;
        place bufPlace;
        string bufString;
        GameObject[] bufObjects;

        for (int i = 0; i < placeCount; i++)
        {
            bufPlace = (place)i;
            bufString = bufPlace.ToString();

            if (bufString != "Desk")
            {
                bufObjects = GameObject.FindGameObjectsWithTag(bufString);
                if (bufObjects != null)
                    SetPlaces(bufPlace, bufObjects);
            }
        }



        //Отдельная сортировка парт
        DeskManager.Instance.Setup();
        this.places.Add(place.Desk, new Transform[2, DeskManager.Instance.desks.Length]);
        busy.Add(place.Desk, new bool[DeskManager.Instance.desks.Length]);
        count.Add(place.Desk, DeskManager.Instance.desks.Length);


        for (int i = 0; i < DeskManager.Instance.desks.Length; i++)
        {
            SIC.Component(DeskManager.Instance.desks[i].gameObject, "Destonation", out this.places[place.Desk][0, i]);
            SIC.Component(DeskManager.Instance.desks[i].gameObject, "Sight Goal", out this.places[place.Desk][1, i]);
        }
    }


    private void SetPlaces(place type_of_place, GameObject[] places)
    {
        switch(type_of_place)
        {
            case place.Home:
                {
                    if(places != null)
                        SortManager.Instance.Sort(places);
                    break;
                }
        }


        this.places.Add(type_of_place, new Transform[2, places.Length]);
        busy.Add(type_of_place, new bool[places.Length]);
        count.Add(type_of_place, places.Length);


        for (int i = 0; i < places.Length; i++)
        {
            SIC.Component(places[i], "Destonation",out this.places[type_of_place][0, i]);
            SIC.Component(places[i], "Sight Goal",out this.places[type_of_place][1, i]);
        }
    }


    public void ResetLevel()
    {
        places = new Dictionary<place, Transform[,]>();
        busy = new Dictionary<place, bool[]>();
        count = new Dictionary<place, int>();
    }



    public Vector3 GetPlace(place type_of_place, int i)
    {
        try
        {
            return places[type_of_place][0, i].position;
        }
        catch
        {
            Debug.LogError("Ошибка в GetPlace");
            return Vector3.zero;
        }
    }

    public Vector3 GetSightGoal(place type_of_place, int i)
    {
        try
        {
            return places[type_of_place][1, i].position;
        }
        catch
        {
            Debug.LogError("Ошибка в GetSightGoal");
            return Vector3.zero;
        }
    }



    public void MakeFree(place type_of_place, int i)
    {
        try
        {
            busy[type_of_place][i] = false;
        }
        catch
        {
            Debug.LogError("Ошибка в MakeFree");
        }
    }

    public void MakeBusy(place type_of_place, int i)
    {
        try
        {
            busy[type_of_place][i] = true;
        }
        catch
        {
            Debug.LogError("Ошибка в MakeFree");
        }
    }



    public bool IsFree(place type_of_place)
    {
        for (int i = 0; i < count[type_of_place]; i++)
        {
            if (!busy[type_of_place][i])
            {
                return true;
            }
        }

        return false;
    }

    public int GetRandomFreePlaceIndex(place type_of_place)
    {
        int random = UnityEngine.Random.Range(0, count[type_of_place]);
        return GetFreePlaceIndex(type_of_place, random);
    }

    public int GetFreePlaceIndex(place type_of_place)
    {
        return GetFreePlaceIndex(type_of_place, 0);
    }

    private int GetFreePlaceIndex(place type_of_place, int start_index)
    {
        int index = start_index % count[type_of_place];

        for (int i = 0; i < count[type_of_place]; i++)
        {
            if (!busy[type_of_place][index])
            {
                return index;
            }

            index++;
            index %= count[type_of_place];
        }

        return -1;
    }
}