using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ScholarEars
{
    private Scholar Scholar;


    public ScholarEars(Scholar s)
    {
        Scholar = s;
    }

    public void Hear(float distance)
    {
        if (Scholar.Senses.Teacher.distance <= distance)
        {
            Scholar.Senses.TeacherHere();
        }
    }


    public void SpecialHear(Vector3 pos)
    {
        //Вероятность нужна тут
        Debug.Log("Я услышал");
    }


    private float GetHearDistance(Vector3 goal)
    {
        return BaseGeometry.GetDistance(Scholar.Move.NavAgent, Scholar.Move.Position(), Player.get.Move.Position());
    }
  



}
