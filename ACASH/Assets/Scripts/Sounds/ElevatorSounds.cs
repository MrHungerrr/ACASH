using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;
using Single;

public class ElevatorSounds: A_Sound
{


    public enum one_shot
    {
        Ring,
        Open,
        Close,
    }

    public enum infinite
    {
        Move,
    }


    public ElevatorSounds(GameObject obj)
    {
        sounds_path += "Local/Elevator/";
        this.obj = obj;

        Setup();
    }

    protected override void Setup()
    {
        for (int i = 0; i < Enum.GetNames(typeof(infinite)).Length; i++)
        {
            infinite name = (infinite)i;
            AddInfiniteWithoutAttach(name.ToString());
        }
    }






    //========================================================================================================================
    //========================================================================================================================
    //Не трогать



    public void Make(one_shot sound)
    {
        base.Make(sound.ToString());
    }

    public void MakeWithoutAttach(one_shot sound)
    {
        base.MakeWithoutAttach(sound.ToString());
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
        base.Stop(sound.ToString(), immediate);
    }
}
