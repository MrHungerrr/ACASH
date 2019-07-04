using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dumb : Scholar
{

    void Awake()
    {
        this.tag = "Dumb";
        key += this.tag + "_";
        IQ_start = 0;
    }


    void Update()
    {
        WritingTest();
    }

    public void Bulling(string bullType, bool strong)
    {
        if(strong)
        {
            Stress(10);
            Stop();
            textBox.Say(key + bullType);
            if (RandomBool(0.5))
                Continue();
            else
                StartWrite();
        }
        else
        {
            Stop();
            textBox.Say(key + bullType);
            if (RandomBool(0.5))
                Continue();
            else
                StartWrite();
        }
    }

    private void WritingTest()
    {
        if(test_bufTime>0)
        {
            test_buf += UnityEngine.Random.value * Time.deltaTime;
            test_bufTime -= Time.deltaTime;
        }
        else
        {
            TestPlus(Convert.ToInt32(test_buf));
            test_bufTime = 1f;
            test_buf = 0;
        }
    }
}
