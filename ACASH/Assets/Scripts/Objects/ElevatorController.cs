using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using N_BH;

public class ElevatorController : Singleton<ElevatorController>
{

    TextMeshPro text;
    [HideInInspector]
    public bool ready;


    private void Awake()
    {
        text = transform.parent.GetComponentInChildren<TextMeshPro>();
    }

    public void Ready()
    {
        text.text = "Ready";
        ready = true;
    }

    public void Close()
    {
        text.text = "Please\nWait";
        Elevator.get.Close();
        ready = false;
    }

    public void Open()
    {
        if(ready)
        {
            ExamManager.get.ResetExam();
            Elevator.get.Open();
            ready = false;
        }
        else
        {
            Debug.Log("Please Wait");
        }
    }

}
