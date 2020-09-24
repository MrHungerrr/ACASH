using UnityEngine;
using FMODUnity;

public class  FMODAudioBase
{
    public bool IsActive { get; private set; } = false;
    public bool IsPlaying { get; private set; } = false;


    protected SoundHolderBase _main;

    protected FMOD.Studio.EventInstance _sound;



    public FMODAudioBase(SoundHolderBase main, FMOD.Studio.EventInstance sound)
    {
        this._main = main;
        this._sound = sound;
    }

    protected virtual void Update()
    {
        if(IsPlaying)
        {
            Playing();
        }
    }


    protected void Playing()
    {
        FMOD.Studio.PLAYBACK_STATE buf;
        _sound.getPlaybackState(out buf);

        if(buf == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            Finish();
        }
    }


    public void Play()
    {
        if (!IsPlaying)
        {
            if (!IsActive)
            {
                Start();
            }
            else
            {
                Continue();
            }
        }
    }


    public void PlayAnyway()
    {
        if(IsPlaying)
        {
            Stop(true);
        }

        Play();
    }


    protected void Start()
    {
        _sound.start();
        IsPlaying = true;
        IsActive = true;
        _main.OnUpdateSound += Update;
    }

    public void Pause()
    {
        if (IsActive && IsPlaying)
        {
            _sound.setPaused(true);
            IsPlaying = false;
        }
    }

    public void Continue()
    {
        if (IsActive && !IsPlaying)
        {
            _sound.setPaused(false);
            IsPlaying = true;
        }
    }

    public void Stop(bool immediate)
    {
        if (IsActive)
        {
            if (immediate)
                _sound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            else
                _sound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            Finish();
        }
    }

    protected void Finish()
    {
        IsPlaying = false;
        IsActive = false;
        _main.OnUpdateSound -= Update;
    }
}
