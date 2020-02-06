using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using N_BH;


public class PostProcessManager: Singleton<PostProcessManager>
{
    private VolumeProfile volume;
    private DepthOfField depthOfField;
    private ColorAdjustments colorAdjustments;
    private const float time_coef = 10f;


    void Awake()
    {
        volume = GameObject.FindObjectOfType<Volume>().profile;
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
        if (volume.TryGet(out depthOfField))
            depthOfField.aperture.value = value;
    }

    public float GetAperture()
    {
        if (volume.TryGet(out depthOfField))
            return depthOfField.aperture.value;
        else
            return 0;
    }

    public void SetFocusDistance(float value)
    {
        if (volume.TryGet(out depthOfField))
            depthOfField.focusDistance.value = value;
    }

    public float GetFocuseDistance()
    {
        if (volume.TryGet(out depthOfField))
            return depthOfField.focusDistance.value;
        else
            return 0;
    }

    public void SetFocalLength(float value)
    {
        if (volume.TryGet(out depthOfField))
            depthOfField.focalLength.value = value;
    }

    public float GetFocalLength()
    {
        if (volume.TryGet(out depthOfField))
            return depthOfField.focalLength.value;
        else
            return 0;
    }

    public void SetContrast(float value)
    {
        if (volume.TryGet(out colorAdjustments))
            colorAdjustments.contrast.value = value;
    }

    public float GetContrast()
    {
        if (volume.TryGet(out colorAdjustments))
            return colorAdjustments.contrast.value;
        else
            return 0;
    }

    public void SetSaturation(float value)
    {
        if (volume.TryGet(out colorAdjustments))
            colorAdjustments.saturation.value = value;
    }

    public float GetSaturation()
    {
        if (volume.TryGet(out colorAdjustments))
            return colorAdjustments.saturation.value;
        else
            return 0;
    }


    private IEnumerator BlurIn()
    {
        float aperture = 10f;
        float focuse = 0.1f;
        float focal = 99f;
        float contrast = -20f;
        float saturation = 0f;

        SetFocusDistance(focuse);
        SetFocalLength(focal);

        while (aperture > 0.1)
        {
            aperture = Mathf.Lerp(aperture, 0.05f, Time.unscaledDeltaTime * time_coef);
            contrast = Mathf.Lerp(contrast, -60f, Time.unscaledDeltaTime * time_coef);
            saturation = Mathf.Lerp(saturation, -60f, Time.unscaledDeltaTime * time_coef);

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
        float focal = 1f;
        float contrast = GetContrast();
        float saturation = GetSaturation();

        SetFocalLength(focal);

        while (aperture < 32)
        {
            aperture = Mathf.Lerp(aperture, 32.001f, Time.unscaledDeltaTime * time_coef);
            focuse = Mathf.Lerp(focuse, 30, Time.unscaledDeltaTime * time_coef);
            contrast = Mathf.Lerp(contrast, 100f, Time.unscaledDeltaTime * time_coef);
            saturation = Mathf.Lerp(saturation, 100f, Time.unscaledDeltaTime * time_coef);

            SetAperture(aperture);
            SetFocusDistance(focuse);   
            SetContrast(contrast);
            SetSaturation(saturation);

            yield return new WaitForEndOfFrame();
        }
    }

}
