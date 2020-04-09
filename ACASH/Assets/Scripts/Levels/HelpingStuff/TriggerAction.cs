using UnityEngine;
using UnityEngine.Events;

public class TriggerAction: MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnEnter;
    [HideInInspector]
    public UnityEvent OnExit;
    [HideInInspector]
    public bool inside;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && OnEnter != null)
            OnEnter.Invoke();

        inside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && OnExit != null)
            OnExit.Invoke();

        inside = false;
    }


    public void Remove()
    {
        OnEnter.RemoveAllListeners();
        OnExit.RemoveAllListeners();
        Destroy(this.gameObject);
    }



}
