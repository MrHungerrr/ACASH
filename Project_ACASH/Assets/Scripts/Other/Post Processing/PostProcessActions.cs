using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityTools.Single;


namespace PostProcessing
{

    public class PostProcessActions : MonoSingleton<PostProcessActions>
    {
        private const float _timeCoef = 10f;


        public void Blur(bool option)
        {
            StopAllCoroutines();

            //if (option)
            //{
            //    StartCoroutine(BlurIn());
            //}
            //else
            //{
            //    StartCoroutine(BlurOut());
            //}
        }

        private IEnumerator BlurIn()
        {
            float aperture = 10f;
            float focuse = 0.1f;
            float focal = 99f;
            float contrast = -20f;
            float saturation = 0f;

            PostProcessManager.SetFocusDistance(focuse);
            PostProcessManager.SetFocalLength(focal);

            while (aperture > 0.1)
            {
                aperture = Mathf.Lerp(aperture, 0.05f, Time.unscaledDeltaTime * _timeCoef);
                contrast = Mathf.Lerp(contrast, -60f, Time.unscaledDeltaTime * _timeCoef);
                saturation = Mathf.Lerp(saturation, -60f, Time.unscaledDeltaTime * _timeCoef);

                PostProcessManager.SetAperture(aperture);
                PostProcessManager.SetContrast(contrast);
                PostProcessManager.SetSaturation(saturation);

                yield return new WaitForEndOfFrame();
            }
        }
        private IEnumerator BlurOut()
        {
            float aperture = PostProcessManager.GetAperture();
            float focuse = PostProcessManager.GetFocuseDistance();
            float focal = 1f;
            float contrast = PostProcessManager.GetContrast();
            float saturation = PostProcessManager.GetSaturation();

            PostProcessManager.SetFocalLength(focal);

            while (aperture < 32)
            {
                aperture = Mathf.Lerp(aperture, 32.001f, Time.unscaledDeltaTime * _timeCoef);
                focuse = Mathf.Lerp(focuse, 30, Time.unscaledDeltaTime * _timeCoef);
                contrast = Mathf.Lerp(contrast, 100f, Time.unscaledDeltaTime * _timeCoef);
                saturation = Mathf.Lerp(saturation, 100f, Time.unscaledDeltaTime * _timeCoef);

                PostProcessManager.SetAperture(aperture);
                PostProcessManager.SetFocusDistance(focuse);
                PostProcessManager.SetContrast(contrast);
                PostProcessManager.SetSaturation(saturation);

                yield return new WaitForEndOfFrame();
            }
        }

    }
}
