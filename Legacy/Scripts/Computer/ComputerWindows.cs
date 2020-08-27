using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Single;

public class ComputerWindows : MonoBehaviour
{
    private A_Computer Computer;

    private Dictionary<string, GameObject> windows;

    [HideInInspector]
    public string current_window;
    private TextMeshProUGUI program_name;
    private GameObject program_bar;
    private GameObject task_bar;


    public void Setup(A_Computer c)
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

        windows = new Dictionary<string, GameObject>();

        foreach (GameObject i in comp_windows)
        {
            windows.Add(i.name, i);
            i.SetActive(false);
        }

        current_window = null;

        Computer = c;

        program_bar.SetActive(false);
        program_name.text = null;
    }



    public void Set(string window)
    {
        Debug.LogWarning(window);
        Disable();
        windows[window].SetActive(true);
        current_window = window;
    }

    public void SetWithoutDisable(string window)
    {
        //Debug.Log(window);
        windows[window].SetActive(true);
        current_window = window;
    }


    public void Disable(string window)
    {
        Debug.LogWarning(window);
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
            Disable(current_window);
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
