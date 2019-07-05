using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using TMPro;

public class Dumb : Scholar
{


    void Awake()
    {
        TextBox = transform.Find("Text/Text Box").GetComponent<TextBoxScholar>();
        Action = GetComponent<ActionsScholar>();
        this.tag = "Dumb";
        keyWord += this.tag + "_";
        IQ_start = 0;
    }



    void Update()
    {
        if(writing)
            WritingTest(UnityEngine.Random.value * 100);
    }

    public void Bulling(string bullType, bool strong)
    {
        Debug.Log("Teacher is bulling");
        if(strong)
        {
            Stress(10);
            Stop();
            TextBox.Say(keyWord + bullType);
            if (Probability(0.5))
                Continue();
            else
                StartWrite();
        }
        else
        {
            Stop();
            TextBox.Say(keyWord + bullType);
            if (Probability(0.5))
                Continue();
            else
                StartWrite();
        }
    }






}
