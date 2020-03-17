using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Single;

public class ElevatorController : Singleton<ElevatorController>, I_Interaction
{

    TextMeshPro text;
    [HideInInspector]
    public bool ready;


    private void Awake()
    {
        text = transform.parent.GetComponentInChildren<TextMeshPro>();
    }

    public void Interaction()
    {
        Open();
        Player.get.Action.Doing(false);
    }

    public void Ready()
    {
        Elevator.get.Sound.Make(ElevatorSounds.one_shot.Ring);
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
            Elevator.get.Open();
            ready = false;
        }
        else
        {
            Debug.Log("Please Wait");
        }
    }

}
