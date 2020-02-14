using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;
using Searching;

public class PlaceManager : Singleton<PlaceManager>
{
    [HideInInspector]
    public enum place
    {
        Toilet,
        Sink,
        Outside,
        Teacher_Room,
        Desk,
    }


    [HideInInspector]
    public Dictionary<place, Transform[,]> places = new Dictionary<place, Transform[,]>();

    [HideInInspector]
    public Dictionary<place, int> count = new Dictionary<place, int>();

    [HideInInspector]
    public Dictionary<place, bool[]> busy = new Dictionary<place, bool[]>();

    public Dictionary<Scholar, ScholarLocation> scholars = new Dictionary<Scholar, ScholarLocation>();


    public void SetLevel()
    {

        //Ищутся все места на уровне и забиваются в базу
        int place_count = Enum.GetNames(typeof(place)).Length;
        place buf_place;
        string buf_string;
        GameObject[] buf_objects;

        for (int i = 0; i < place_count; i++)
        { 
            buf_place = (place)i;
            buf_string = buf_place.ToString();

            if (buf_string != "Desk")
            {
                buf_objects = GameObject.FindGameObjectsWithTag(buf_string);
                if(buf_objects != null)
                    SetPlaces(buf_place, buf_objects);
            }
        }


        //Специальная инцилизация для парт
        DeskManager.get.SetDeskManager();
        buf_place = place.Desk;

        places.Add(buf_place, new Transform[2, DeskManager.get.desks.Length]);
        busy.Add(buf_place, new bool[DeskManager.get.desks.Length]);
        count.Add(buf_place, DeskManager.get.desks.Length);

        for (int i = 0; i < DeskManager.get.desks.Length; i++)
        {
            GameObject desk = DeskManager.get.desks[i].gameObject;

            this.places[buf_place][0, i] = desk.transform.Find("Destonation");
            this.places[buf_place][1, i] = desk.transform;
            //Debug.Log(desks[0, i].position);
        }
    }


    private void SetPlaces(place type_of_place, GameObject[] places)
    {
        this.places.Add(type_of_place, new Transform[2, places.Length]);
        busy.Add(type_of_place, new bool[places.Length]);
        count.Add(type_of_place, places.Length);

        SIC search = new SIC("Destonation");

        for (int i = 0; i < places.Length; i++)
        {
            search.Key("Destonation");
            search.Component(places[i], out this.places[type_of_place][0, i]);
            search.Key("Sight Goal");
            search.Component(places[i], out this.places[type_of_place][1, i]);
        }
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
                busy[type_of_place][index] = true;
                return index;
            }

            index++;
            index %= count[type_of_place];
        }

        return -1;
    }
}