using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using N_BH;

public class ExecuteHUDController : Singleton<ExecuteHUDController>
{
    [HideInInspector]
    public bool active;

    [HideInInspector]
    public float moveInput;
    [HideInInspector]
    public int move_cd;
    private const int move_const_keyboard_cd = 12;
    private const int move_const_gamepad_cd = 12;

    private string[] reasons = new string[]
    {
        "Walking",
        "Cheating",
        "Talking"
    };

    private ExecuteHUDSelect[] topics = new ExecuteHUDSelect[3];

    private int selected;

    private void Awake()
    {
        Transform buf = transform.GetChild(0);
        selected = 0;

        for (int i = 0; i < reasons.Length; i++)
        {
            topics[i] = buf.Find(reasons[i]).GetComponentInChildren<ExecuteHUDSelect>();
            topics[i].number = i;
        }

        Select(1);
    }


    private void Update()
    {
        if(active)
        {
            Move();
        }
    }


    public void Select(int number)
    {

        selected = number;

        for (int i = 0; i < topics.Length; i++)
        {
            if (active)
            {
                if (selected != i)
                {
                    topics[i].Select(false);
                }
                else
                {
                    topics[number].Select(true);
                }
            }
            else
            {
                topics[i].Disable();
            }
        }
    }

    public void Accept()
    {
        Player.get.Execute(reasons[selected]);
        InputManager.get.ExecuteBack();
    }


    private void Move()
    {
        if (Mathf.Abs(moveInput) > 0.75)
        {
            if (move_cd <= 0)
            {

                if (moveInput > 0)
                {
                    int buf = selected + 1;

                    if (buf >= reasons.Length)
                        buf = 2;

                    Select(buf);
                }
                else
                {
                    int buf = selected - 1;

                    if (buf < 0)
                        buf = 0;

                    Select(buf);
                }

                MoveCD();

            }
        }
        else if (moveInput == 0)
        {
            move_cd = 0;
        }

        if (move_cd > 0)
        {
            move_cd--;
        }
    }


    public void MoveCD()
    {
        switch (InputManager.get.inputType)
        {
            case "keyboard":
                {
                    move_cd = move_const_keyboard_cd;
                    break;
                }
            default:
                {
                    move_cd = move_const_gamepad_cd;
                    break;
                }
        }
    }


    public void InputType()
    {
        if (active)
        {
            switch (InputManager.get.inputType)
            {
                case "playstation":
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        break;
                    }
                case "xbox":
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
