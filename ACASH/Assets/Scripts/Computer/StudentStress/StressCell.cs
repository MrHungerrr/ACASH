using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class StressCell : MonoBehaviour
{
    private bool active = false;
    private Scholar scholar;
    private TextMeshProUGUI emotion;
    private TextMeshProUGUI scholar_name;
    private SliderWatch slider;
    private int stress;

    private void Awake()
    {
        slider = transform.GetComponentInChildren<SliderWatch>();
        emotion = transform.Find("Emotion").GetComponent<TextMeshProUGUI>();
        scholar_name = transform.Find("Name").GetComponent<TextMeshProUGUI>();
    }

    public void Set(Scholar s)
    {
        active = true;
        scholar = s;
        scholar_name.text = scholar.Info.surname + " " + scholar.Info.name;
    }

    public void Refresh()
    {
        if (active)
        {
            stress = scholar.Stress.value;
            //Debug.Log("Стресс чувака - " + stress/10);
            slider.Select(stress / 10);
            Mood(scholar.Stress.GetMoodType());
        }
    }

    private void Mood(string mood)
    {
        switch(mood)
        {
            case"chill":
                {
                    emotion.text = ":)";
                    break;
                }
            case "normal":
                {
                    emotion.text = ":|";
                    break;
                }
            case "panic":
                {
                    emotion.text = ":(";
                    break;
                }
        }
    }
}
