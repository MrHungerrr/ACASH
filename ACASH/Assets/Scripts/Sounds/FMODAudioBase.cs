using UnityEngine;
using FMODUnity;

public class  FMODAudioBase
{
    protected A_SoundBase main;

    protected FMOD.Studio.EventInstance sound;

    public bool active { get; private set; } = false;
    public bool playing { get; private set; } = false;



    public FMODAudioBase(A_SoundBase main, FMOD.Studio.EventInstance sound)
    {
        this.main = main;
        this.sound = sound;
    }

    protected virtual void Update()
    {
        if(playing)
        {
            IsPlaying();
        }
    }


    protected void IsPlaying()
    {
        FMOD.Studio.PLAYBACK_STATE buf;
        sound.getPlaybackState(out buf);

        if(buf == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            Finish();
        }
    }


    public bool Play()
    {
        if (!playing)
        {
            if (!active)
            {
                Start();
            }
            else
            {
                Continue();
            }

            return true;
        }
        else
        {
            return false;
        }
    }


    public void PlayAnyway()
    {
        if(!Play())
        {
            Stop(true);
            Play();
        }
    }


    protected void Start()
    {
        sound.start();
        playing = true;
        active = true;
        main.UpdateSound += Update;
    }

    public bool Pause()
    {
        if (active && playing)
        {
            sound.setPaused(true);
            playing = false;
            return true;
        }
        else
            return false;
    }

    protected void Continue()
    {
        sound.setPaused(false);
        playing = true;
    }

    public bool Stop(bool immediate)
    {
        if (active)
        {
            if (immediate)
                sound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            else
                sound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            Finish();

            return true;
        }
        else
        {
            return false;
        }
    }

    protected void Finish()
    {
        playing = false;
        active = false;
        main.UpdateSound -= Update;
    }
}
