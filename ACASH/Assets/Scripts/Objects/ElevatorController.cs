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
        text.text = "Please\nWait";
        ready = false;
    }

    public void Interaction()
    {
        Open();
        Player.get.Action.Doing(false);
    }

    public void Ready()
    {
        Elevator.get.Sound.Stop(ElevatorSounds.sounds.Move);
        text.text = "Ready";
        ready = true;
    }

    public void Close()
    {
        Elevator.get.Sound.Play(ElevatorSounds.sounds.Move);
        text.text = "Please\nWait";
        Elevator.get.Close();
        ready = false;
    }

    public void Open()
    {
        if(ready)
        {
            Elevator.get.Sound.Play(ElevatorSounds.sounds.Ring);
            Elevator.get.Open(false);
            ready = false;
        }
    }

}
