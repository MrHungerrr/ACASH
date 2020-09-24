using System;
using System.Collections.Generic;
using UnityEngine;
using Vkimow.Tools.Single;

public class AudioManager : Singleton<AudioManager>
{
    public MusicManager Music { get; private set; }
    public NoiseManager Noise { get; private set; }
    public VoiceManager Voice { get; private set; }
    public List<SoundHolder3D> WorldSounds { get; private set; }



    public void Setup()
    {
        Music = new MusicManager();
        Voice = new VoiceManager();
        Noise = new NoiseManager();

        Reset();
    }

    private void Reset()
    {
        WorldSounds = new List<SoundHolder3D>();
    }


    public void Pause(bool option)
    {
        if (option)
        {
            for (int i = 0; i < WorldSounds.Count; i++)
            {
                WorldSounds[i].PauseAll();
            }

            Music.PauseAll();
            Voice.Pause();
            Noise.PauseAll();
        }
        else
        {
            for (int i = 0; i < WorldSounds.Count; i++)
            {
                WorldSounds[i].ContinueAll();
            }

            Music.ContinueAll();
            Voice.Continue();
            Noise.ContinueAll();
        }
    }




    public void UnsetupSchool()
    {
        Stop();
        Reset();
    }


    private void Stop()
    {
        for (int i = 0; i < WorldSounds.Count; i++)
        {
            WorldSounds[i].StopAll();
        }

        Music.StopAll();
        Voice.Stop();
        Noise.StopAll();
    }
}
