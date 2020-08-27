using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;

public sealed class MusicManager : SoundHolder2D
{
    public enum music
    {
        Track_1,
    }



    public MusicManager(): base("Global/Music")
    {
        SetupSounds();
    }

    protected override void SetupSounds()
    {
        for (int i = 0; i < Enum.GetNames(typeof(music)).Length; i++)
        {
            music name = (music)i;
            AddSound2D(name.ToString());
        }
    }






    //========================================================================================================================
    //========================================================================================================================
    //Не трогать

    public void Play(music sound)
    {
        base.Play(sound.ToString());
    }

    public void Pause(music sound)
    {
        base.Pause(sound.ToString());
    }

    public void Stop(music sound)
    {
        base.Stop(sound.ToString());
    }

    public void Stop(music sound, bool immediate)
    {
        base.Stop(sound.ToString(), immediate);
    }
}
