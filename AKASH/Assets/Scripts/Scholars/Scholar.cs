using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scholar : MonoBehaviour
{
    [HideInInspector]
    public int stress;
    [HideInInspector]
    public bool cheating;
    [HideInInspector]
    public TextBoxScholar TextBox;
    [HideInInspector]
    public ActionsScholar Action;
    [HideInInspector]
    public Emotions Emotions;
    [HideInInspector]
    public string keyWord;
    private double rnd;
    [HideInInspector]
    public byte behaviourType;
    [HideInInspector]
    public int IQ_start;
    [HideInInspector]
    public int test;
    [HideInInspector]
    public double test_buf;
    [HideInInspector]
    public float test_bufTime;
    [HideInInspector]
    public string view = "Nothing_";
    [HideInInspector]
    public bool writing;
    [HideInInspector]
    public Dictionary<string, bool> remarks = new Dictionary<string, bool>()
    {
        { "Pen_", false },
        { "Calculator_", false },
        { "Talking_", false },
        { "Cheating_", false },
        { "Walking_", false },
        { "Nothing_", false }
    };


    public void Continue()
    {
        writing = true;
    }

    public void Stop()
    {
        writing = false;
    }

    public void StartWrite()
    {
        writing = true;
    }

    public void Stress(int value)
    {
        stress += value;
        if (stress > 100)
            stress = 100;
        if (stress < 0)
            stress = 0;
    }

    public bool Probability(double a)
    {
        rnd = UnityEngine.Random.value;

        if (a >= rnd)
            return true;
        else
            return false;
    }

    public void WritingTest(float value)
    {
        //Debug.Log("Пишу тест");
        if (test_bufTime > 0)
        {
            test_buf += value * Time.deltaTime;
            test_bufTime -= Time.deltaTime;
        }
        else
        {
            test += Convert.ToInt32(test_buf);
            test_bufTime = 1f;
            test_buf = 0;
        }
    }

}
