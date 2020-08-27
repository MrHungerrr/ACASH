using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;
[RequireComponent(typeof(Rigidbody))]

public class ObjectSound : SoundHolder3D
{

    private readonly HashSet<string> _soundNames;


    public ObjectSound(GameObject host, string path, params string[] soundNames) : base(host, path)
    {
        _soundNames = new HashSet<string>(soundNames);
        SetupSounds();
    }

    protected override void SetupSounds()
    {
        foreach(string sound in _soundNames)
        {
            AddSound3D(sound);
        }
    }

    public new void Play(string sound)
    {
        if (_soundNames.Contains(sound))
            base.Play(sound);
        else
            throw new Exception($"Нет звука {sound}");
    }

    public new void Pause(string sound)
    {
        if(_soundNames.Contains(sound))
            base.Pause(sound);
        else
            throw new Exception($"Нет звука {sound}");
    }

    public new void Stop(string sound)
    {
        Stop(sound, false);
    }

    public new void Stop(string sound, bool immediate)
    {
        if (_soundNames.Contains(sound))
            base.Stop(sound, immediate);
        else
            throw new Exception($"Нет звука {sound}");
    }
}
