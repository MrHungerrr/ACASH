using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Searching;

public class ScholarObjects
{


    private Scholar Scholar { get; }
    ScholarObjectsManager.obj_name object_current;
    Transform objects_holder;
    Transform objects_target;

    private bool holding;


    [HideInInspector]
    public Dictionary<ScholarObjectsManager.obj_name, GameObject> objects = new Dictionary<ScholarObjectsManager.obj_name, GameObject>();



    public ScholarObjects(Scholar Scholar)
    {
        holding = false;
        objects_holder = Scholar.transform.Find("Objects");
        SIC.Component(Scholar.transform, "Arm_Target_R_2", out objects_target);

        for (int i = 0; i < Enum.GetNames(typeof(ScholarObjectsManager.obj_name)).Length; i++)
        {
            ScholarObjectsManager.obj_name name_buf = (ScholarObjectsManager.obj_name)i;
            GameObject object_buf = objects_holder.Find(name_buf.ToString()).gameObject;
            objects.Add(name_buf, object_buf);

            object_buf.SetActive(false);
        }

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


    public void Hold(ScholarObjectsManager.obj_name obj)
    {
        if (!holding)
        {
            object_current = obj;
            holding = true;
            objects[obj].SetActive(true);
        }
    }

    public void ThrowOut()
    {
        if (holding)
        {
            holding = false;
            objects[object_current].SetActive(false);

            GameObject buf = GameObject.Instantiate(ScholarObjectsManager.get.objects[object_current], objects_target.position, objects[object_current].transform.rotation, ScholarObjectsManager.get.object_parent);

            buf.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.value - 0.5f, 1f, UnityEngine.Random.value - 0.5f).normalized * UnityEngine.Random.Range(11, 15));
        }
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
