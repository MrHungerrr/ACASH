using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;
using Computers;


public class ComputerSounds : SoundHolder3D
{
    private readonly A_Computer _computer;

    public enum sounds
    {
        Load,
        Click,
    }

    public ComputerSounds(A_Computer computer, GameObject soundHost) : base(soundHost, "Local/Computer")
    {
        _computer = computer;
        SetupSounds();
    }

    protected override void SetupSounds()
    {
        for (int i = 0; i < Enum.GetNames(typeof(sounds)).Length; i++)
        {
            sounds name = (sounds)i;
            AddSound3D(name.ToString());
        }
    }






    //========================================================================================================================
    //========================================================================================================================
    //Не трогать


    public void Play(sounds sound)
    {
        base.Play(sound.ToString());
    }

    public void PlayAnyway(sounds sound)
    {
        base.PlayAnyway(sound.ToString());
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
        base.Stop(sound.ToString(),immediate);
    }
}
