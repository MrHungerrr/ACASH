using Overwatch.Watchable;
using System;
using System.Collections.Generic;
using UnityEngine;




public class Object: MonoBehaviour
{
    public Rigidbody RB => _rb;
    public Renderer Renderer => _renderer;


    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private Renderer _renderer;


    private ObjectWatchable _watchable;

    public void Setup()
    {
        _watchable = new ObjectWatchable(this);
    }
}

