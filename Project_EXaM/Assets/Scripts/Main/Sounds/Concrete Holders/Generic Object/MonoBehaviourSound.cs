using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;
[RequireComponent(typeof(Rigidbody))]

public class MonoBehaviourSound : MonoBehaviour
{
    private enum _type
    {
        Auto,
        Manual,
    }

    [SerializeField] private _type _typeStart;
    [SerializeField] [EventRef] private string _path;

    private ObjectSound _soundHolder;
    private string _soundName;



    private void Start()
    {
        SplitPathAndName();

        _soundHolder = new ObjectSound(gameObject, _path, _soundName);

        if (_typeStart == _type.Auto)
            Play();
    }


    private void SplitPathAndName()
    {
        _soundName = "";

        _path = _path.Remove(0, 7);

        for (int i = _path.Length - 1; i >= 0; i--)
        {
            if (_path[i] != '/')
            {
                _soundName = _path[i] + _soundName;
            }
            else
            {
                _path = _path.Remove(i + 1, _path.Length - i - 1);
                return;
            }
        }
    }


    public void Play()
    {
        _soundHolder.Play(_soundName);
    }

    public void Pause()
    {
        _soundHolder.Pause(_soundName);
    }

    public void Stop()
    {
        _soundHolder.Stop(_soundName);
    }

    public void Stop(bool immediate)
    {
        _soundHolder.Stop(_soundName);
    }
}
