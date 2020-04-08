using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public abstract class A_Sound3D: A_Sound2D
{
    public GameObject obj { get; private set; }
    public string[] ignore_tags { get; private set; } = null;


    public event ActionEvent.OnActionBool OcclusionUpdate;
    private bool occlusion;





    protected virtual void Setup(GameObject obj, string path)
    {
        this.obj = obj;
        base.Setup(path);
    }



    protected void AddSound3D(string name)
    {
        try
        {
            if (obj != null)
            {
                FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(sounds_path + name);
                RuntimeManager.AttachInstanceToGameObject(sound, obj.transform, obj.GetComponent<Rigidbody>());
                FMODAudio3D audio = new FMODAudio3D(this, sound);
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



    protected override void Update()
    {
        if (OcclusionCalculate() && OcclusionUpdate != null)
            OcclusionUpdate(occlusion);

        base.Update();
    }






    protected void SetIgnore(string ignore_tag)
    {
        this.ignore_tags = new string[] { ignore_tag };
    }

    protected void SetIgnore(string[] ignore_tags)
    {
        this.ignore_tags = ignore_tags;
    }





    private bool OcclusionCalculate()
    {
        if(occlusion != Player.get.Hear.GetOcclusion(obj, ignore_tags))
        {
            occlusion = !occlusion;
            return true;
        }

        return false;
    }
}
