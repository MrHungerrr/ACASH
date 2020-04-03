using UnityEngine;
using FMODUnity;

public class  FMODAudio
{
    private A_Sound main;

    private FMOD.Studio.EventInstance sound;
    private FMOD.Studio.PARAMETER_ID volume, LPF;


    private bool attached;
    private bool occlusion = true;


    public bool active { get; private set; } = false;
    public bool playing { get; private set; } = false;



    public FMODAudio(A_Sound main ,FMOD.Studio.EventInstance sound, bool attached)
    {
        this.main = main;
        this.sound = sound;
        this.attached = attached;



        if (attached)
        {
            FMOD.Studio.EventDescription description;
            sound.getDescription(out description);

            FMOD.Studio.PARAMETER_DESCRIPTION volume_d, LPF_d;

            description.getParameterDescriptionByName("Volume", out volume_d);
            description.getParameterDescriptionByName("LPF", out LPF_d);

            volume = volume_d.id;
            LPF = LPF_d.id;
        }
    }

    public void SetOcclusion(bool option)
    {
        occlusion = option;
    }

    private void Update()
    {
        if(playing)
        {
            if(attached && occlusion)
            {
                OcclusionCalculate();
            }

            IsPlaying();
        }
    }


    private void IsPlaying()
    {
        FMOD.Studio.PLAYBACK_STATE buf;
        sound.getPlaybackState(out buf);

        if(buf == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            Finish();
        }
    }


    private void OcclusionCalculate()
    {
        if(Player.get.Hear.GetOcclusion(main.obj, main.ignore_tags))
        {
            sound.setParameterByID(volume, 0.75f);
            sound.setParameterByID(LPF, 12000f);
        }
        else
        {
            sound.setParameterByID(volume, 1);
            sound.setParameterByID(LPF, 22000f);
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

    private void Start()
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

    private void Continue()
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

    private void Finish()
    {
        playing = false;
        active = false;
        main.UpdateSound -= Update;
    }
}
