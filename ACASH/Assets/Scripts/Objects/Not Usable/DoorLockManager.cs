using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using N_BH;


public class DoorLockManager : Singleton<DoorLockManager>
{
    private float mouseSensitivity = 3f;
    private float gamepadSensitivity = 60f;
    [HideInInspector]
    public Vector2 rotInput;
    [HideInInspector]
    public Vector2 rot;

    private DoorLock DLock;
    private bool active;


    private void Awake()
    {
        active = false;
    }


    public void Set(DoorLock DL)
    {
        if(DLock != null)
            DLock.moving = false;

        DLock = DL;
        active = true;
        InputManager.get.SwitchGameInput("doorlock");
    }

    public void Disable()
    {
        active = false;
    }

    private void Update()
    {
        if (active)
        {
            Input();
        }
    }

    private void Input()
    {

        switch (InputManager.get.inputType)
        {
            case "keyboard":
                {
                    rotInput *= mouseSensitivity * Time.deltaTime;
                    break;
                }
            default:
                {
                    rotInput *= gamepadSensitivity * Time.deltaTime;
                    break;
                }
        }
        rot = new Vector3(0, rotInput.x, -rotInput.y);

        DLock.Rotate(rot);
    }



    public void Zoom(bool option)
    {
        if(option)
        {
            DLock.zoom = true;
            DLock.zooming = true;
        }
        else
        {
            DLock.zoom = false;
        }
    }


    public void Exit()
    {
        Zoom(false);
        DLock.Enable(false);
        Disable();
    }
}
