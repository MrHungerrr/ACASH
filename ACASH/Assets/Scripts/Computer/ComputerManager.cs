using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using N_BH;


public class ComputerManager : Singleton<ComputerManager>
{
    private float mouseSensitivity = 0.02f;
    private float gamepadSensitivity = 0.07f;
    private float mouseCameraSensitivity = 3f;
    private float gamepadCameraSensitivity = 60f;
    [HideInInspector]
    public Vector2 mouseInput;
    [HideInInspector]
    public Vector2 mouse;

    private ComputerController CompControl;
    private bool active = false;
    [HideInInspector]
    public bool fast;

    [HideInInspector]
    public string current_window;


    [HideInInspector]
    public Color[] colors = new Color[2]
    {
        new Color(0f,0f,0f,0f),
        new Color(1f,1f,1f,0.3f),
    };



    public void SetComputerManager()
    {
        Debug.Log("Setup'им ComputerManager");
        var buf = GameObject.FindObjectsOfType<ComputerController>();
        foreach(ComputerController cc in buf)
        {
            cc.SetComputerController();
        }
    }

    public void Set(ComputerController comp)
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
                        mouseInput *= mouseCameraSensitivity * Time.deltaTime;
                        break;
                    }
                default:
                    {
                        mouseInput *= gamepadCameraSensitivity * Time.deltaTime;
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
                        mouseInput *= mouseSensitivity * Time.deltaTime;
                        break;
                    }
                default:
                    {
                        mouseInput = mouseInput.normalized * gamepadSensitivity * Time.deltaTime;

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



}
