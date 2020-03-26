using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;
using Single;

public class ElevatorSounds: A_Sound
{
    public enum sounds
    {
        Move,
        Ring,
        Open,
        Close,
    }



    public new void Setup(GameObject obj)
    {
        base.Setup(obj, "Local/Elevator/");

        for (int i = 0; i < Enum.GetNames(typeof(sounds)).Length; i++)
        {
            sounds name = (sounds)i;
            AddSound(name.ToString());
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

    public void Continue(sounds sound)
    {
        base.Continue(sound.ToString());
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
