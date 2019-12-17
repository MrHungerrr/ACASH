using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElevatorScreen : MonoBehaviour
{

    TextMeshPro text;
    private bool ready;
    

    public void Ready()
    {

    }

    public void PleaseWait()
    {

    }



    public void Open()
    {
        if(ready)
        {
            Elevator.get.Open(false);
            ready = false;
        }
        else
        {
            Debug.Log("Please Wait");
        }
    }

}
