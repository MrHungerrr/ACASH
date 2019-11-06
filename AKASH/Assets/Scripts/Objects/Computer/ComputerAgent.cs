using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;

public class ComputerAgent : MonoBehaviour
{

    private Dictionary<string, GameObject> windows = new Dictionary<string, GameObject>();
    [HideInInspector]
    public ComputerController CompControl;

    private void Awake()
    {
        GameObject[] comp_windows = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            comp_windows[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject i in comp_windows)
        {
            windows.Add(i.name, i);
            Debug.Log(i.name);
            i.SetActive(false);
        }
    }

    public void Enter(string type)
    {
        switch (type)
        {
            case "Student Stress":
                {
                    Disable(CompControl.desktop);
                    CompControl.SetProgram(type);
                    StudentStress.get.Refresh();
                    Set(type);
                    break;
                }
            case "Overwatch":
                {
                    Disable(CompControl.desktop);
                    CompControl.SetProgram(type);
                    Set(type);
                    break;
                }
            case "Info":
                {
                    Disable(CompControl.desktop);
                    CompControl.SetProgram(type);
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
                    Escape(CompControl.current_window);
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
        switch (type)
        {
            case "Student Stress":
                {
                    Disable(type);
                    CompControl.CloseProgram();
                    Set("Desktop_" + CompControl.desktop_num);
                    break;
                }
            case "Overwatch":
                {
                    Disable(type);
                    CompControl.CloseProgram();
                    Set("Desktop_" + CompControl.desktop_num);
                    break;
                }
            case "Info":
                {
                    Disable(type);
                    CompControl.CloseProgram();
                    Set("Desktop_" + CompControl.desktop_num);
                    break;
                }
        }
    }

    public void Set(string window)
    {
        Debug.Log(window);
        windows[window].SetActive(true);
        CompControl.current_window = window;
    }


    public void Disable(string window)
    {
        windows[window].SetActive(false);
    }
}
