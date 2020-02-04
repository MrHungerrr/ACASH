using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;


public abstract class Computer : MonoBehaviour
{
    [HideInInspector]
    public ComputerWindows Windows;
    [HideInInspector]
    public LoginController Login;
    [HideInInspector]
    public DesktopController Desktop;
    [HideInInspector]
    public StudentStress SS;
    [HideInInspector]
    public NumpadController Numpad;

    [HideInInspector]
    public string select;


    public virtual void SetComputer()
    {
        Debug.Log("Setup'им Computer");

        Transform win = transform.Find("Screen").Find("UI").Find("Canvas").Find("Windows");
        Numpad = win.parent.Find("Screen").GetComponentInChildren<NumpadController>();


        Login = win.GetComponent<LoginController>();
        Login.SetLoginController(this);

        Desktop = win.GetComponent<DesktopController>();
        Desktop.SetDesktopController();

        SS = win.GetComponent<StudentStress>();
        SS.Set();

        Debug.Log(name);

        Windows = win.GetComponent<ComputerWindows>();
        Windows.SetWindows(this);
    }

    public virtual void SetScholars()
    {
        SS.SetScholars();
    }

    public virtual void Select()
    {
        Windows.Enter(select);
    }


}
