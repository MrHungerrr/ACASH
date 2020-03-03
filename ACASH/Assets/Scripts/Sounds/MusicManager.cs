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

    private music_name sound_now;
    [HideInInspector]
    public bool playing;


    Dictionary<music_name, FMOD.Studio.EventInstance> music = new Dictionary<music_name, FMOD.Studio.EventInstance>();


    public void Setup()
    {
        string music_path = "event:/Music/";
        for (int i = 0; i < Enum.GetNames(typeof(music_name)).Length; i++)
        {
            music_name name = (music_name)i;
            FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(music_path + name.ToString());
            music.Add(name, sound);
        }

        sound_now = default;
    }


    public void Play(music_name sound)
    {
        Debug.Log("Infinite Sound Start - " + sound.ToString());
        sound_now = sound;
        playing = true;
        music[sound].start();
    }




    public void Pause()
    {
            Pause(sound_now);
    }

    public void Pause(music_name sound)
    {
        if (playing)
            music[sound].setPaused(true);
        else
            Debug.LogError("Никакой трек не играет");
    }

    public void Continue()
    {
        Continue(sound_now);
    }

    public void Continue(music_name sound)
    {
        if (playing)
            music[sound].setPaused(false);
        else
            Debug.LogError("Никакой трек не играет");
    }




    //Остановка Sound
    public void Stop(music_name sound)
    {
        Stop(sound, false);
    }

    public void Stop(music_name sound, bool immediate)
    {
        if (playing)
        {
            Debug.Log("Infinite Sound Stop - " + sound.ToString());

            if (immediate)
                music[sound].stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            else
                music[sound].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            playing = false;
            sound_now = default;
        }
    }



    //Остановка Sound_Now
    public void Stop()
    {
        Stop(false);
    }

    public void Stop(bool immediate)
    {
        Stop(sound_now, immediate);
    }

}
