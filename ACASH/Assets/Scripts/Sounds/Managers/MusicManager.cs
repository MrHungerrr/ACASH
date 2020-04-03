using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;

public class MusicManager : A_Sound
{
    public enum music
    {
        Track_1,
    }


    //====================================================================================================
    //Singleton
    private static MusicManager _get;
    private static System.Object _lock = new System.Object();

    public static MusicManager get
    {
        get
        {
            if (_get == null)
            {

                lock (_lock)
                {
                    _get = FindObjectOfType<MusicManager>();

                    if (FindObjectsOfType<MusicManager>().Length > 1)
                    {
                        Debug.LogError("Несколько Синглтонов '" + typeof(MusicManager).ToString() + "' найдено! ");
                    }

                    if (_get == null)
                    {
                        Debug.Log("На сцене отсутсвует " + typeof(MusicManager).ToString());
                        return null;
                    }
                }
            }

            return _get;
        }
    }


    private void Awake()
    {
        Setup();
    }


    protected override void Setup()
    {
        base.Setup("Global/Music/");

        for (int i = 0; i < Enum.GetNames(typeof(music)).Length; i++)
        {
            music name = (music)i;
            AddSoundWithoutAttach(name.ToString());
        }
    }






    //========================================================================================================================
    //========================================================================================================================
    //Не трогать

    public void Play(music sound)
    {
        base.Play(sound.ToString());
    }

    public void Pause(music sound)
    {
        base.Pause(sound.ToString());
    }

    public void Stop(music sound)
    {
        base.Stop(sound.ToString());
    }

    public void Stop(music sound, bool immediate)
    {
        base.Stop(sound.ToString(), immediate);
    }
}
