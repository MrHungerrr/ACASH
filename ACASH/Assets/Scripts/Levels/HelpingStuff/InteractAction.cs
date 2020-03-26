using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(I_ObjectSelect))]


public class InteractAction : MonoBehaviour, I_Interaction
{
    public event ActionEvent.OnAction OnInteraction;


    private void Start()
    {
        this.tag = "ActionObject";
    }

    public void Interaction()
    {
        if (OnInteraction != null)
            OnInteraction();
    }


    public void Remove()
    {
        ActionEvent.Unsubscribe(OnInteraction);
        Destroy(this);
    }
}
