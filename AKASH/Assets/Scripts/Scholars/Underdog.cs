using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underdog : Scholar
{

    void Awake()
    {
        TextBox = transform.Find("Text/Text Box").GetComponent<TextBoxScholar>();
        this.tag = "Underdog";
        keyWord += this.tag + "_";
    }


    void Update()
    {
        
    }

    public void Bulling(string bullType, bool strong)
    {
        if (strong)
        {
            Stress(10);
            Stop();
            TextBox.Say(keyWord + bullType);
            Continue();
        }
        else
        {
            Stop();
            TextBox.Say(keyWord + bullType);
            Continue();
        }
    }
}
