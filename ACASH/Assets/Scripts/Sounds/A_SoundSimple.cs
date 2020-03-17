using UnityEngine;
using FMODUnity;

public abstract class A_SoundSimple
{
    protected string sounds_path = "event:/";

    protected GameObject obj;


    protected void Make(string sound)
    {
        try
        {
            Debug.Log("One Shot Sound Play - " + sound);
            RuntimeManager.PlayOneShotAttached(sounds_path + "One Shot/" + sound.ToString(), obj);
        }
        catch
        {
            Debug.LogError("One Shot Sound is MISSING - " + sound);
        }
    }

    protected void MakeWithoutAttach(string sound)
    {
        try
        {
            Debug.Log("One Shot Sound Play - " + sound);
            RuntimeManager.PlayOneShot(sounds_path + "One Shot/" + sound.ToString());
        }
        catch
        {
            Debug.LogError("One Shot Sound is MISSING - " + sound);
        }
    }
}
