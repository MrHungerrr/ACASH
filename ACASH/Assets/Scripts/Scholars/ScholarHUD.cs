using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Searching;

public class ScholarHUD
{
    private Scholar Scholar;

    Transform main;

    TextMeshProUGUI text;
    private bool active = false;
    private bool fading = false;


    private Image[] images;
    private TextMeshProUGUI[] texts;
    private float fade_previous;
    private float fade;
    private float fade_target;
    private float time_fade = 0f;
    private const float time_fade_const = 0.3f;


    private int stress_previous;
    private int stress;
    private int stress_target;
    private float time = 0f;
    private const float time_const = 1f;


    public ScholarHUD(Scholar Scholar)
    {
        Setup(Scholar);
    }


    private void Setup(Scholar Scholar)
    {
        this.Scholar = Scholar;

        main = Scholar.transform.Find("HUD");
        text = main.Find("Canvas").Find("Stress").GetComponent<TextMeshProUGUI>();

        SIC<Image>.Components(main.gameObject, out images);
        SIC<TextMeshProUGUI>.Components(main.gameObject, out texts);

        Fade(0f);
    }


    public void Update()
    {
        if (active)
        {
            Changing();
        }

        if (fading)
        {
            Fading();
        }
    }
   

    private void Changing()
    {

        Set(Scholar.Stress.value_show);

        Rotate();
    }




    private void Set(int value)
    {
        stress = value;
        text.text = "Stress:" + stress + "%";
    }


    public void Enable(bool option)
    {
        if (active != option && Scholar.active)
        {
            active = option;
            fade_previous = fade;
            fading = true;

            if (option)
            {
                fade_target = 1f;
                Set((int)Scholar.Stress.value);
                time = time_const;
            }
            else
                fade_target = 0f;
        }
    }

    private void Fading()
    {
        time_fade += Time.deltaTime;

        if (time_fade < time_fade_const)
        {
            Fade(Mathf.Lerp(fade_previous, fade_target, (time_fade/time_fade_const)));
        }
        else
        {
            Fade(fade_target);
            time_fade = 0f;
            fading = false;
        }
    }
    



    private void Fade(float value)
    {
        fade = value;

        foreach (Image i in images)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, value);
        }

        foreach (TextMeshProUGUI t in texts)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, value);
        }
    }


    private void Rotate()
    {
        main.rotation = BaseGeometry.GetQuaternionToY(main, Player.get.Camera.transform.position);
    }
}
