using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Searching;

public class ScholarHUD
{
    private Scholar Scholar;

    Transform HUD;

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

        HUD = Scholar.transform.Find("HUD");
        text = HUD.Find("Canvas").Find("Stress").GetComponent<TextMeshProUGUI>();

        SIC<Image>.Components(HUD.gameObject, out images);
        SIC<TextMeshProUGUI>.Components(HUD.gameObject, out texts);

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

        Disapearing();
    }




    private void Set(int value)
    {
        stress = value;
        text.text = "Stress:" + stress + "%";
    }


    public void Enable()
    {
        if (Scholar.active)
        {
            if (!active)
            {
                active = true;
                fade_previous = fade;
                fading = true;

                fade_target = 1f;
                Set((int)Scholar.Stress.value);
                time = time_const;

            }
            else
            {
                time = time_const;
            }
        }
    }



    private void Disable()
    {
        active = false;
        fade_previous = fade;
        fading = true;
        fade_target = 0f;
    }

    private void Fading()
    {
        time_fade += Time.deltaTime;

        if (time_fade < time_fade_const)
        {
            Fade(Mathf.Lerp(fade_previous, fade_target, (time_fade / time_fade_const)));
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
        HUD.rotation = BaseGeometry.GetQuaternionToY(HUD, Player.Instance.Camera.transform.position);
    }


    private void Disapearing()
    {

        if (time <= 0)
        {
            Disable();
        }
        else
            time -= Time.deltaTime;
    }
}
