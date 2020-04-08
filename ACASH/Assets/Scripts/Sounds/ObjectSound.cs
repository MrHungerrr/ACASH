using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;
[RequireComponent(typeof(Rigidbody))]

public class ObjectSound : A_Sound3D
{

    [SerializeField]
    [EventRef]
    private string path;

    private new string name;



    public void Awake()
    {
        Setup();
    }


    protected void Setup()
    {
        name = "";

        path = path.Remove(0, 7);

        for (int i = path.Length - 1; i >= 0; i--)
        {
            if (path[i] != '/')
            {
                name = path[i] + name;
            }
            else
            {
                path = path.Remove(i + 1, path.Length - i - 1);
                break;
            }
        }

        Setup(gameObject, path);

        AddSound3D(name);
    }


    public void Play()
    {
        base.Play(name);
    }

    public void Pause()
    {
        base.Pause(name);
    }

    public void Stop()
    {
        base.Stop(name);
    }

    public void Stop(bool immediate)
    {
        base.Stop(name, immediate);
    }
}
