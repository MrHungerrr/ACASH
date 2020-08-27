using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class ScholarObjectsManager : MonoSingleton<ScholarObjectsManager>
{
    public enum objectType
    {
        Note,
    }

    public Transform ObjectContainer { get; private set; }

    public IReadOnlyDictionary<objectType, GameObject> Objects => _objects;

    private Dictionary<objectType, GameObject> _objects = new Dictionary<objectType, GameObject>();


    public void Setup()
    {
        for(int i = 0; i < Enum.GetNames(typeof(objectType)).Length; i++)
        {
            objectType nameBuf = (objectType)i;
            GameObject objectBuf = Resources.Load("Objects/" + nameBuf.ToString()) as GameObject;

            _objects.Add(nameBuf, objectBuf);
        }
    }


    public void SetLevel()
    {
        ObjectContainer = GameObject.FindGameObjectWithTag("Objects_Container").transform;
    }
}
