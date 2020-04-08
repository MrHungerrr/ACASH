using UnityEngine;
using System.Collections;
using System;

public class ScholarExam
{
    private Scholar Scholar;
    public bool writing;
    public int test { get; private set; }
    public int test_procent { get; private set; }
    public double test_buf { get; private set; }
    public float test_bufTime { get; private set; }
    public bool finished { get; private set; }
    public double speed { get; private set; }


    public ScholarExam(Scholar Scholar)
    {
        this.Scholar = Scholar;
        finished = false;
        test = 0;
        test_buf = 0;
        test_bufTime = 0;
    }

    public void Update()
    {
        WritingSpeedCalculate();
        WritingTest();
    }

    public void WritingTest()
    {
        if (test_bufTime > 0)
        {
            test_buf += speed * Time.deltaTime;
            test_bufTime -= Time.deltaTime;
        }
        else
        {
            test += Convert.ToInt32(test_buf);
            test_bufTime = 1f;
            test_buf = 0;

            test_procent = (int) (test / 20000f);

            if (test >= 20000)
            {
                test_procent = 100;
                finished = true;
            }
        }
    }




    private void WritingSpeedCalculate()
    {
        speed = (1 / (0.5 * (Scholar.Stress.value / 100) - 1)) + 2;
        speed *= 100;
    }

}
