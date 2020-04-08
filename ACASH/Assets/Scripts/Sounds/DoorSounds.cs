﻿using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;

public class DoorSounds : A_SoloSound
{

    public enum sounds
    {
        Open,
        Close
    }



    public new void Setup(GameObject obj)
    {
        base.Setup(transform.parent.gameObject, "Local/Door/");

        base.SetIgnore("Door");

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
        base.Stop(sound.ToString(), immediate);
    }
}
