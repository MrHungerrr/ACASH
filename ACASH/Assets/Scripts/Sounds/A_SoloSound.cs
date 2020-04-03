using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public abstract class A_SoloSound: A_Sound
{
    [HideInInspector]
    public bool playing;
    [HideInInspector]
    public string sound;



    protected override void Play(string sound)
    {

        if (playing && this.sound != sound)
            Stop(this.sound);

        base.Play(sound);

        playing = true;
        this.sound = sound;
    }


    protected override void Pause(string sound)
    {
        if (FMODsounds[sound].Pause())
        {
            playing = false;
        }
        else
        {
            Debug.Log("Sound is not playing - " + sound);
        }

    }



    protected override void Stop(string sound, bool immediate)
    {
        if(FMODsounds[sound].Stop(immediate))
        {
            playing = false;
            sound = null;
        }
        else
        {
            Debug.Log("Sound is not active - " + sound);
        }
    }
}
