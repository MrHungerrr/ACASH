using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;
using AI.Scholars;



public class ScholarSounds : SoundHolder3D
{
    private readonly Scholar _scholar;


    public enum sounds
    {
        Talk,
        Walk,
    }

    public ScholarSounds(Scholar scholar) : base(scholar.gameObject, "Local/Scholar")
    {
        this._scholar = scholar;
    }


    protected override void SetupSounds()
    {
        for (int i = 0; i < Enum.GetNames(typeof(sounds)).Length; i++)
        {
            sounds name = (sounds)i;
            AddSound3D(name.ToString());
        }
    }





    //========================================================================================================================
    //========================================================================================================================
    //Не трогать


    public void Play(sounds sound)
    {
        base.Play(sound.ToString());
    }

    public void Pause(sounds sound)
    {
        base.Pause(sound.ToString());
    }

    public void Stop(sounds sound)
    {
        base.Stop(sound.ToString());
    }

    public void Stop(sounds sound, bool immediate)
    {
        base.Stop(sound.ToString(),immediate);
    }
}
