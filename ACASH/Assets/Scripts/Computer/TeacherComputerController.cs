using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;
using N_BH;


public class TeacherComputerController: MonoBehaviour
{
    private TeacherComputer Comp;
    private ComputerUIColliderManager CompCol;


    //Камера
    private CinemachineVirtualCamera CompCam;
    [HideInInspector]
    public bool zoom;
    [HideInInspector]
    public bool zooming;
    [HideInInspector]
    public bool moving;
    private const float y_delta = 10;
    private const float x_delta = 15;
    private Quaternion normRotation;
    private float normFOV = 60;
    private float zoomFOV = 20;
    private float FOV;



    [HideInInspector]
    public string mouse_collision;
    [HideInInspector]
    public bool collision;
    private bool active;


    private Vector2 position;

    private GameObject cursor;
    private Image pointer;
    private Image finger;



    public void  SetTeacherComputerController()
    {
        Comp = GetComponent<TeacherComputer>();

        Transform buf = transform.Find("Screen").Find("UI").Find("Canvas");
        Transform screen = buf.Find("Screen");

        cursor = screen.Find("Cursor").gameObject;
        pointer = screen.Find("Cursor").Find("Pointer").GetComponent<Image>();
        finger = screen.Find("Cursor").Find("Select").GetComponent<Image>();
        ChangeImage("pointer");

        CompCol = buf.GetComponentInParent<ComputerUIColliderManager>();
        CompCam = buf.parent.parent.GetComponentInChildren<CinemachineVirtualCamera>();
        normRotation = CompCam.transform.rotation;

        zoom = false;
        moving = false;
        zooming = false;

        Enable(false);
    }


    public void Enable(bool option)
    {
        if (option)
        {
            CompCam.transform.rotation = normRotation;
            FOV = normFOV;
            CompCam.m_Lens.FieldOfView = FOV;
            ComputerManager.get.Set(this);
        }
        else
        {
            moving = false;
            zooming = false;
        }

        active = option;
        CompCol.enabled = option;
        PlayerCamera.get.Enable(!option);
        CompCam.enabled = option;
    }


    private void Update()
    {
        if (active)
        {
            MouseCollision();

            if (zoom || zooming)
                Zoom();
            if (!zoom && moving)
                CameraMoveToOrigin();
        }
    }

    public void Move(Vector3 movement)
    {
        if (!zoom)
        {
            CursorMove(movement);
        }
        else
        {
            CameraMove(movement);
        }
    }

    private void CursorMove(Vector3 movement)
    {
        cursor.transform.Translate(movement);

        if (Mathf.Abs(cursor.transform.localPosition.x) > 720)
        {
            cursor.transform.localPosition = new Vector3(Mathf.Sign(cursor.transform.localPosition.x) * 720, cursor.transform.localPosition.y, cursor.transform.localPosition.z);
        }

        if (Mathf.Abs(cursor.transform.localPosition.y) > 540)
        {
            cursor.transform.localPosition = new Vector3(cursor.transform.localPosition.x, Mathf.Sign(cursor.transform.localPosition.y) * 540, cursor.transform.localPosition.z);
        }
    }

    private void CameraMove(Vector3 movement)
    {

        CompCam.transform.Rotate(movement);

        if (Mathf.Abs(CompCam.transform.localEulerAngles.y - 180) > y_delta)
        {
            ClampYAxisRotationToValue(180 + Mathf.Sign(CompCam.transform.localEulerAngles.y - 180) * y_delta);
        }

        if (CompCam.transform.localEulerAngles.x > x_delta && CompCam.transform.localEulerAngles.x < (360 - x_delta))
        {
            ClampXAxisRotationToValue((((int)CompCam.transform.localEulerAngles.x / 180) * -(x_delta * 2)) + x_delta);
        }

        ClampZAxisRotationToValue(0);
    }



    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = CompCam.transform.localEulerAngles;
        eulerRotation.x = value;
        CompCam.transform.localEulerAngles = eulerRotation;
    }

    private void ClampYAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = CompCam.transform.localEulerAngles;
        eulerRotation.y = value;
        CompCam.transform.localEulerAngles = eulerRotation;
    }

    private void ClampZAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = CompCam.transform.localEulerAngles;
        eulerRotation.z = value;
        CompCam.transform.localEulerAngles = eulerRotation;
    }


    private void CameraMoveToOrigin()
    {    
        CompCam.transform.rotation = Quaternion.Slerp(CompCam.transform.rotation, normRotation, 4f * Time.deltaTime);

        if (CompCam.transform.rotation == normRotation)
        {
            moving = false;
        }
    }

    private void Zoom()
    {
        if (zoom)
        {
            FOV = Mathf.Lerp(CompCam.m_Lens.FieldOfView, zoomFOV, 4 * Time.deltaTime);
        }
        else
        {
            FOV = Mathf.Lerp(CompCam.m_Lens.FieldOfView, normFOV, 4 * Time.deltaTime);
        }
        CompCam.m_Lens.FieldOfView = FOV;


        if ((normFOV - FOV) < 0.01f)
        {
            zooming = false;
        }
    }



    private void MouseCollision()
    {
        string buf_col = CompCol.MouseCollision(cursor.transform.localPosition);

        if (buf_col != null)
        {
            ChangeImage("finger");
            //Debug.Log("Enter " + buf_col);
            collision = true;
        }
        else if (collision)
        {
            ChangeImage("pointer");
            //Debug.Log("Exit " + mouse_collision);
            collision = false;
        }

        mouse_collision = buf_col;
    }

    private void ChangeImage(string option)
    {
        pointer.enabled = false;
        finger.enabled = false;
        switch (option)
        {
            case "pointer":
                {
                    pointer.enabled = true;
                    break;
                }
            case "finger":
                {
                    finger.enabled = true;
                    break;
                }
        }
    }


    public void Select()
    {
        if (collision)
        {
            Comp.select = mouse_collision;
            Comp.Select();
        }
        else
            Debug.Log("Не чувстсвую коллайдера");
    }
}
