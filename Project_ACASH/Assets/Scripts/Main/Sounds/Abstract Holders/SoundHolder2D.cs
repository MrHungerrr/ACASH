using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public abstract class SoundHolder2D: SoundHolderBase
{
    protected Dictionary<string, FMODAudioBase> FMODsounds = new Dictionary<string, FMODAudioBase>();



    protected SoundHolder2D(string path) : base(path)
    {
    }

    protected abstract void SetupSounds();

    protected void AddSound2D(string name)
    {
        try
        {
            if (!FMODsounds.ContainsKey(name))
            {
                FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(_soundPath + name);
                FMODAudioBase audio = new FMODAudioBase(this, sound);
                FMODsounds.Add(name, audio);
            }
            else
            {
                Debug.LogError("Sound is Already Added - " + name);
            }
        }
        catch
        {
            Debug.LogError("ERROR in adding Sound - " + name);
        }
    }


    protected override void Update()
    {
        base.Update();
    }


    public virtual void PauseAll()
    {
        foreach (KeyValuePair<string, FMODAudioBase> pair in FMODsounds)
        {
            Pause(pair.Value);
        }
    }

    public virtual void ContinueAll()
    {
        foreach (KeyValuePair<string, FMODAudioBase> pair in FMODsounds)
        {
            Continue(pair.Value);
        }
    }

    public virtual void StopAll()
    {
        foreach (KeyValuePair<string, FMODAudioBase> pair in FMODsounds)
        {
            Stop(pair.Value, false);
        }
    }


    protected virtual void Play(string sound)
    {
        base.Play(FMODsounds[sound]);
    }

    protected virtual void PlayAnyway(string sound)
    {
        base.PlayAnyway(FMODsounds[sound]);
    }


    protected virtual void Pause(string sound)
    {
        base.Pause(FMODsounds[sound]);
    }

    //protected void Continue(string sound)
    //{
    //    base.Continue(FMODsounds[sound]);
    //}



    //Остановка Sound
    protected virtual void Stop(string sound)
    {
        Stop(sound, false);
    }

    protected virtual void Stop(string sound, bool immediate)
    {
        base.Stop(FMODsounds[sound], immediate);
    }
}
