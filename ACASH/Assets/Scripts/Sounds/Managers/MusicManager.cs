using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;

public class MusicManager : A_Sound
{

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
                    if (_get == null)
                    {
                        _get = new MusicManager();
                    }
                }
            }

            return _get;
        }
    }


    public enum music
    {
        Track_1,
    }



    private MusicManager()
    {
        Setup();
    }


    protected override void Setup()
    {
        sounds_path += "Global/Music/";
        base.Setup();

        for (int i = 0; i < Enum.GetNames(typeof(music)).Length; i++)
        {
            music name = (music)i;
            AddInfiniteWithoutAttach(name.ToString());
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

    public void Continue(music sound)
    {
        base.Continue(sound.ToString());
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
