using UnityEngine;
using System.Collections;
using Vkimow.Unity.Tools.Single;
using PostProcessing;


public class MenuManager : MonoSingleton<MenuManager>
{
    private bool _isActive;


    private int _moveCD;
    private const int MOVE_KEYBOARD_CD = 12;
    private const int MOVE_GAMEPAD_CD = 12;


    private Vector2 _moveInput;



    public void Setup()
    {
        Enable(false);
    }


    public void Enable(bool option)
    {
        _isActive = option;

        if (option)
        {
            Time.timeScale = 0;
            InputType();
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Enter()
    {

    }

    public void Escape()
    {
        InputManager.MenuResume();
    }


    public void MoveInput(Vector2 input)
    {
        _moveInput = input;
    }


    public void MoveCD()
    {
        switch (InputManager.InputType)
        {
            case InputManager.Input.Keyboard:
                {
                    _moveCD = MOVE_KEYBOARD_CD;
                    break;
                }
            default:
                {
                    _moveCD = MOVE_GAMEPAD_CD;
                    break;
                }
        }
    }



    public void InputType()
    {
        if (_isActive)
        {
            switch (InputManager.InputType)
            {
                case InputManager.Input.Playstation:
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        break;
                    }
                case InputManager.Input.Xbox:
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        break;
                    }
                default:
                    {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        break;
                    }
            }
        }
    }

}
