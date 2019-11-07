using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;
using TMPro;
using N_BH;


public class ComputerController : MonoBehaviour
{
    private ComputerAgent CompAgent;
    private ComputerUIColliderManager comp_col;


    //Камера
    private CinemachineVirtualCamera comp_cam;
    [HideInInspector]
    public bool zoom;
    [HideInInspector]
    public bool zooming;
    [HideInInspector]
    public bool moving;
    private Quaternion normRotation;
    private float normFOV = 60;
    private float zoomFOV = 20;
    private float FOV;
        

    private string mouse_collision;
    private bool collision;
    private bool active;


    private Vector2 position;


    public int desktop_num;
    [HideInInspector]
    public string desktop;
    [HideInInspector]
    public string current_window;
    private TextMeshProUGUI program_name;
    private GameObject close_bar;
    private GameObject screen;
    private GameObject cursor;
    private Image pointer;
    private Image select;


    private void Awake()
    {
        Transform buf = transform.Find("Computer_UI").Find("Canvas");
        screen = buf.Find("Screen").gameObject;
        close_bar = screen.transform.Find("Close Bar").gameObject;
        cursor = screen.transform.Find("Cursor").gameObject;
        pointer = screen.transform.Find("Cursor").Find("Pointer").GetComponent<Image>();
        select = screen.transform.Find("Cursor").Find("Select").GetComponent<Image>();
        program_name = close_bar.transform.GetComponentInChildren<TextMeshProUGUI>();
        ChangeImage("pointer");

        desktop = "Desktop_" + desktop_num;
        CompAgent = buf.Find("Computer Agent").GetComponent<ComputerAgent>();
        CompAgent.CompControl = this;
        CompAgent.Set(desktop);
        CloseProgram();

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

            if (Mathf.Abs(comp_cam.transform.localEulerAngles.y - 180) > 10)
            {
                ClampYAxisRotationToValue(180 + Mathf.Sign(comp_cam.transform.localEulerAngles.y - 180)*10);
            }

            if (comp_cam.transform.localEulerAngles.x > 15 && comp_cam.transform.localEulerAngles.x < 345 )
            {
                ClampXAxisRotationToValue((((int)comp_cam.transform.localEulerAngles.x / 180) * -30) + 15);
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
        
        comp_cam.transform.rotation = Quaternion.Slerp(comp_cam.transform.rotation, normRotation, 0.03f);

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

    public void SetProgram(string n)
    {
        close_bar.SetActive(true);
        program_name.text = n;
    }

    public void CloseProgram()
    {
        close_bar.SetActive(false);
        program_name.text = null;
    }

    public void Select()
    {
        if(collision)
            Debug.Log(mouse_collision);
        else
            Debug.Log("Не чувстсвую коллайдера");
       
        CompAgent.Enter(mouse_collision);
    }


}
