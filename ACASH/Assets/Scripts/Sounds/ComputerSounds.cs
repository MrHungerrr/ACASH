using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;

public class ComputerSounds : A_Sound3D
{
    A_Computer Computer;

    public enum sounds
    {
        Load,
        Click,
    }


    public void Setup(A_Computer Computer)
    {
        this.Computer = Computer;

        base.Setup(gameObject, "Local/Computer/");

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

    public void PlayAnyway(sounds sound)
    {
        base.PlayAnyway(sound.ToString());
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
