using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using N_BH;

public class ComputerWindows : MonoBehaviour
{

    private Dictionary<string, GameObject> windows = new Dictionary<string, GameObject>();
    [HideInInspector]

    private string current_window;
    private TextMeshProUGUI program_name;
    private GameObject program_bar;
    private GameObject task_bar;


    public void SetBars(GameObject program, GameObject task)
    {
        task_bar = task;
        program_bar = program;
        program_name = program_bar.transform.transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetWindows()
    {
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

        Enter("Desktop");
    }

    public void Enter(string type)
    {
        switch (type)
        {
            case "Desktop":
                {
                    Disable();
                    CloseProgram();
                    Set(type);
                    break;
                }
            case "Student Stress":
                {
                    Disable("Desktop");
                    SetProgram(type);
                    StudentStress.get.Refresh();
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
                    StudentStress.get.Refresh();
                    break;
                }
            case "Close":
                {
                    Escape(current_window);
                    break;
                }
            case "Exit":
                {
                    //
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
}
