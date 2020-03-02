using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;
using Single;

public class SoundManager : Singleton<SoundManager>
{

    static string sounds_path = "event:/Sounds/";

    public enum sound   
    {
        Fuck,
    }



    public void Make(sound sound)
    {
        Debug.Log("One Shot Sound Play - " + sound.ToString());
        RuntimeManager.PlayOneShot(sounds_path + "One Shot/" + sound.ToString());
    }



}
