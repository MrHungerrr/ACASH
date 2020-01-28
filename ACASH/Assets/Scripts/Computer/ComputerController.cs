using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;
using N_BH;


public class ComputerController : MonoBehaviour
{
    private ComputerWindows CompWindows;
    private ComputerUIColliderManager comp_col;


    //Камера
    private CinemachineVirtualCamera comp_cam;
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


        

    private string mouse_collision;
    private bool collision;
    private bool active;


    private Vector2 position;

    private GameObject screen;
    private GameObject cursor;
    private Image pointer;
    private Image select;


    public void SetComputerController()
    {
        Transform buf = transform.Find("Computer_UI").Find("Canvas");
        screen = buf.Find("Screen").gameObject;
        cursor = screen.transform.Find("Cursor").gameObject;
        pointer = screen.transform.Find("Cursor").Find("Pointer").GetComponent<Image>();
        select = screen.transform.Find("Cursor").Find("Select").GetComponent<Image>();
        ChangeImage("pointer");

        CompWindows = buf.Find("Computer Agent").GetComponent<ComputerWindows>();
        CompWindows.SetProgramBar(screen.transform.Find("Close Bar").gameObject);
        CompWindows.SetWindows();

        comp_col = GetComponent<ComputerUIColliderManager>();
        comp_cam = transform.GetComponentInChildren<CinemachineVirtualCamera>();
        normRotation = comp_cam.transform.rotation;

        zoom = false;
        moving = false;
        zooming = false;

        Enable(false);
    }


    public void Enable(bool option)
    {
        if (option)
        {
            comp_cam.transform.rotation = normRotation;
            FOV = normFOV;
            comp_cam.m_Lens.FieldOfView = FOV;
            ComputerManager.get.Set(this);
        }
        else
        {
            moving = false;
            zooming = false;
        }

        active = option;
        comp_col.enabled = option;
        PlayerCamera.get.Enable(!option);
        comp_cam.enabled = option;
    }


    private void Update()
    {
        if (active)
        {
            MouseCollision();

            if (zoom || zooming)
                Zoom();
            if (!zoom && moving)
                CameraMove();
        }
    }

    public void Move(Vector3 movement)
    {
        if (!zoom)
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
        else
        {

            comp_cam.transform.Rotate(movement);

            if (Mathf.Abs(comp_cam.transform.localEulerAngles.y - 180) > y_delta)
            {
                ClampYAxisRotationToValue(180 + Mathf.Sign(comp_cam.transform.localEulerAngles.y - 180)*y_delta);
            }

            if (comp_cam.transform.localEulerAngles.x > x_delta && comp_cam.transform.localEulerAngles.x < (360 - x_delta) )
            {
                ClampXAxisRotationToValue((((int)comp_cam.transform.localEulerAngles.x / 180) * -(x_delta*2)) + x_delta);
            }



            ClampZAxisRotationToValue(0);
        }
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = comp_cam.transform.localEulerAngles;
        eulerRotation.x = value;
        comp_cam.transform.localEulerAngles = eulerRotation;
    }

    private void ClampYAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = comp_cam.transform.localEulerAngles;
        eulerRotation.y = value;
        comp_cam.transform.localEulerAngles = eulerRotation;
    }

    private void ClampZAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = comp_cam.transform.localEulerAngles;
        eulerRotation.z = value;
        comp_cam.transform.localEulerAngles = eulerRotation;
    }


    private void CameraMove()
    {
        
        comp_cam.transform.rotation = Quaternion.Slerp(comp_cam.transform.rotation, normRotation, 4f * Time.deltaTime);

        if (comp_cam.transform.rotation == normRotation)
        {
            moving = false;
        }
        
    }

    private void Zoom()
    {
        if (zoom)
        {
            FOV = Mathf.Lerp(comp_cam.m_Lens.FieldOfView, zoomFOV, 4 * Time.deltaTime);
        }
        else
        {
            FOV = Mathf.Lerp(comp_cam.m_Lens.FieldOfView, normFOV, 4 * Time.deltaTime);
        }
        comp_cam.m_Lens.FieldOfView = FOV;


        if ((normFOV - FOV) < 0.01f)
        {
            zooming = false;
        }
    }



    private void MouseCollision()
    {


        string buf_col = comp_col.MouseCollision(cursor.transform.localPosition);

        if (buf_col != null)
        {
            ChangeImage("select");
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
        select.enabled = false;
        switch (option)
        {
            case "pointer":
                {
                    pointer.enabled = true;
                    break;
                }
            case "select":
                {
                    select.enabled = true;
                    break;
                }
        }
    }

    public void Select()
    {
        if(collision)
            Debug.Log(mouse_collision);
        else
            Debug.Log("Не чувстсвую коллайдера");
       
        CompWindows.Enter(mouse_collision);
    }


}
