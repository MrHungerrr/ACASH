using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
[RequireComponent(typeof(IObjectSelect))]


public class InteractAction : MonoBehaviour, IInteraction
{
    public UnityEvent OnInteraction;


    private void Start()
    {
        this.tag = "ActionObject";
    }

    public void Interact()
    {
        OnInteraction.Invoke();
    }


    public void Remove()
    {
        Destroy(this);
    }
}
