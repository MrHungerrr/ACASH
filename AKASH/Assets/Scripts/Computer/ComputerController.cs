using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using N_BH;


public class ComputerController : Singleton<ComputerController>
{
    private float mouseSensitivity = 90;
    private float gamepadSensitivity = 500;
    [HideInInspector]
    public Vector2 mouseInput;
    [HideInInspector]
    public Vector2 mouse;
    private string mouse_collision;
    private bool collision;
    private bool active;
    [HideInInspector]
    public bool fast;


    private Vector2 position;


    [HideInInspector]
    public int desktop_num;
    [HideInInspector]
    public string desktop;
    [HideInInspector]
    public string current_window;
    private GameObject close_bar;
    private TextMeshProUGUI program_name;
    private GameObject black_bar;
    private GameObject screen;
    private GameObject cursor;
    private Image pointer;
    private Image select;


    private void Awake()
    {
        black_bar = transform.Find("Canvas").Find("Black Bar").gameObject;
        screen = transform.Find("Canvas").Find("Screen").gameObject;
        close_bar = screen.transform.Find("Close Bar").gameObject;
        cursor = screen.transform.Find("Cursor").gameObject;
        pointer = screen.transform.Find("Cursor").Find("Pointer").GetComponent<Image>();
        select = screen.transform.Find("Cursor").Find("Select").GetComponent<Image>();
        program_name = close_bar.transform.GetComponentInChildren<TextMeshProUGUI>();
        ChangeImage("pointer");

        desktop_num = 1;
        desktop = "Desktop_" + desktop_num;
        Enable(true);
    }


    public void Enable(bool option)
    {
        active = option;
        ComputerUIColliderManager.get.enabled = option;
        black_bar.SetActive(option);
        screen.SetActive(option);

        if (option)
        {
            close_bar.SetActive(false);
            ComputerAgent.get.Set(desktop);
        }
        else
        {
            if (current_window != null)
                ComputerAgent.get.Disable(current_window);
        }
    }


    private void Update()
    {
        if (active)
        {
            Move();
            MouseCollision();
        }
    }

    private void Move()
    {
        switch(InputManager.get.inputType)
        {
            case "keyboard":
                {
                    mouseInput *= mouseSensitivity*Time.deltaTime;
                    break;
                }
            default:
                {
                    if(mouseInput.magnitude>=1)
                        mouseInput = mouseInput.normalized * gamepadSensitivity * Time.deltaTime;
                    else
                        mouseInput = mouseInput * gamepadSensitivity * Time.deltaTime;

                    if (fast)
                        mouseInput *= 3f;
                    break;
                }
        }


        mouse = new Vector3(mouseInput.x, mouseInput.y, 0);
        cursor.transform.Translate(mouse);

        
        if(Mathf.Abs(cursor.transform.localPosition.x) > 720)
        {
            cursor.transform.localPosition = new Vector3 (Mathf.Sign(cursor.transform.localPosition.x) * 720, cursor.transform.localPosition.y, cursor.transform.localPosition.z);
        }

        if (Mathf.Abs(cursor.transform.localPosition.y) > 540)
        {
            cursor.transform.localPosition = new Vector3(cursor.transform.localPosition.x, Mathf.Sign(cursor.transform.localPosition.y) * 540, cursor.transform.localPosition.z);
        }
        
    }



    private void MouseCollision()
    {


        string buf_col = ComputerUIColliderManager.get.MouseCollision(cursor.transform.localPosition);

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
       
        ComputerAgent.get.Enter(mouse_collision);
    }

    public void Escape()
    {
        ComputerAgent.get.Escape(current_window);
    }

    public void Exit()
    {
        InputManager.get.SwitchGameInput(InputManager.get.gameType_last);
        Enable(false);
    }
}
