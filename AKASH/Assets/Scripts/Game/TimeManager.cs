using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;

public class TimeManager : Singleton<TimeManager>
{

    private bool active;
    [HideInInspector]
    public float time_passed;
    [HideInInspector]
    public float time_left;
    [HideInInspector]
    public float time_test;
    private Timer[] timers;


    private void Awake()
    {
        active = false;
        time_test = 60 * 10;
        time_left = 0;
        time_passed = time_test;

        timers = FindObjectsOfType<Timer>();
    }

    private void Update()
    {
        if(active)
        {
            TestTime();
        }
    }

    public void Enable()
    {
        time_left = time_test;
        time_passed = 0;
        active = true;

        foreach(Timer t in timers)
        {
            t.Enable();
        }
    }

    private void TestTime()
    {
        time_passed += Time.deltaTime;
        time_left = time_test - time_passed;
    }
}
