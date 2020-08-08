using System;
using UnityEngine;
using Single;

public class SoundManager : MonoSingleton<SoundManager>
{

    private A_Sound3D[] sounds;


    public void SetLevel()
    {
        UnsetLevel();

        sounds = FindObjectsOfType<A_Sound3D>();
    }

    public void Pause(bool option)
    {
        if (sounds != null)
        {
            if (option)
            {
                for (int i = 0; i < sounds.Length; i++)
                {
                    sounds[i].Pause();
                }

                VoiceManager.get.Pause();
                MusicManager.get.Pause();
                NoiseManager.get.Pause();

            }
            else
            {
                for (int i = 0; i < sounds.Length; i++)
                {
                    sounds[i].Continue();
                }

                VoiceManager.get.Continue();
                MusicManager.get.Continue();
                NoiseManager.get.Continue();
            }
        }
    }




    public void UnsetLevel()
    {
        if (sounds != null)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].Stop();
            }

            VoiceManager.get.Stop();
            MusicManager.get.Pause();
            NoiseManager.get.Stop();

            sounds = null;
        }
    }
}
