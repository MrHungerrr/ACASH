using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;
using TMPro;
using N_BH;


public class DeskController : MonoBehaviour
{

    private ComputerWindows Windows;

    public void SetDeskController()
    {
        Windows = GetComponent<ComputerWindows>();
        Windows.SetWindows();
        Windows.SetProgramBar(transform.Find("Program Bar").gameObject);
    }

    public void Select(string key)
    {
        Windows.Enter(key);
    }

}
