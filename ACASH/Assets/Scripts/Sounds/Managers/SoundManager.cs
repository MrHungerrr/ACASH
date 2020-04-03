using System;
using UnityEngine;

public class SoundManager : A_Sound
{
    public enum sounds
    {
        Rain,
    }



    private static SoundManager _get;
    private static System.Object _lock = new System.Object();

    public static SoundManager get
    {
        get
        {
            if (_get == null)
            {
                
                lock(_lock)
                {
                    _get = FindObjectOfType<SoundManager>();

                    if (FindObjectsOfType<SoundManager>().Length > 1)
                    {
                        Debug.LogError("Несколько Синглтонов '" + typeof(SoundManager).ToString() + "' найдено! ");
                    }

                    if (_get == null)
                    {
                        Debug.Log("На сцене отсутсвует " + typeof(SoundManager).ToString());
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
        base.Setup("Global/Sounds/");

        for (int i = 0; i < Enum.GetNames(typeof(sounds)).Length; i++)
        {
            sounds name = (sounds)i;
            AddSoundWithoutAttach(name.ToString());
        }
    }






    //========================================================================================================================
    //========================================================================================================================
    //Не трогать

    public void Play(sounds sound)
    {
        base.Play(sound.ToString());
    }

    public void Pause(sounds sound)
    {
        base.Pause(sound.ToString());
    }

    public void Stop(sounds sound)
    {
        base.Stop(sound.ToString());
    }

    public void Stop(sounds sound, bool immediate)
    {
        base.Stop(sound.ToString(), immediate);
    }
}
