using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;

public class VoiceManager : A_SoundBase
{

    private FMODAudioBase voice;


    //====================================================================================================
    //Singleton
    private static VoiceManager _get;
    private static System.Object _lock = new System.Object();

    public static VoiceManager get
    {
        get
        {
            if (_get == null)
            {

                lock (_lock)
                {
                    _get = FindObjectOfType<VoiceManager>();

                    if (FindObjectsOfType<VoiceManager>().Length > 1)
                    {
                        Debug.LogError("Несколько Синглтонов '" + typeof(VoiceManager).ToString() + "' найдено! ");
                    }

                    if (_get == null)
                    {
                        Debug.Log("На сцене отсутсвует " + typeof(VoiceManager).ToString());
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
        base.Setup("Voice/");
    }


    public void Play(KeyWord key)
    {
        string full_path = sounds_path + ScriptManager.Instance.voiceLanguage + '/' + key.GetMain() + '/' + key.GetFullWord();

        try 
        {
            FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(full_path);
            voice = new FMODAudio2D(this, sound);
            Play(voice);
        }
        catch
        {
            Debug.LogError("Неправильный путь - " + full_path);
        }
    }

    public void Pause()
    {
        if (voice != null)
            voice.Pause();
    }

    public void Continue()
    {
        if(voice != null)
            voice.Continue();
    }


    public void Stop()
    {
        if (voice != null)
            Stop(voice, true);
    }
}
