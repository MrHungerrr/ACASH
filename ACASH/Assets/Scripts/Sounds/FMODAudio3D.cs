using UnityEngine;
using FMODUnity;

public class  FMODAudio3D: FMODAudio2D
{
    protected new A_Sound3D main;

    private FMOD.Studio.PARAMETER_ID volume, LPF;

    public FMODAudio3D(A_Sound3D main, FMOD.Studio.EventInstance sound) : base(main, sound)
    {
        FMOD.Studio.EventDescription description;
        sound.getDescription(out description);

        FMOD.Studio.PARAMETER_DESCRIPTION volume_d, LPF_d;

        description.getParameterDescriptionByName("Volume", out volume_d);
        description.getParameterDescriptionByName("LPF", out LPF_d);

        volume = volume_d.id;
        LPF = LPF_d.id;

        main.OcclusionUpdate += SetOcclusion;
    }


    protected override void Update()
    {
        if (playing)
        {

            IsPlaying();
        }
    }


    public void SetOcclusion(bool occlusion)
    {
        if (occlusion)
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
}
