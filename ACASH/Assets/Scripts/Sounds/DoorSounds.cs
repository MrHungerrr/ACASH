using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;

public class DoorSounds : A_SoundSimple
{
    Door Door;

    public enum one_shot
    {
        Open,
        Close
    }


    public DoorSounds(Door Door)
    {
        sounds_path += "Local/Door/";
        this.Door = Door;

        Setup(Door.gameObject);
    }



    public void Make(one_shot sound)
    {
        base.MakeWithAttach(sound.ToString());
    }
}
