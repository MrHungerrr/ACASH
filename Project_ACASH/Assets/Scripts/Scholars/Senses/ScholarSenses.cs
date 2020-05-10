using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ScholarSenses
{
    private Scholar Scholar;
    private ScholarEyes Eyes;
    public ScholarEars Ears { get;}
    public ScholarTeacherCalculate Teacher { get; }  // Пятое чувство  (Жопой чувствую)


    //Main Bools
    public bool T_here { get; private set; }
    public bool T_look_at_us { get; private set; }
    public bool T_look_near_at_us { get; private set; }


    //Look Timers
    public float T_look_time { get; private set; }
    public float T_look_near_time { get; private set; }


    //Vanish Time
    private float T_look_vanish_time;
    private const float T_look_vanish_time_const = 0.2f;

    private float T_vanish_time;
    private const float T_vanish_time_const = 4f;



    public ScholarSenses(Scholar Scholar)
    {
        this.Scholar = Scholar;

        Eyes = new ScholarEyes(Scholar);
        Ears = new ScholarEars(Scholar);
        Teacher = new ScholarTeacherCalculate(Scholar);
    }


    public void Update()
    {
        Teacher.Update();
        Eyes.Update();
        Vanish();
        LookTimers();
    }




    public void TeacherHere()
    {
        T_here = true;
        T_vanish_time = T_vanish_time_const;
    }

    public void TeacherLookAtUs()
    {
        T_look_at_us = true;
        T_look_vanish_time = T_look_vanish_time_const;

        Scholar.HUD.Enable();
    }


    public void TeacherLookNearAtUs(bool option)
    {
        T_look_near_at_us = option;
    }





    private void LookTimers()
    {
        if (T_look_at_us)
        {
            T_look_time += Time.deltaTime * Teacher.look_coef;
        }
        else
        {
            T_look_time = 0;
        }


        if (T_look_near_at_us)
        {
            T_look_near_time += Time.deltaTime * Teacher.look_coef;
        }
        else
        {
            T_look_near_time = 0;
        }
    }



    private void Vanish()
    {
        TeacherVanish();
        TeacherLookVanish();
    }


    private void TeacherVanish()
    {
        if (Teacher.distance > 0.4f)
        {
            if (T_vanish_time > 0)
            {
                T_vanish_time -= Time.deltaTime;
            }
            else
            {
                T_here = false;
            }
        }
    }

    private void TeacherLookVanish()
    {
        if (T_look_vanish_time > 0)
        {
            T_look_vanish_time -= Time.deltaTime;
        }
        else
        {
            T_look_at_us = false;
        }
    }


}
