using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;
using N_BH;


public class PostProcessManager: Singleton<PostProcessManager>
{
    private PostProcessVolume volume;
    private DepthOfField depthOfField;
    private ColorGrading colorGrading;
    private const float dof_coef = 5f;


    void Awake()
    {
        volume = GameObject.FindObjectOfType<PostProcessVolume>();
    }


    public void Blur(bool option)
    {
        StopAllCoroutines();

        if(option)
        {
            StartCoroutine(BlurIn());
        }
        else
        {
            StartCoroutine(BlurOut());
        }
    }

    public void SetAperture(float value)
    {
        if (volume.profile.TryGetSettings(out depthOfField))
            depthOfField.aperture.value = value;
    }

    public float GetAperture()
    {
        depthOfField = volume.profile.GetSetting<DepthOfField>();
        return depthOfField.aperture.value;
    }

    public void SetFocusDistance(float value)
    {
        if (volume.profile.TryGetSettings(out depthOfField))
            depthOfField.focusDistance.value = value;
    }

    public float GetFocuseDistance()
    {
        depthOfField = volume.profile.GetSetting<DepthOfField>();
        return depthOfField.focusDistance.value;
    }

    public void SetContrast(float value)
    {
        //Debug.Log("Change Contrast");
        if (volume.profile.TryGetSettings(out colorGrading))
            colorGrading.contrast.value = value;
    }

    public float GetContrast()
    {
        //Debug.Log("Get Contrast");
        colorGrading = volume.profile.GetSetting<ColorGrading>();
        return colorGrading.contrast.value;
    }

    public void SetSaturation(float value)
    {
        //Debug.Log("Change Saturation");
        if (volume.profile.TryGetSettings(out colorGrading))
            colorGrading.saturation.value = value;
    }

    public float GetSaturation()
    {
        //Debug.Log("Get Saturation");
        colorGrading = volume.profile.GetSetting<ColorGrading>();
        return colorGrading.saturation.value;
    }


    private IEnumerator BlurIn()
    {
        float aperture = 10f;
        float focuse = 0.1f;
        float contrast = -20f;
        float saturation = 0f;

        SetFocusDistance(focuse);

        while (aperture > 0.1)
        {
            aperture = Mathf.Lerp(aperture, 0.05f, Time.unscaledDeltaTime * dof_coef);
            contrast = Mathf.Lerp(contrast, -60f, Time.unscaledDeltaTime * dof_coef *2);
            saturation = Mathf.Lerp(saturation, -60f, Time.unscaledDeltaTime * dof_coef *2);

            SetAperture(aperture);
            SetContrast(contrast);
            SetSaturation(saturation);

            yield return new WaitForEndOfFrame();
        }
    }
    private IEnumerator BlurOut()
    {
        float aperture = GetAperture();
        float focuse = GetFocuseDistance();
        float contrast = GetContrast();
        float saturation = GetSaturation();

        while (aperture < 32)
        {
            aperture = Mathf.Lerp(aperture, 32.001f, Time.unscaledDeltaTime * dof_coef);
            focuse = Mathf.Lerp(focuse, 30, Time.unscaledDeltaTime * dof_coef);
            contrast = Mathf.Lerp(contrast, 100f, Time.unscaledDeltaTime * dof_coef);
            saturation = Mathf.Lerp(saturation, 100f, Time.unscaledDeltaTime * dof_coef);

            SetAperture(aperture);
            SetFocusDistance(focuse);
            SetContrast(contrast);
            SetSaturation(saturation);

            yield return new WaitForEndOfFrame();
        }
    }

}
