﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class ScholarObjectsManager : Singleton<ScholarObjectsManager>
{


    public enum obj_name
    {
        Note,
    }

    [HideInInspector]
    public Transform object_parent;

    [HideInInspector]
    public Dictionary<obj_name, GameObject> objects = new Dictionary<obj_name, GameObject>();


    private void Awake()
    {
        for(int i = 0; i < Enum.GetNames(typeof(obj_name)).Length; i++)
        {
            obj_name name_buf = (obj_name)i;
            Debug.Log("Objects/" + name_buf.ToString());
            GameObject object_buf = Resources.Load("Objects/" + name_buf.ToString()) as GameObject;
            Debug.Log(object_buf.name);

            objects.Add(name_buf, object_buf);
        }
    }


    public void Setup()
    {
        object_parent = GameObject.FindGameObjectWithTag("Objects_Container").transform;
    }
}
