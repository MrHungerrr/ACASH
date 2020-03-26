using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;


public class TeacherComputerController: MonoBehaviour, I_Interaction
{
    [HideInInspector]
    public TeacherComputer Computer { get; private set; }


    //Камера
    private CinemachineVirtualCamera Cam;
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
        Computer = GetComponent<TeacherComputer>();
        Transform screen = transform.Find("Screen").Find("UI").Find("Canvas").Find("Screen");

        cursor = screen.Find("Cursor").gameObject;
        pointer = screen.Find("Cursor").Find("Pointer").GetComponent<Image>();
        finger = screen.Find("Cursor").Find("Select").GetComponent<Image>();
        ChangeImage("pointer");

        Cam = transform.Find("Screen").GetComponentInChildren<CinemachineVirtualCamera>();
        normRotation = Cam.transform.rotation;

        zoom = false;
        moving = false;
        zooming = false;

        Enable(false);
    }


    public void Interaction()
    {
        Enable(true);
        Player.get.Action.Doing(false);
    }



    public void Enable(bool option)
    {
        if (option)
        {
            Cam.transform.rotation = normRotation;
            FOV = normFOV;
            Cam.m_Lens.FieldOfView = FOV;
            ComputerManager.get.Set(this);
        }
        else
        {
            moving = false;
            zooming = false;
        }

        active = option;
        Computer.Col.enabled = option;
        Player.get.Camera.Enable(!option);
        Cam.enabled = option;
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

        if (Mathf.Abs(cursor.transform.localPosition.x - 720) >  720)
        {
            cursor.transform.localPosition = new Vector3(720 + Mathf.Sign(cursor.transform.localPosition.x - 720) * 720, cursor.transform.localPosition.y, cursor.transform.localPosition.z);
        }

        if (Mathf.Abs(cursor.transform.localPosition.y + 540) > 540)
        {
            cursor.transform.localPosition = new Vector3(cursor.transform.localPosition.x, -540 + Mathf.Sign(cursor.transform.localPosition.y + 540) * 540, cursor.transform.localPosition.z);
        }
    }

    private void CameraMove(Vector3 movement)
    {

        Cam.transform.Rotate(movement);

        if (Mathf.Abs(Cam.transform.localEulerAngles.y - 180) > y_delta)
        {
            ClampYAxisRotationToValue(180 + Mathf.Sign(Cam.transform.localEulerAngles.y - 180) * y_delta);
        }

        if (Cam.transform.localEulerAngles.x > x_delta && Cam.transform.localEulerAngles.x < (360 - x_delta))
        {
            ClampXAxisRotationToValue((((int)Cam.transform.localEulerAngles.x / 180) * -(x_delta * 2)) + x_delta);
        }

        ClampZAxisRotationToValue(0);
    }



    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = Cam.transform.localEulerAngles;
        eulerRotation.x = value;
        Cam.transform.localEulerAngles = eulerRotation;
    }

    private void ClampYAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = Cam.transform.localEulerAngles;
        eulerRotation.y = value;
        Cam.transform.localEulerAngles = eulerRotation;
    }

    private void ClampZAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = Cam.transform.localEulerAngles;
        eulerRotation.z = value;
        Cam.transform.localEulerAngles = eulerRotation;
    }


    private void CameraMoveToOrigin()
    {    
        Cam.transform.rotation = Quaternion.Slerp(Cam.transform.rotation, normRotation, 4f * Time.deltaTime);

        if (Cam.transform.rotation == normRotation)
        {
            moving = false;
        }
    }

    private void Zoom()
    {
        if (zoom)
        {
            FOV = Mathf.Lerp(Cam.m_Lens.FieldOfView, zoomFOV, 4 * Time.deltaTime);
        }
        else
        {
            FOV = Mathf.Lerp(Cam.m_Lens.FieldOfView, normFOV, 4 * Time.deltaTime);
        }
        Cam.m_Lens.FieldOfView = FOV;


        if ((normFOV - FOV) < 0.01f)
        {
            zooming = false;
        }
    }



    private void MouseCollision()
    {
        string buf_col = Computer.Col.MouseCollision(cursor.transform.localPosition);

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
        Computer.Sound.Play(ComputerSounds.sounds.Click);

        if (collision)
        {
            Computer.ExecuteCommand(mouse_collision);
        }
        else
            Debug.Log("Не чувстсвую коллайдера");
    }
}
