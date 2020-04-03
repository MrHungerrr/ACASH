using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public abstract class A_Sound: MonoBehaviour
{
    protected string sounds_path = "event:/";


    protected Dictionary<string, FMODAudio> FMODsounds = new Dictionary<string, FMODAudio>();
    public GameObject obj { get; private set; }
    public string[] ignore_tags { get; private set; } = null;

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

    protected void SetIgnore(string ignore_tag)
    {

        this.ignore_tags = new string[] { ignore_tag };
    }

    protected void SetIgnore(string[] ignore_tags)
    {
        this.ignore_tags = ignore_tags;
    }




    public void Update()
    {
        if (UpdateSound != null)
            UpdateSound();
    }

    protected void AddSound(string name)
    {
        try
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
                Debug.LogError("No object to Attach - " + name);
            }
        }
        catch
        {
            Debug.LogError("ERROR in adding Sound - " + name);
        }
        
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
            Debug.LogError("ERROR in adding Sound - " + name);
        }
    }



    protected virtual void Play(string sound)
    {
        try
        {
            if(FMODsounds[sound].Play())
            {
                Debug.Log("Sound Play - " + sound);
            }
            else
            {
                //Debug.Log("Sound is already playing - " + sound);
            }

        }
        catch
        {
            Debug.LogError("Sound is MISSING - " + sound);
        }
    }

    protected virtual void PlayAnyway(string sound)
    {
        try
        {
            FMODsounds[sound].PlayAnyway();
            Debug.Log("Sound Play - " + sound);
        }
        catch
        {
            Debug.LogError("Sound is MISSING - " + sound);
        }
    }



    protected virtual void Pause(string sound)
    {
        if (!FMODsounds[sound].Pause())
        {
            //Debug.Log("Sound is not playing - " + sound);
        }

    }

    //Остановка Sound
    protected void Stop(string sound)
    {
        Stop(sound, false);
    }

    protected virtual void Stop(string sound, bool immediate)
    {
        if(!FMODsounds[sound].Stop(immediate))
        {
            //Debug.Log("Sound is not active - " + sound);
        }
    }
}
