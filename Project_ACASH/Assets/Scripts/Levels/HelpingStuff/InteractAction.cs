using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
[RequireComponent(typeof(I_ObjectSelect))]


public class InteractAction : MonoBehaviour, I_Interaction
{
    public UnityEvent OnInteraction;


    private void Start()
    {
        this.tag = "ActionObject";
    }

    public void Interaction()
    {
        OnInteraction.Invoke();
    }


    public void Remove()
    {
        Destroy(this);
    }
}
