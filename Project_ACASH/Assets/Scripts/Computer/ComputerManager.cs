using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Single;


public class ComputerManager : Singleton<ComputerManager>
{
    private float mouseSensitivity = 0.001f;
    private float gamepadSensitivity = 0.004f;
    private float mouseCameraSensitivity = 0.2f;
    private float gamepadCameraSensitivity = 3f;
    [HideInInspector]
    public Vector2 mouseInput;
    [HideInInspector]
    public Vector2 mouse;

    [HideInInspector]
    public TeacherComputerController CompControl { get; private set; }
    private A_Computer[] computers;
    private bool active = false;
    [HideInInspector]
    public bool fast;

    [HideInInspector]
    public bool end;


    [HideInInspector]
    public static Color[] colors = new Color[2]
    {
        new Color(0f,0f,0f,0f),
        new Color(1f,1f,1f,0.3f),
    };



    public void Setup()
    {
        UserManager.get.SetUserManager();

        computers = GameObject.FindObjectsOfType<A_Computer>();
        foreach(A_Computer comp in computers)
        {
            comp.Setup();
        }
    }

    public void Unsetup()
    {
        Exit();
    }

    public void SetScholars()
    {
        foreach (A_Computer comp in computers)
        {
            comp.SetScholars();
        }
    }

    public void Set(TeacherComputerController comp)
    {
        CompControl = comp;
        active = true;
        InputManager.get.SwitchGameInput("computer");
    }

    public void Disable()
    {
        active = false;
    }

    private void Update()
    {
        if (active)
        {
            Move();
        }
    }

    private void Move()
    {
        if (CompControl.zoom)
        {
            switch (InputManager.get.inputType)
            {
                case "keyboard":
                    {
                        mouseInput *= mouseCameraSensitivity * Time.deltaTime * Player.get.Camera.coefSensitivity;
                        break;
                    }
                default:
                    {
                        mouseInput *= gamepadCameraSensitivity * Time.deltaTime * Player.get.Camera.coefSensitivity;
                        break;
                    }
            }

            mouse = new Vector3(-mouseInput.y, mouseInput.x, 0);
        }
        else
        {
            switch (InputManager.get.inputType)
            {
                case "keyboard":
                    {
                        mouseInput *= mouseSensitivity * Time.deltaTime * Player.get.Camera.coefSensitivity;
                        break;
                    }
                default:
                    {
                        mouseInput = mouseInput.normalized * gamepadSensitivity * Time.deltaTime * Player.get.Camera.coefSensitivity;

                        if (fast)
                            mouseInput *= 3f;
                        break;
                    }
            }

            mouse = new Vector3(mouseInput.x, mouseInput.y, 0);
        }


        CompControl.Move(mouse);
    }

    public void Zoom(bool option)
    {
        if(option)
        {
            CompControl.zoom = true;
            CompControl.zooming = true;
            CompControl.moving = true;
        }
        else
        {
            CompControl.zoom = false;
        }
    }


    public void Select()
    {
        CompControl.Select();
    }

    public void Exit()
    {
        Zoom(false);
        CompControl.Enable(false);
        Disable();
    }







    public void Sensitivity()
    {

    }
}
