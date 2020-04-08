using System;
using UnityEngine;

public class NoiseManager : A_Sound2D
{
    public enum sounds
    {
        Rain,
    }



    private static NoiseManager _get;
    private static System.Object _lock = new System.Object();

    public static NoiseManager get
    {
        get
        {
            if (_get == null)
            {
                
                lock(_lock)
                {
                    _get = FindObjectOfType<NoiseManager>();

                    if (FindObjectsOfType<NoiseManager>().Length > 1)
                    {
                        Debug.LogError("Несколько Синглтонов '" + typeof(NoiseManager).ToString() + "' найдено! ");
                    }

                    if (_get == null)
                    {
                        Debug.Log("На сцене отсутсвует " + typeof(NoiseManager).ToString());
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


    protected void Setup()
    {
        base.Setup("Global/Sounds/");

        for (int i = 0; i < Enum.GetNames(typeof(sounds)).Length; i++)
        {
            sounds name = (sounds)i;
            AddSound2D(name.ToString());
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
