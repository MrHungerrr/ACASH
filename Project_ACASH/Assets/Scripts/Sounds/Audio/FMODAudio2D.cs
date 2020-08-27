using UnityEngine;
using FMODUnity;

public class FMODAudio2D : FMODAudioBase
{
    protected new SoundHolder2D _main;



    public FMODAudio2D(SoundHolderBase main, FMOD.Studio.EventInstance sound) : base(main, sound)
    {

    }
}
