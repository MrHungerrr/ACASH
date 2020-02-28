using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class ObjectsManager : Singleton<ObjectsManager>
{


    enum objects
    {
        Note,
    }


    Dictionary<objects, GameObject> objects_dic = new Dictionary<objects, GameObject>();

    [HideInInspector]
    public GameObject[] game_objects;


    private void Awake()
    {
        for(int i = 0; i < Enum.GetNames(typeof(objects)).Length; i++)
        {
            (objects)i.ToString()
        }
    }
}
