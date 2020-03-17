using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;

public class ScholarSounds : A_Sound
{
    Scholar Scholar;

    public enum one_shot
    {
        Touch,
    }

    public enum infinite
    {
        Talk,
        Walk,
    }


    public ScholarSounds(Scholar Scholar)
    {
        sounds_path += "Local/Scholar/";
        this.Scholar = Scholar;
        obj = Scholar.gameObject;

        Setup();
    }

    protected override void Setup()
    {
        for (int i = 0; i < Enum.GetNames(typeof(infinite)).Length; i++)
        {
            infinite name = (infinite)i;
            AddInfinite(name.ToString());
        }
    }





    //========================================================================================================================
    //========================================================================================================================
    //Не трогать

    public void Make(one_shot sound)
    {
        base.Make(sound.ToString());
    }

    public void Play(infinite sound)
    {
        base.Play(sound.ToString());
    }

    public void Pause(infinite sound)
    {
        base.Pause(sound.ToString());
    }

    public void Continue(infinite sound)
    {
        base.Continue(sound.ToString());
    }

    public void Stop(infinite sound)
    {
        base.Stop(sound.ToString());
    }

    public void Stop(infinite sound, bool immediate)
    {
        base.Stop(sound.ToString(),immediate);
    }
}
