using System;
using UnityEngine;
using Single;

public class SoundManager : Singleton<SoundManager>
{

    A_Sound3D[] sounds;


    public void SetLevel()
    {
        UnsetLevel();

        sounds = FindObjectsOfType<A_Sound3D>();

        foreach (A_Sound3D sound in sounds)
        {
            sound.Enable(false);
        }
    }


    public void UnsetLevel()
    {
        if (sounds != null)
        {
            foreach (A_Sound3D sound in sounds)
            {
                sound.Enable(false);
            }

            sounds = null;
        }
    }
}
