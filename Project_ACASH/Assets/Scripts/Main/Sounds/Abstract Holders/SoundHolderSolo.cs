using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public abstract class SoundHolderSolo: SoundHolder3D
{
    private string _sound;


    public SoundHolderSolo(GameObject host, string path) : base(host, path)
    {
    }


    protected override void Play(string sound)
    {
        if (_sound != sound && FMODsounds[_sound].IsActive)
            Stop(_sound);

        base.Play(sound);

        _sound = sound;
    }

    protected override void PlayAnyway(string sound)
    {
        if (_sound != sound && FMODsounds[_sound].IsActive)
            Stop(_sound);

        base.PlayAnyway(sound);

        _sound = sound;
    }
}
