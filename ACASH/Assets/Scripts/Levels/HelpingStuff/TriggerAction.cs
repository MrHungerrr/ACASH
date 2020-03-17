using System;
using UnityEngine;
using Single;


public class TriggerAction: MonoBehaviour
{
    public delegate void OnAction();
    public event OnAction OnEnter;
    public event OnAction OnExit;
    public event OnAction OnStay;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && OnEnter != null)
            OnEnter();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && OnStay != null)
            OnStay();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && OnExit != null)
            OnExit();
    }


    public void Remove()
    {
        Unsubscribe(OnEnter);
        Unsubscribe(OnStay);
        Unsubscribe(OnExit);
        Destroy(this.gameObject);
    }


    private static void Unsubscribe(OnAction ConcreteDelegate)
    {
        if (ConcreteDelegate != null)
        {
            Delegate[] clientList = ConcreteDelegate.GetInvocationList();

            foreach (var d in clientList)
                ConcreteDelegate -= (d as OnAction);
        }
    }
}
