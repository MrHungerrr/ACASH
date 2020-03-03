using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ScholarSenses
{
    private Scholar Scholar;

    //T - это Teacher
    private float T_angle_x;
    private float T_angle_y;
    private Vector3 T_direction;
    private float T_distance;
    private float T_look_time;
    private bool T_behind_wall;
    private float T_look_near_time;
    private float T_look_vanish_time;
    private const float T_look_vanish_time_const = 0.1f;
    public bool T_look_at_us;
    public bool T_look_near_at_us;
    private int T_look_coef;
    private float angle_to_teacher;
    public bool T_here { get; set; }
    public bool T_in_sight;
    private float T_vanish_time;
    private const float T_vanish_time_const = 4f;


    private const float peripheral_vision_angle = 140f;
    private const float central_vision_angle = 30f;
    private const float vision_distance = 5f;
    private LayerMask visible_layerMask = LayerMask.GetMask("Wall");


    public ScholarSenses(Scholar s)
    {
        Scholar = s;
    }


    public void Update()
    {
        TeacherCalculate();
        WhereTeacher();
    }


    public void ISeeYou()
    {
        if (T_here)
        {
            T_look_at_us = true;
            T_look_vanish_time = T_look_vanish_time_const;
        }
    }


    public void Hear(float distance)
    {
        if (T_distance <= distance)
        {
            T_here = true;
            T_vanish_time = T_vanish_time_const;
        }
    }


    public void SpecialHear(Vector3 pos)
    {
        //Вероятность нужна тут
        Debug.Log("Я услышал");
    }


    public float GetHearDistance(Vector3 goal)
    {
        NavMeshPath path = new NavMeshPath();

        Scholar.Move.NavAgent.CalculatePath(goal, path);

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = Scholar.transform.position;
        allWayPoints[allWayPoints.Length - 1] = goal;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float buf = 0f;

        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            buf += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return buf;
    }





    private void TeacherCalculate()
    {
        if (Player.get.Camera.zoom)
            T_look_coef = 2;
        else
            T_look_coef = 1;

        T_behind_wall = true;

        T_angle_y = BaseGeometry.LookingAngle(Player.get.transform, Scholar.Move.transform.position);
        T_angle_x = (Player.get.Camera.transform.rotation.eulerAngles.x + 30) % 360;

        T_direction = new Vector3(Player.get.transform.position.x - Scholar.Move.transform.position.x, 0, Player.get.transform.position.z - Scholar.Move.transform.position.z).normalized;

        T_distance = GetHearDistance(Player.get.transform.position);


        RaycastHit hit;
        Debug.DrawRay(Scholar.Move.transform.position + Scholar.transform.up.normalized * 0.3f, T_direction, Color.red);
        if (Physics.Raycast(Scholar.Move.transform.position + Scholar.transform.up.normalized * 0.3f, T_direction, out hit, vision_distance, visible_layerMask))
        {
            if (hit.collider.tag == "Player")
            {
                T_behind_wall = false;
            }
        }


        angle_to_teacher = BaseGeometry.LookingAngle(Scholar.Move.transform, Player.get.transform.position);

        //Debug.Log("Angle to Teacher: " + angle_to_teacher);


        if ((T_angle_y < (48 / (T_look_coef * T_look_coef)) && T_angle_x < 80) || (T_distance <= 0.5))
        {
            T_look_near_at_us = true;
        }
        else
        {
            T_look_near_at_us = false;
        }


        if (T_look_at_us)
        {
            T_look_time += Time.deltaTime * T_look_coef;
        }
        else
        {
            T_look_time = 0;
        }

        if (T_look_near_at_us)
        {
            T_look_near_time += Time.deltaTime * T_look_coef;
        }
        else
        {
            T_look_near_time = 0;
        }


        if (T_look_vanish_time > 0)
        {
            T_look_vanish_time -= Time.deltaTime;
        }
        else
        {
            T_look_at_us = false;
        }
    }


    private void WhereTeacher()
    {
        //Debug.Log("T Behind Wall - " + T_behind_wall);

        if (!T_behind_wall)
        {
            if (T_distance > 0.5)
            {
                if ((angle_to_teacher <= peripheral_vision_angle * 0.5f && T_here) || (angle_to_teacher <= central_vision_angle * 0.5f && !T_here))
                {
                    T_in_sight = true;
                }
                else
                {
                    T_in_sight = false;
                }
            }
            else
            {
                T_in_sight = true;
            }
        }
        else
        {
            T_in_sight = false;
        }



        if (T_in_sight)
        {
            T_here = true;

            T_vanish_time = T_vanish_time_const;
        }
        else
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

        //Debug.Log("T In Sight - " + T_in_sight);
        // Debug.Log("T Here - " + T_here);
    }

  



}
