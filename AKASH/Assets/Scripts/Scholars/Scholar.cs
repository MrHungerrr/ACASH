using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scholar : MonoBehaviour
{
    [HideInInspector]
    public int panic;
    [HideInInspector]
    public bool cheating;
    [HideInInspector]
    public TextBoxScholar textBox;
    [HideInInspector]
    public string key = "scholar_";
    private double rnd;

    public bool[] remarks = new bool[2]; 

    private void Awake()
    {
        textBox = new TextBoxScholar(transform.Find("Text Box").GetComponent<TextMeshProUGUI>());
    }


    public void PanicUp(int value)
    {
        panic += value;
        if (panic > 100)
            panic = 100;
        if (panic < 100)
            panic = 0;
    }

    public bool RandomBool(double a)
    {
        rnd = Random.value;

        if (a >= rnd)
            return true;
        else
            return false;
    }

    public void Continue()
    {

    }

    public void Stop()
    {

    }
}
