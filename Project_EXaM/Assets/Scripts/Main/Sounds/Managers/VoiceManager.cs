using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;

public sealed class VoiceManager : SoundHolderBase
{
    private FMODAudioBase voice;


    public VoiceManager() : base("Voice")
    {
    }

    protected override void Update()
    {
        base.Update();
    }


    public void Play(KeyWord key)
    {
        string fullPath = _soundPath + ScriptManager.Instance.voiceLanguage + '/' + key.Main + '/' + key.FullWorld;

        try 
        {
            FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(fullPath);
            voice = new FMODAudio2D(this, sound);
            Play(voice);
        }
        catch
        {
            Debug.LogError("Неправильный путь - " + fullPath);
        }
    }

    public void Pause()
    {
        if (voice != null)
            voice.Pause();
    }

    public void Continue()
    {
        if(voice != null)
            voice.Continue();
    }


    public void Stop()
    {
        if (voice != null)
            Stop(voice, true);
    }
}
