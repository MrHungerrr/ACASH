using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;

[RequireComponent(typeof(Rigidbody))]

public class ObjectSound : MonoBehaviour, I_Sound
{
    [SerializeField]
    [EventRef]
    private string sound_path;

    private FMOD.Studio.EventInstance sound;

    public void Setup()
    {
        sound = RuntimeManager.CreateInstance(sound_path);
        RuntimeManager.AttachInstanceToGameObject(sound, gameObject.transform, gameObject.GetComponent<Rigidbody>());
    }


    public void Play()
    {
        sound.start();
    }

    public void Pause()
    {
        sound.setPaused(true);
    }

    public void Continue()
    {
        sound.setPaused(false);
    }

    public void Stop()
    {
        sound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }



}
