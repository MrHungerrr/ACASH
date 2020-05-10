using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public abstract class A_Sound2D: A_SoundBase
{

    protected Dictionary<string, FMODAudioBase> FMODsounds = new Dictionary<string, FMODAudioBase>();
    protected bool active = false;


    protected override void Setup(string path)
    {
        active = true;
        base.Setup(path);
    }


    protected void AddSound2D(string name)
    {
        try
        {
            if (!FMODsounds.ContainsKey(name))
            {
                FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(sounds_path + name);
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
        if(active)
            base.Update();
    }



    protected virtual void Play(string sound)
    {
        if (base.Play(FMODsounds[sound])) ;
    }

    protected virtual void PlayAnyway(string sound)
    {
        base.PlayAnyway(FMODsounds[sound]);
    }


    public void Pause()
    {
        foreach(KeyValuePair<string, FMODAudioBase> pair in FMODsounds)
        {
            Pause(pair.Value);
        }
    }

    public void Continue()
    {
        foreach (KeyValuePair<string, FMODAudioBase> pair in FMODsounds)
        {
            Continue(pair.Value);
        }
    }

    protected virtual void Pause(string sound)
    {
        base.Pause(FMODsounds[sound]);
    }

    protected virtual void Continue(string sound)
    {
        base.Continue(FMODsounds[sound]);
    }



    //Остановка Sound
    protected void Stop(string sound)
    {
        base.Stop(FMODsounds[sound]);
    }

    protected virtual void Stop(string sound, bool immediate)
    {
        base.Stop(FMODsounds[sound], immediate);
    }

    public void Stop()
    {
        active = false;

        foreach (KeyValuePair<string, FMODAudioBase> pair in FMODsounds)
        {
            Stop(pair.Value);
        }
    }


}
