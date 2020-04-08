using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public abstract class A_Sound2D: A_SoundBase
{

    protected Dictionary<string, FMODAudioBase> FMODsounds = new Dictionary<string, FMODAudioBase>();


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




    protected virtual void Play(string sound)
    {
        if (base.Play(FMODsounds[sound])) ;
    }

    protected virtual void PlayAnyway(string sound)
    {
        base.PlayAnyway(FMODsounds[sound]);
    }



    protected virtual void Pause(string sound)
    {
        base.Pause(FMODsounds[sound]);
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
}
