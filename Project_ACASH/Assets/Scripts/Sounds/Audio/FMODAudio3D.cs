using UnityEngine;
using FMODUnity;

public class  FMODAudio3D: FMODAudio2D
{
    protected new SoundHolder3D _main;

    private FMOD.Studio.PARAMETER_ID _volume, _LPF;


    public FMODAudio3D(SoundHolder3D main, FMOD.Studio.EventInstance sound) : base(main, sound)
    {
        FMOD.Studio.EventDescription description;
        sound.getDescription(out description);

        FMOD.Studio.PARAMETER_DESCRIPTION volume_d, LPF_d;

        description.getParameterDescriptionByName("Volume", out volume_d);
        description.getParameterDescriptionByName("LPF", out LPF_d);

        _volume = volume_d.id;
        _LPF = LPF_d.id;

        main.OcclusionUpdate += SetOcclusion;
    }


    protected override void Update()
    {
        if (IsPlaying)
        {
            Playing();
        }
    }


    public void SetOcclusion(bool occlusion)
    {
        if (occlusion)
        {
            _sound.setParameterByID(_volume, 0.75f);
            _sound.setParameterByID(_LPF, 12000f);
        }
        else
        {
            _sound.setParameterByID(_volume, 1);
            _sound.setParameterByID(_LPF, 22000f);
        }
    }
}
