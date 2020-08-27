using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Single;


public class ComputerManager : MonoSingleton<ComputerManager>
{
    private const float MOUSE_SENSITIVITY = 0.001f;
    private const float GAMEPAD_SENSITIVITY = 0.004f;
    private const float MOUSE_CAMERA_SENSITIVITY = 0.2f;
    private const float GAMEPAD_CAMERA_SENSITIVITY = 3f;

    private Vector2 _mouseInput;
    private Vector2 _mouse;


    public TeacherComputerController CompControl { get; private set; }

    private A_Computer[] _computers;
    private bool _active = false;


    public static readonly Color[] colors = new Color[2]
    {
        new Color(0f,0f,0f,0f),
        new Color(1f,1f,1f,0.3f),
    };



    public void SetLevel()
    {
        UserManager.Instance.SetUserManager();

        _computers = GameObject.FindObjectsOfType<A_Computer>();
        foreach(A_Computer comp in _computers)
        {
            comp.Setup();
        }
    }

    public void UnsetLevel()
    {
        Exit();
    }

    public void Set(TeacherComputerController comp)
    {
        CompControl = comp;
        _active = true;
        InputManager.SwitchGameInput(InputManager.GameplayType.Computer);
    }

    public void Disable()
    {
        _active = false;
    }

    private void Update()
    {
        if (_active)
        {
            Move();
        }
    }

    private void Move()
    {
        if (CompControl.zoom)
        {
            switch (InputManager.InputType)
            {
                case InputManager.Input.Keyboard:
                    {
                        _mouseInput *= MOUSE_CAMERA_SENSITIVITY * Time.deltaTime * PlayerSettings.SensetivityCoef;
                        break;
                    }
                default:
                    {
                        _mouseInput *= GAMEPAD_CAMERA_SENSITIVITY * Time.deltaTime * PlayerSettings.SensetivityCoef;
                        break;
                    }
            }

            _mouse = new Vector3(-_mouseInput.y, _mouseInput.x, 0);
        }
        else
        {
            switch (InputManager.InputType)
            {
                case InputManager.Input.Keyboard:
                    {
                        _mouseInput *= MOUSE_SENSITIVITY * Time.deltaTime * PlayerSettings.SensetivityCoef;
                        break;
                    }
                default:
                    {
                        _mouseInput = _mouseInput.normalized * GAMEPAD_SENSITIVITY * Time.deltaTime * PlayerSettings.SensetivityCoef;
                        break;
                    }
            }

            _mouse = new Vector3(_mouseInput.x, _mouseInput.y, 0);
        }


        CompControl.Move(_mouse);
    }


    public void MouseInput(Vector2 input)
    {
        _mouseInput = input;
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
