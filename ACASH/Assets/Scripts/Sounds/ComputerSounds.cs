using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;

public class ComputerSounds : A_Sound
{
    A_Computer Computer;

    public enum one_shot
    {
        Click,
        Open_Window,
    }

    public enum infinite
    {
        Load,
    }


    public ComputerSounds(A_Computer Computer)
    {
        sounds_path += "Local/Computer/";
        this.Computer = Computer;
        obj = Computer.gameObject;

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
