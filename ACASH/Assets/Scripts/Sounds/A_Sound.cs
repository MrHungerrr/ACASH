using UnityEngine;
using FMODUnity;
using System.Collections.Generic;



public abstract class A_Sound : A_SoundSimple
{

    protected string playing_now;
    [HideInInspector]
    public bool playing;

    protected Dictionary<string, FMOD.Studio.EventInstance> infinite_sounds = new Dictionary<string, FMOD.Studio.EventInstance>();


    protected override void Setup(GameObject obj)
    {
        base.Setup(obj);
    }



    protected void AddInfinite(string name)
    {
        try
        {
            FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(sounds_path + "Infinite/" + name);
            RuntimeManager.AttachInstanceToGameObject(sound, obj.transform, obj.GetComponent<Rigidbody>());
            infinite_sounds.Add(name, sound);
        }
        catch
        {
            Debug.LogError("Infinite Sound is MISSING - " + name);
        }
    }


    protected void AddInfiniteWithoutAttach(string name)
    {
        try
        {
            FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(sounds_path + "Infinite/" + name);
            infinite_sounds.Add(name, sound);
        }
        catch
        {
            Debug.LogError("Infinite Sound is MISSING - " + name);
        }
    }



    protected void Play(string sound)
    {
        try
        {
            infinite_sounds[sound].start();
            Debug.Log("Infinite Sound Start - " + sound);
            playing_now = sound;
            playing = true;
        }
        catch
        {
            Debug.LogError("Infinite Sound is MISSING - " + sound);
        }
    }


    public void Pause()
    {
        Pause(playing_now);
    }

    protected void Pause(string sound)
    {
        if (playing)
            infinite_sounds[sound].setPaused(true);
        else
            Debug.LogError("Никакой трек не играет");
    }

    public void Continue()
    {
        Continue(playing_now);
    }

    protected void Continue(string sound)
    {
        if (playing)
            infinite_sounds[sound].setPaused(false);
        else
            Debug.LogError("Никакой трек не играет");
    }




    //Остановка Sound
    protected void Stop(string sound)
    {
        Stop(sound, false);
    }

    protected void Stop(string sound, bool immediate)
    {
        if (playing)
        {
            try
            {
                Debug.Log("Infinite Sound Stop - " + sound);

                if (immediate)
                    infinite_sounds[sound].stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                else
                    infinite_sounds[sound].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

                playing = false;
                playing_now = default;
            }
            catch
            {
                Debug.LogError("Infinite Sound is MISSING - " + sound);
            }
        }
    }



    //Остановка Sound_Now
    public void Stop()
    {
        Stop(false);
    }

    public void Stop(bool immediate)
    {
        Stop(playing_now, immediate);
    }

}
