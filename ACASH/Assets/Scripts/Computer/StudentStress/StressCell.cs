using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using ScholarOptions;


public class StressCell : MonoBehaviour
{


    private bool active = false;
    private Scholar scholar;
    private Image emotion;
    private TextMeshProUGUI scholar_name;
    private TextMeshProUGUI scholar_stress;
    private SliderWatch slider;
    private int stress;

    private void Awake()
    {
        slider = transform.GetComponentInChildren<SliderWatch>();
        slider.Setup();
        emotion = transform.Find("Emotion").Find("Face").GetComponent<Image>();
        scholar_name = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        scholar_stress = transform.Find("Stress").GetComponent<TextMeshProUGUI>();
    }

    public void Set(Scholar s)
    {
        active = true;
        scholar = s;
        scholar_name.text = "Scholar #" + scholar.Info.number;

        Refresh();
    }

    public void Refresh()
    {
        if (active)
        {
            stress = (int) scholar.Stress.value;
            //Debug.Log("Стресс чувака - " + stress/10);
            scholar_stress.text = "Stress:" + stress + "%";
            slider.Select(stress);
            Mood();
        }
    }

    private void Mood()
    {
        if (scholar.active)
        {
            switch (scholar.Stress.GetMoodType())
            {
                case GetS.mood.Chill:
                    {
                        emotion.sprite = ScholarFaces.get[GetS.faces.Smile];
                        break;
                    }
                case GetS.mood.Normal:
                    {
                        emotion.sprite = ScholarFaces.get[GetS.faces.Ussual];
                        break;
                    }
                case GetS.mood.Panic:
                    {
                        emotion.sprite = ScholarFaces.get[GetS.faces.Upset];
                        break;
                    }
            }
        }
        else
        {
            emotion.sprite = ScholarFaces.get[GetS.faces.Dead];
        }
    }
}
