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
    private TextMeshProUGUI exam;
    private TextMeshProUGUI introdaction;

    void Awake()
    {
        rep = transform.Find("Reputation").Find("Box").GetComponentInChildren<TextMeshProUGUI>();
        rep_change = transform.Find("Reputation Change").Find("Box").GetComponentInChildren<TextMeshProUGUI>();
        report = transform.Find("Report").Find("Box").GetComponentInChildren<TextMeshProUGUI>();
        timer = transform.Find("Timer").Find("Box").GetComponentInChildren<TextMeshProUGUI>();
        timer_header = transform.Find("Timer").Find("Title").GetComponent<TextMeshProUGUI>();
        exam = transform.Find("Exam").GetComponentInChildren<TextMeshProUGUI>();
        introdaction = transform.Find("Introdaction").GetComponentInChildren<TextMeshProUGUI>();

        Time("00:00");
        TimeHeader("Exam is OVER");
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
        report.text = text;
    }

    public void Time(string time)
    {
        timer.text = time;
    }

    public void TimeHeader(string text)
    {
        timer_header.text = text;
    }

    public void Introdaction(string text)
    {
        introdaction.text = text;
    }



    public void Exam(string text)
    {
        exam.text = text;
    }
}
