using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using N_BH;

public class ComputerWindows : MonoBehaviour
{
    private Computer Comp;

    private Dictionary<string, GameObject> windows = new Dictionary<string, GameObject>();
    [HideInInspector]

    private string current_window;
    private TextMeshProUGUI program_name;
    private GameObject program_bar;
    private GameObject task_bar;


    public void SetWindows(Computer c)
    {
        Transform buf = transform.parent.Find("Screen");

        task_bar = buf.Find("Task Bar").gameObject;
        program_bar = buf.Find("Program Bar").gameObject;
        program_name = program_bar.transform.GetComponentInChildren<TextMeshProUGUI>();

        GameObject[] comp_windows = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            comp_windows[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject i in comp_windows)
        {
            windows.Add(i.name, i);
            i.SetActive(false);
        }

        Comp = c;

        Enter("Login");
    }

    public void Enter(string type)
    {
        switch (type)
        {
            case "Login":
                {
                    Disable();
                    CloseProgram();
                    EnableTaskBar(false);
                    Comp.Numpad.Enable(true);
                    Comp.Login.Reset();
                    Set(type);
                    break;
                }
            case "Desktop":
                {
                    Disable();
                    CloseProgram();
                    EnableTaskBar(true);
                    Comp.Numpad.Enable(false);
                    Set(type);
                    break;
                }
            case "Student Stress":
                {
                    Disable("Desktop");
                    SetProgram(type);
                    Comp.SS.Refresh();
                    Set(type);
                    break;
                }
            case "Overwatch":
                {
                    Disable("Desktop");
                    SetProgram(type);
                    Set(type);
                    break;
                }
            case "Info":
                {
                    Disable("Desktop");
                    SetProgram(type);
                    Set(type);
                    break;
                }
            case "Refresh":
                {
                    Comp.SS.Refresh(); 
                    break;
                }
            case "Input Field Login":
                {
                    Comp.Numpad.Set(Comp.Login.login);
                    break;
                }
            case "Input Field Password":
                {
                    Comp.Numpad.Set(Comp.Login.password);
                    break;
                }
            case "Log In Computer":
                {
                    Comp.Login.TryLogin();
                    break;
                }
            case "0":
                {
                    Comp.Numpad.Plus(0);
                    break;
                }
            case "1":
                {
                    Comp.Numpad.Plus(1);
                    break;
                }
            case "2":
                {
                    Comp.Numpad.Plus(2);
                    break;
                }
            case "3":
                {
                    Comp.Numpad.Plus(3);
                    break;
                }
            case "4":
                {
                    Comp.Numpad.Plus(4);
                    break;
                }
            case "Backspace":
                {
                    Comp.Numpad.Backspace();
                    break;
                }
            case "Close":
                {
                    Escape(current_window);
                    break;
                }
            case "Exit":
                {
                    Enter("Login");
                    break;
                }
            default:
                {
                    Debug.Log("Несуществуюющее окно - " + type);
                    break;
                }
        }
    }


    public void Escape(string type)
    {
        Disable(type);

        switch (type)
        {
            case "Student Stress":
                {
                    CloseProgram();
                    Set("Desktop");
                    break;
                }
            case "Overwatch":
                {
                    CloseProgram();
                    Set("Desktop");
                    break;
                }
            case "Info":
                {
                    CloseProgram();
                    Set("Desktop");
                    break;
                }
        }
    }

    public void Set(string window)
    {
        //Debug.Log(window);
        windows[window].SetActive(true);
        current_window = window;
    }


    public void Disable(string window)
    {
        windows[window].SetActive(false);
    }

    public void DisableAll()
    {
        foreach (KeyValuePair<string, GameObject> window in windows)
        {
            window.Value.SetActive(false);
        }
    }

    public void Disable()
    {
        if (current_window != null)
        {
            windows[current_window].SetActive(false);
            current_window = null;
        }
    }


    public void SetProgram(string n)
    {
        program_bar.SetActive(true);
        program_name.text = n;
    }

    public void CloseProgram()
    {
        program_bar.SetActive(false);
        program_name.text = null;
    }

    public void EnableTaskBar(bool option)
    {
        task_bar.SetActive(option);
    }
}
