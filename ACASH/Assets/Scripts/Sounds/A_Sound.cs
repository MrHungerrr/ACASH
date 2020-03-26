using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public abstract class A_Sound: MonoBehaviour
{
    protected string sounds_path = "event:/";


    protected Dictionary<string, FMODAudio> FMODsounds = new Dictionary<string, FMODAudio>();
    public GameObject obj { get; private set; }

    public event ActionEvent.OnAction UpdateSound;


    protected virtual void Setup()
    {
        obj = null;
    }

    protected virtual void Setup(GameObject obj)
    {
        this.obj = obj;
    }

    protected virtual void Setup(string path)
    {
        obj = null;
        this.sounds_path += path;
    }

    protected virtual void Setup(GameObject obj, string path)
    {
        this.obj = obj;
        this.sounds_path += path;
    }

    public void Update()
    {
        if (UpdateSound != null)
            UpdateSound();
    }

    protected void AddSound(string name)
    {
        //try
        {
            if (obj != null)
            {
                FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(sounds_path + name);
                RuntimeManager.AttachInstanceToGameObject(sound, obj.transform, obj.GetComponent<Rigidbody>());
                FMODAudio audio = new FMODAudio(this, sound, true);
                FMODsounds.Add(name, audio);
            }
            else
            {
                Debug.Log("No object to Attach - " + name);
            }
        }
       /* catch
        {
            Debug.Log("ERROR in adding Sound - " + name);
        }
        */
    }


    protected void AddSoundWithoutAttach(string name)
    {
        try
        {
            FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(sounds_path + name);
            FMODAudio audio = new FMODAudio(this, sound, false);
            FMODsounds.Add(name, audio);
        }
        catch
        {
            Debug.Log("ERROR in adding Sound - " + name);
        }
    }



    protected void Play(string sound)
    {
        try
        {
            if(FMODsounds[sound].Play())
            {
                Debug.Log("Sound Start - " + sound);
            }
            else
            {
                Debug.Log("Sound is already playing - " + sound);
            }

        }
        catch
        {
            Debug.Log("Sound is MISSING - " + sound);
        }
    }


    protected void Pause(string sound)
    {
        if (!FMODsounds[sound].Pause())
        {
            Debug.Log("Sound is not playing - " + sound);
        }

    }

    protected void Continue(string sound)
    {
        if (!FMODsounds[sound].Continue())
        {
            Debug.Log("Sound is not paused - " + sound);
        }
    }




    //Остановка Sound
    protected void Stop(string sound)
    {
        FMODsounds[sound].Stop();
    }

    protected void Stop(string sound, bool immediate)
    {
        if(!FMODsounds[sound].Stop(immediate))
        {
            Debug.Log("Sound is not active - " + sound);
        }
    }
}
