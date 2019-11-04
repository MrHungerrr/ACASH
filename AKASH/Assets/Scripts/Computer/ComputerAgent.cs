using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;

public class ComputerAgent : Singleton<ComputerAgent>
{

    [HideInInspector]
    private Dictionary<string, GameObject> windows = new Dictionary<string, GameObject>();
    private LayerMask visible_layerMask;
    private LayerMask not_visible_layerMask;

    private void Awake()
    {
        GameObject[] comp_windows = GameObject.FindGameObjectsWithTag("Window");


        foreach (GameObject i in comp_windows)
        {
            windows.Add(i.name, i);
            i.SetActive(false);
        }
    }



    public void Enter(string type)
    {
        switch(type)
        {
            case "Student Stress":
                {
                    Disable(ComputerController.get.desktop);
                    ComputerController.get.SetProgram(type);
                    StudentStress.get.Refresh();
                    Set(type);

                    break;
                }
            case "Overwatch":
                {
                    Disable(ComputerController.get.desktop);
                    ComputerController.get.SetProgram(type);
                    Set(type);
                    break;
                }
            case "Info":
                {
                    Disable(ComputerController.get.desktop);
                    ComputerController.get.SetProgram(type);
                    Set(type);
                    break;
                }
            case "Close":
                {
                    Escape(ComputerController.get.current_window);
                    break;
                }
            case "Exit":
                {
                    ComputerController.get.Exit();
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
                    ComputerController.get.CloseProgram();
                    Set("Desktop_" + ComputerController.get.desktop_num);
                    break;
                }
            case "Overwatch":
                {
                    Disable(type);
                    ComputerController.get.CloseProgram();
                    Set("Desktop_" + ComputerController.get.desktop_num);
                    break;
                }
            case "Info":
                {
                    Disable(type);
                    ComputerController.get.CloseProgram();
                    Set("Desktop_" + ComputerController.get.desktop_num);
                    break;
                }
            case "Desktop_1":
                {
                    ComputerController.get.Exit();
                    break;
                }
            case "Desktop_2":
                {
                    ComputerController.get.Exit();
                    break;
                }
            case "Desktop_3":
                {
                    ComputerController.get.Exit();
                    break;
                }
        }
    }

    public void Set(string window)
    {
        windows[window].SetActive(true);
        ComputerController.get.current_window = window;
    }


    public void Disable(string window)
    {
        windows[window].SetActive(false);
    }
}
