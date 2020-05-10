using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ScholarEyes
{
    private Scholar Scholar;


    private const float peripheral_vision_angle = 140f;
    private const float central_vision_angle = 30f;
    private const float vision_distance = 5f;


    public ScholarEyes(Scholar s)
    {
        Scholar = s;
    }


    public void Update()
    {
        LookingForTeacher();
    }






    private void LookingForTeacher()
    {
        //Debug.Log("T Behind Wall - " + T_behind_wall);

        if (!Scholar.Senses.Teacher.behind_wall)
        {
            if(Scholar.Senses.T_here)
            {
                AmISeeTeacher(peripheral_vision_angle);
            }
            else
            {
                AmISeeTeacher(central_vision_angle);
            }
        }
    }
  

    private void AmISeeTeacher(float vision_angle)
    {
        if (Scholar.Senses.Teacher.angle_to <= (vision_angle * 0.5f))
        {
            Scholar.Senses.TeacherHere();
        }
    }


}
