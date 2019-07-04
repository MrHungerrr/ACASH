using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scholar : MonoBehaviour
{
    [HideInInspector]
    public int stress;
    [HideInInspector]
    public bool cheating;
    [HideInInspector]
    public TextBoxScholar textBox;
    [HideInInspector]
    public string key = "Scholar_";
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
    
    public bool[] remarks = new bool[2]; 

    private void Awake()
    {
        textBox = new TextBoxScholar(transform.Find("Text Box").GetComponent<TextMeshProUGUI>());
    }


    public void Stress(int value)
    {
        stress += value;
        if (stress > 100)
            stress = 100;
        if (stress < 0)
            stress = 0;
    }

    public bool RandomBool(double a)
    {
        rnd = Random.value;

        if (a >= rnd)
            return true;
        else
            return false;
    }

    public void TestPlus(int value)
    {
        test += value;
    }

    public void Continue()
    {
        
    }

    public void Stop()
    {

    }

    public void StartWrite()
    {

    }
}
