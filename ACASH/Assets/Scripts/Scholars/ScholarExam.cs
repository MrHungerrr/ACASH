using UnityEngine;
using System.Collections;
using System;

public class ScholarExam
{

    public bool writing;
    public int test { get; private set; }
    public double test_buf { get; private set; }
    public float test_bufTime { get; private set; }
    public bool finished { get; private set; }


    public ScholarExam()
    {
        finished = false;
        test = 0;
        test_buf = 0;
        test_bufTime = 0;
    }

    public void Update()
    {
        if (writing)
            WritingTest(10);
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
