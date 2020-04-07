using UnityEngine;


public class TriggerAction: MonoBehaviour
{
    [HideInInspector]
    public event ActionEvent.OnAction OnEnter;
    [HideInInspector]
    public event ActionEvent.OnAction OnExit;
    [HideInInspector]
    public bool inside;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && OnEnter != null)
            OnEnter();

        inside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && OnExit != null)
            OnExit();

        inside = false;
    }


    public void Remove()
    {
        ActionEvent.Unsubscribe(OnEnter);
        ActionEvent.Unsubscribe(OnExit);
        Destroy(this.gameObject);
    }



}
