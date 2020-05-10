using UnityEngine;
using FMODUnity;

public class FMODAudio2D : FMODAudioBase
{
    protected new A_Sound2D main;



    public FMODAudio2D(A_SoundBase main, FMOD.Studio.EventInstance sound) : base(main, sound)
    {

    }
}
