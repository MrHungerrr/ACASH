using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Searching;

public class ScholarObjects
{


    private Scholar Scholar { get; }
    Transform objects_holder;
    Transform objects_target;

    private bool holding;





    public ScholarObjects(Scholar Scholar)
    {
        holding = false;

        objects_holder = Scholar.transform.parent.Find("Objects");
        SIC.Component(Scholar.transform.parent, "Arm_Target_R_2", out objects_target);
        objects_holder.gameObject.SetActive(false);

        this.Scholar = Scholar;
    }

    public void Update()
    {
        if (holding)
        {
            MoveObject();
            RotateObject();
        }
    }


    public void Hold()
    {
        holding = true;
        objects_holder.gameObject.SetActive(true);
    }

    public void ThrowOut()
    {
        holding = false;
        objects_holder.gameObject.SetActive(false);
    }


    private void MoveObject()
    {
        objects_holder.position = objects_target.position;
    }

    private void RotateObject()
    {
        objects_holder.rotation = objects_target.rotation;
    }
}
