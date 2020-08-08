using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Single;

public class FadeController : MonoSingleton<FadeController>
{
    [HideInInspector]
    public bool active;
    private bool fadeNeed;
    private float currentFade;
    private float targetFade;
    private Image fade;

    private void Awake()
    {
        fade = GetComponent<Image>();
    }

    private void Update()
    {
        if(active)
            Fading();
    }

    public void Fade(bool option)
    {
        fadeNeed = option;

        if (option)
        {
            targetFade = 1f;
        }
        else
        {
            targetFade = 0f;
        }

        active = true;
    }

    public void FastFade(bool option)
    {
        if (option)
        {
            currentFade = 1f;
        }
        else
        {
            currentFade = 0f;
        }

        fade.color = new Color(0, 0, 0, currentFade);

        active = false;
    }


    private void Fading()
    {
        if(fadeNeed)
        {
            currentFade = Mathf.Lerp(currentFade, (targetFade + 0.01f), Time.unscaledDeltaTime * 15f);
        }
        else
        {
            currentFade = Mathf.Lerp(currentFade, (targetFade - 0.01f), Time.unscaledDeltaTime * 2f);
        }

        fade.color = new Color(0, 0, 0, currentFade);

        if ((targetFade - Mathf.Sign(targetFade - 0.5f)*currentFade) < 0.00001f)
        {
            currentFade = targetFade;
            fade.color = new Color(0, 0, 0, currentFade);
            active = false;
        }
    }

}
