using System;
using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public abstract class SoundHolder3D: SoundHolder2D
{
    public event Action<bool> OcclusionUpdate;


    private readonly GameObject _host;
    private string[] _ignoreTags;
    private bool _occlusion;


    protected SoundHolder3D(GameObject host, string path) : base(path)
    {
        _host = host;
        AudioManager.Instance.WorldSounds.Add(this);
    }


    protected void AddSound3D(string name)
    {
        try
        {
            if (_host != null)
            {
                if (!FMODsounds.ContainsKey(name))
                {
                    FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(_soundPath + name);
                    RuntimeManager.AttachInstanceToGameObject(sound, _host.transform, _host.GetComponent<Rigidbody>());
                    FMODAudio3D audio = new FMODAudio3D(this, sound);
                    FMODsounds.Add(name, audio);
                }
                else
                {
                    Debug.LogError("Sound is Already Added - " + name);
                }
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



    protected override void Update()
    {
        /*
        if (OcclusionCalculate())
            OcclusionUpdate(occlusion);
            */

        UpdateSound?.Invoke();
    }






    protected void SetIgnore(string ignore_tag)
    {
        this._ignoreTags = new string[] { ignore_tag };
    }

    protected void SetIgnore(string[] ignore_tags)
    {
        this._ignoreTags = ignore_tags;
    }





    private bool OcclusionCalculate()
    {
        if(_occlusion != Player.Instance.Hear.GetOcclusion(_host, _ignoreTags))
        {
            _occlusion = !_occlusion;
            return true;
        }

        return false;
    }
}
