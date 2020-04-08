using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public abstract class A_SoundBase: MonoBehaviour
{
    protected string sounds_path = "event:/";

    public event ActionEvent.OnAction UpdateSound;



    protected virtual void Setup(string path)
    {
        this.sounds_path += path;
    }


    protected virtual void Update()
    {
        if (UpdateSound != null)
            UpdateSound();
    }


    protected virtual bool Play(FMODAudioBase sound)
    {
        try
        {
            if(sound.Play())
            {
                //Debug.Log("Sound Play - " + sound);
                return true;
            }
            else
            {
                //Debug.Log("Sound is already playing - " + sound);
            }

        }
        catch
        {
            Debug.LogError("Sound is MISSING");
        }
        return false;
    }

    protected virtual void PlayAnyway(FMODAudioBase sound)
    {
        try
        {
            sound.PlayAnyway();
            //Debug.Log("Sound Play - " + sound);
        }
        catch
        {
            Debug.LogError("Sound is MISSING!");
        }
    }



    protected virtual void Pause(FMODAudioBase sound)
    {
        if (!sound.Pause())
        {
            //Debug.Log("Sound is not playing - " + sound);
        }

    }

    //Остановка Sound
    protected void Stop(FMODAudioBase sound)
    {
        Stop(sound, false);
    }

    protected virtual void Stop(FMODAudioBase sound, bool immediate)
    {
        if(!sound.Stop(immediate))
        {
            //Debug.Log("Sound is not active - " + sound);
        }
    }
}
