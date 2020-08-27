using System;
using UnityEngine;

public sealed class NoiseManager : SoundHolder2D
{
    public enum sounds
    {
        Rain,
    }


    public NoiseManager() : base("Global/Sounds")
    {
        SetupSounds();
    }

    protected override void SetupSounds()
    {
        for (int i = 0; i < Enum.GetNames(typeof(sounds)).Length; i++)
        {
            sounds name = (sounds)i;
            AddSound2D(name.ToString());
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
        base.Stop(sound.ToString(), immediate);
    }
}
