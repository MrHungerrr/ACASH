using UnityEngine;
using FMODUnity;

public class  FMODAudio
{
    private A_Sound main;

    private FMOD.Studio.EventInstance sound;
    private FMOD.Studio.PARAMETER_ID volume, LPF;


    private bool attached;


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

    public void Update()
    {
        if(playing)
        {
            if(attached)
            {
                OcclusionCalculate();
            }

            IsPlaying();
        }
    }


    public void IsPlaying()
    {
        FMOD.Studio.PLAYBACK_STATE buf;
        sound.getPlaybackState(out buf);

        if(buf == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            End();
        }
    }


    public void OcclusionCalculate()
    {
        if(Player.get.Hear.GetOcclusion(main.obj))
        {
            sound.setParameterByID(volume, 0.5f);
            sound.setParameterByID(LPF, 10000);
        }
        else
        {
            sound.setParameterByID(volume, 1);
            sound.setParameterByID(LPF, 22000f);
        }
    }


    public bool Play()
    {
        if (!active && !playing)
        {
            sound.start();
            playing = true;
            active = true;
            main.UpdateSound += Update;

            return true;
        }
        else
        {
            return false;
        }
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

    public bool Continue()
    {
        if (active && !playing)
        {
            sound.setPaused(false);
            playing = true;
            return true;
        }
        else
            return false;

    }

    public bool Stop()
    {
        return Stop(false);
    }

    public bool Stop(bool immediate)
    {
        if (active)
        {
            if (immediate)
                sound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            else
                sound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            active = false;

            return true;
        }
        else
        {
            return false;
        }
    }

    private void End()
    {
        main.UpdateSound -= Update;
        playing = false;
    }
}
