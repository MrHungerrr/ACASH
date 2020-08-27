using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Searching;

public class ScholarObjects
{


    private Scholar Scholar { get; }
    ScholarObjectsManager.objectType objectCurrent;
    Transform objectsHolder;
    Transform objectsTarget;

    private bool holding;


    [HideInInspector]
    public Dictionary<ScholarObjectsManager.objectType, GameObject> objects = new Dictionary<ScholarObjectsManager.objectType, GameObject>();



    public ScholarObjects(Scholar Scholar)
    {
        holding = false;
        objectsHolder = Scholar.transform.Find("Objects");
        SIC.Component(Scholar.gameObject, "Arm_Target_R_2", out objectsTarget);

        for (int i = 0; i < Enum.GetNames(typeof(ScholarObjectsManager.objectType)).Length; i++)
        {
            ScholarObjectsManager.objectType name_buf = (ScholarObjectsManager.objectType)i;
            GameObject object_buf = objectsHolder.Find(name_buf.ToString()).gameObject;
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


    public void Hold(ScholarObjectsManager.objectType obj)
    {
        if (!holding)
        {
            objectCurrent = obj;
            holding = true;
            objects[obj].SetActive(true);
        }
    }

    public void ThrowOut()
    {
        if (holding)
        {
            holding = false;
            objects[objectCurrent].SetActive(false);

            GameObject buf = GameObject.Instantiate
                (ScholarObjectsManager.Instance.Objects[objectCurrent],
                objectsTarget.position, objects[objectCurrent].transform.rotation,
                ScholarObjectsManager.Instance.ObjectContainer
                );

            buf.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.value - 0.5f, 1f, UnityEngine.Random.value - 0.5f).normalized * UnityEngine.Random.Range(11, 15));
        }
    }


    private void MoveObject()
    {
        objectsHolder.position = objectsTarget.position;
    }

    private void RotateObject()
    {
        objectsHolder.rotation = objectsTarget.rotation;
    }
}
