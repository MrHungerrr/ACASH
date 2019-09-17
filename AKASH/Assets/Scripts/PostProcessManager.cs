using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;


public class PostProcessManager: MonoBehaviour
{
    private PostProcessVolume volume;
    private DepthOfField depthOfField;
    private ColorGrading colorGrading;


    void Start()
    {
        volume = GameObject.FindObjectOfType<PostProcessVolume>();
    }

    public void DOF(float value)
    {
        Debug.Log("Change DOF");
        if (volume.profile.TryGetSettings(out depthOfField))
            depthOfField.aperture.value = value;
    }

    public float GetDOF()
    {
        Debug.Log("Get DOF");
        depthOfField = volume.profile.GetSetting<DepthOfField>();
          return depthOfField.aperture.value;
    }

    public void Contrast(float value)
    {
        Debug.Log("Change Contrast");
        if (volume.profile.TryGetSettings(out colorGrading))
            colorGrading.contrast.value = value;
    }

    public float GetContrast()
    {
        Debug.Log("Get Contrast");
        colorGrading = volume.profile.GetSetting<ColorGrading>();
        return colorGrading.contrast.value;
    }

    public void Saturation(float value)
    {
        Debug.Log("Change Saturation");
        if (volume.profile.TryGetSettings(out colorGrading))
            colorGrading.saturation.value = value;
    }

    public float GetSaturation()
    {
        Debug.Log("Get Saturation");
        colorGrading = volume.profile.GetSetting<ColorGrading>();
        return colorGrading.saturation.value;
    }

}
