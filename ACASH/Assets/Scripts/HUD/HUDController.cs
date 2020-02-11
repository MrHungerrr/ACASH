using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Single;

public class HUDController : Singleton<HUDController>
{

    private TextMeshProUGUI rep;
    private TextMeshProUGUI rep_change;
    private TextMeshProUGUI report;
    private TextMeshProUGUI timer;
    private TextMeshProUGUI timer_header;
    private TextMeshProUGUI[] specials = new TextMeshProUGUI[2];
    private GameObject[] marks = new GameObject[2];

    void Awake()
    {
        rep = transform.Find("Reputation").Find("ReputationBox").GetComponentInChildren<TextMeshProUGUI>();
        rep_change = transform.Find("Reputation Change").Find("ReputationBox").GetComponentInChildren<TextMeshProUGUI>();
        report = transform.Find("Report").Find("ReportBox").GetComponentInChildren<TextMeshProUGUI>();
        timer = transform.Find("Timer").Find("TimeBox").GetComponentInChildren<TextMeshProUGUI>();
        timer_header = transform.Find("Timer").Find("Title").GetComponent<TextMeshProUGUI>();
        specials[0] = transform.Find("Specials").Find("SpecialsBox").Find("Tasks").Find("Special_0").GetComponent<TextMeshProUGUI>();
        specials[1] = transform.Find("Specials").Find("SpecialsBox").Find("Tasks").Find("Special_1").GetComponent<TextMeshProUGUI>();
        marks[0] = transform.Find("Specials").Find("SpecialsBox").Find("Marks").Find("Special_0").Find("Mark").gameObject;
        marks[1] = transform.Find("Specials").Find("SpecialsBox").Find("Marks").Find("Special_1").Find("Mark").gameObject;
    }


    public void Reputation(int value)
    {
        rep.text = value.ToString();
    }

    public void ReputationChange(int value)
    {
        if (value > 0)
            rep_change.text = "+" + value.ToString();
        else
            rep_change.text = value.ToString();
    }

    public void Report(string text)
    {
        report.text = report.text;
    }

    public void Time(string time)
    {
        timer.text = time;
    }

    public void TimeHeader(string text)
    {
        timer_header.text = text;
    }

    public void SetSpecials(string special_0, string special_1)
    {
        specials[0].text = special_0;
        specials[1].text = special_1;
        marks[0].SetActive(false);
        marks[1].SetActive(false);
    }

    public void SpecialComplete(int special_num)
    {
        marks[special_num].SetActive(true);
    }
}
