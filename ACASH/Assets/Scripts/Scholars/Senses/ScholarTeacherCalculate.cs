using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ScholarTeacherCalculate
{
    private Scholar Scholar;


    //Teacher Looking
    public int look_coef { get; private set; }
    private float looking_angle_x;
    private float looking_angle_y;


    //Scholar Looking
    public float angle_to { get; private set; }


    //Behind Wall
    private LayerMask visible_layerMask = LayerMask.GetMask("Wall", "Teacher");
    public bool behind_wall { get; private set; }


    //Other
    public float distance { get; private set; }
    private Vector3 direction;





    public ScholarTeacherCalculate(Scholar s)
    {
        Scholar = s;
    }


    public void Update()
    {
        TeacherCalculate();
        TeacherNearLook();
    }




    private void TeacherCalculate()
    {
        IsTeacherBehindWall();

        if (Player.get.Camera.zoom)
            look_coef = 2;
        else
            look_coef = 1;

        looking_angle_y = BaseGeometry.LookingAngle2D(Player.get.transform, Scholar.Move.transform.position);
        looking_angle_x = (Player.get.Camera.transform.rotation.eulerAngles.x + 30) % 360;

        distance = Vector3.Distance(Scholar.Move.Position(), Player.get.Move.Position());
        angle_to = BaseGeometry.LookingAngle2D(Scholar.Move.transform, Player.get.Move.Position());

        //Debug.Log("Distance to Teacher: " + distance);
        //Debug.Log("Angle to Teacher: " + angle_to);
    }



    private void IsTeacherBehindWall()
    {
        direction = BaseGeometry.GetDirection2D(Scholar.Move.Position(), Player.get.Move.Position());

        RaycastHit hit;
        Debug.DrawRay(Scholar.Move.Position() + Scholar.Move.transform.up.normalized * 0.3f, direction, Color.red);
        if (Physics.Raycast(Scholar.Move.Position() + Scholar.Move.transform.up.normalized * 0.3f, direction, out hit, visible_layerMask))
        {
            if (hit.collider.tag == "Player")
            {
                behind_wall = false;
            }
            else
            {
                behind_wall = true;
            }
        }
        else
        {
            behind_wall = true;
        }
    }

  

    private void TeacherNearLook()
    {
        if (!behind_wall)
        {
            if ((looking_angle_y < (48 / (look_coef * look_coef)) && looking_angle_x < (80 / look_coef)))
            {
                Scholar.Senses.TeacherLookNearAtUs(true);
            }
            else
            {
                Scholar.Senses.TeacherLookNearAtUs(false);
            }
        }
        else
        {
            Scholar.Senses.TeacherLookNearAtUs(false);
        }
    }


}
