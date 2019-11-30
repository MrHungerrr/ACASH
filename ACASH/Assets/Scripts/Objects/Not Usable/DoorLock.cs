using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    private bool active;

    private CinemachineVirtualCamera lock_cam;
    private Door door;
    private Transform cam_trans;
    [HideInInspector]
    public bool zoom;
    [HideInInspector]
    public bool zooming;
    private const float y_delta = 10;
    private const float x_delta = 15;
    private Quaternion normRotation;
    private float normFOV = 60;
    private float zoomFOV = 20;
    private float FOV;

    private Transform door_lock;
    private Vector3 door_lock_pos;
    [HideInInspector]
    public bool moving;

    private void Awake()
    {
        cam_trans = transform.parent.Find("Handle Camera");
        lock_cam = cam_trans.GetComponentInChildren<CinemachineVirtualCamera>();
        normRotation = lock_cam.transform.rotation;

        door = transform.parent.parent.GetComponentInChildren<Door>();

        door_lock = transform.parent.parent.Find("Door Lock");
        door_lock_pos = door_lock.position;

        zoom = false;
        zooming = false;

        Enable(false);
    }

    public void Enable(bool option)
    {
        if (option)
        {
            lock_cam.transform.rotation = normRotation;
            FOV = normFOV;
            lock_cam.m_Lens.FieldOfView = FOV;
            DoorLockManager.get.Set(this);
        }
        else
        {
            zooming = false;
        }

        active = option;
        moving = true;
        PlayerCamera.get.Enable(!option);
        lock_cam.enabled = option;
    }

    void Update()
    {
        if (active)
        {
            if (zoom || zooming)
                Zoom();
        }

        if(moving)
        {
            MoveLock();
        }
    }


    private void MoveLock()
    {
        Vector3 goal;

        if(active)
        {
            goal = door_lock_pos + Vector3.up*2;
        }
        else
        {
            goal = door_lock_pos;
        }


        if (Vector3.Distance(door_lock.position, goal) < 0.001f)
        {
            moving = false;
        }
        else
        {
            door_lock.Translate(Vector3.Lerp(door_lock.position, goal, 1f));
        }
    }

    public void Rotate(Vector3 movement)
    {
        cam_trans.Rotate(movement);


        if (cam_trans.localEulerAngles.y > y_delta && cam_trans.localEulerAngles.y < (360 - y_delta))
        {
            ClampXAxisRotationToValue((((int)cam_trans.localEulerAngles.y / 180) * -(y_delta * 2)) + y_delta);
        }

        if (cam_trans.localEulerAngles.z > x_delta && cam_trans.localEulerAngles.z < (360 - x_delta))
        {
            ClampZAxisRotationToValue((((int)cam_trans.localEulerAngles.z / 180) * -(x_delta * 2)) + x_delta);
        }



        ClampXAxisRotationToValue(0);
    }



    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = cam_trans.localEulerAngles;
        eulerRotation.x = value;
        cam_trans.localEulerAngles = eulerRotation;
    }

    private void ClampYAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = cam_trans.localEulerAngles;
        eulerRotation.y = value;
        cam_trans.localEulerAngles = eulerRotation;
    }

    private void ClampZAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = cam_trans.localEulerAngles;
        eulerRotation.z = value;
        cam_trans.localEulerAngles = eulerRotation;
    }



    private void Zoom()
    {
        if (zoom)
        {
            FOV = Mathf.Lerp(lock_cam.m_Lens.FieldOfView, zoomFOV, 4 * Time.deltaTime);
        }
        else
        {
            FOV = Mathf.Lerp(lock_cam.m_Lens.FieldOfView, normFOV, 4 * Time.deltaTime);
        }
        lock_cam.m_Lens.FieldOfView = FOV;


        if ((normFOV - FOV) < 0.01f)
        {
            zooming = false;
        }
    }
}
