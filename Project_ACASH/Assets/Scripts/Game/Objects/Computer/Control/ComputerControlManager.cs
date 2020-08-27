using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;
using Single;

public class ComputerControlManager: Singleton<ComputerControlManager>
{
    private const float MOUSE_SENSITIVITY = 0.001f;
    private const float GAMEPAD_SENSITIVITY = 0.004f;
    private const float MOUSE_CAMERA_SENSITIVITY = 0.2f;
    private const float GAMEPAD_CAMERA_SENSITIVITY = 3f;


    private bool _active = false;
    private ComputerControl _computerControl;

    private Vector2 _mouseInput;
    private Vector2 _mouse;


    public void UnsetLevel()
    {
        Exit();
    }

    public void Set(ComputerControl comp)
    {
        _computerControl = comp;
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
            _computerControl.MyUpdate();
        }
    }

    private void Move()
    {
        if (_computerControl.Camera.Control)
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
            _computerControl.Camera.Move(_mouse);
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
            _computerControl.Cursor.Move(_mouse);
        }
    }


    public void MouseInput(Vector2 input)
    {
        _mouseInput = input;
    }

    public void Zoom(bool option)
    {
        _computerControl.Camera.SetZoom(option);
    }


    public void Select()
    {
        _computerControl.Select();
    }

    public void Exit()
    {
        Zoom(false);
        _computerControl.Enable(false);
        Disable();
    }
}
