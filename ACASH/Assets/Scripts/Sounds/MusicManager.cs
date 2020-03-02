using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;
using Single;

public class MusicManager : Singleton<MusicManager>
{

    public enum music_name
    {
        Track_1,
    }


    Dictionary<music_name, FMOD.Studio.EventInstance> music = new Dictionary<music_name, FMOD.Studio.EventInstance>();
    static string sounds_path = "event:/Music/";


    public void Setup()
    {
        string music_path = "event:/Music/";
        for (int i = 0; i < Enum.GetNames(typeof(music_name)).Length; i++)
        {
            music_name name = (music_name)i;
            FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(music_path + name.ToString());
            music.Add(name, sound);
        }
    }


    public void Start(music_name sound)
    {
        Debug.Log("Infinite Sound Start - " + sound.ToString());
        music[sound].start();
    }


    public void Stop(music_name sound)
    {
        Stop(sound, false);
    }

    public void Stop(music_name sound, bool immediate)
    {
        Debug.Log("Infinite Sound Stop - " + sound.ToString());

        if (immediate)
            music[sound].stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        else
            music[sound].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
