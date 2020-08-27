using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class StressCell : MonoBehaviour
{


    private bool active = false;
    private Scholar scholar;
    private Image emotion;
    private TextMeshProUGUI scholar_name;
    private TextMeshProUGUI scholar_test;
    private TextMeshProUGUI scholar_stress;
    private SliderWatch slider;
    private int stress;
    private int test;

    private void Awake()
    {
        slider = transform.GetComponentInChildren<SliderWatch>();
        slider.Setup();
        emotion = transform.Find("Emotion").Find("Face").GetComponent<Image>();
        scholar_name = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        scholar_test = transform.Find("Test").GetComponent<TextMeshProUGUI>();
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
            test = 0;
            stress = 0;
            //Debug.Log("Стресс чувака - " + stress/10);
            scholar_test.text = "Test:" + test + '%';
            scholar_stress.text = "Stress:" + stress + '%';
            slider.Select(stress);
        }
    }
}
