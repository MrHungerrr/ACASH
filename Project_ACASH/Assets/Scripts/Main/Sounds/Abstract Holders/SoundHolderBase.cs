using UnityEngine;
using FMODUnity;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class SoundHolderBase
{
    public Action UpdateSound { get; set; }


    protected readonly string _soundPath = "event:/";


    protected SoundHolderBase(string path)
    {
        _soundPath = $"event:/{path}";

        if (path.Last() != '/')
            _soundPath += '/';

        UpdateManager.Instance.AddUpdate(Update);
    }


    protected abstract void Update();


    protected virtual void Play(FMODAudioBase sound)
    {
        try
        {
            if(!sound.IsPlaying)
            {
                sound.Play();
            }
            else
            {
                //Debug.Log("Sound is already playing - " + sound);
            }
        }
        catch
        {
            Debug.LogError($"Sound \"{sound}\" is MISSING");
        }
    }

    protected virtual void PlayAnyway(FMODAudioBase sound)
    {
        try
        {
            sound.PlayAnyway();
        }
        catch
        {
            Debug.LogError("Sound is MISSING!");
        }
    }


    protected virtual void Pause(FMODAudioBase sound)
    {
        if (sound.IsActive && sound.IsPlaying)
        {
            sound.Pause();
        }
        else
        {
            //Debug.Log("Sound is not playing - " + sound);
        }
    }

    protected virtual void Continue(FMODAudioBase sound)
    {
        if (sound.IsActive && !sound.IsPlaying)
        {
            sound.Continue();
        }
        else
        {
            //Debug.Log("Sound is not playing - " + sound);
        }
    }

    protected virtual void Stop(FMODAudioBase sound, bool immediate)
    {
        if(sound.IsActive)
        {
            sound.Stop(immediate);
        }
        else
        {
            //Debug.Log("Sound is not active - " + sound);
        }
    }
}
