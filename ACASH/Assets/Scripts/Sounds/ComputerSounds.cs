using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;

public class ComputerSounds
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


    Dictionary<infinite, FMOD.Studio.EventInstance> infinite_sounds = new Dictionary<infinite, FMOD.Studio.EventInstance>();
    static string sounds_path = "event:/Computer/";


    public ComputerSounds(A_Computer Computer)
    {
        this.Computer = Computer;

        for(int i = 0; i < Enum.GetNames(typeof(infinite)).Length; i++)
        {
            infinite name = (infinite)i;
            FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(sounds_path + "Infinite/" + name.ToString());
            infinite_sounds.Add(name, sound);
        }
    }

    public void MakeSound(one_shot sound)
    {
        Debug.Log("One Shot Sound Play - " + sound.ToString());
        RuntimeManager.PlayOneShotAttached(sounds_path + "One Shot/" + sound.ToString(), Computer.gameObject);
    }


    public void StartSound(infinite sound)
    {
        Debug.Log("Infinite Sound Start - " + sound.ToString());
        infinite_sounds[sound].start();
    }


    public void StopSound(infinite sound)
    {
        StopSound(sound, false);
    }

    public void StopSound(infinite sound, bool immediate)
    {
        Debug.Log("Infinite Sound Stop - " + sound.ToString());

        if (immediate)
            infinite_sounds[sound].stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        else
            infinite_sounds[sound].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
