using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class StressCell : MonoBehaviour
{
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
        scholar = s;
        scholar_name.text = scholar.name_1 + " " + scholar.name_2;
        Debug.Log("Имя чувака - " + scholar.name_1 + " " + scholar.name_2);
        Debug.Log("Стресс чувака - " + scholar.name_1 + " " + scholar.name_2);
    }

    public void Refresh()
    {
        stress = scholar.stress;
        Debug.Log("Стресс чувака - " + stress/10);
        slider.Select(stress / 10);
        Mood(scholar.GetMoodType());
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
