using UnityEngine;


public class TriggerAction: MonoBehaviour
{
    public event ActionEvent.OnAction OnEnter;
    public event ActionEvent.OnAction OnExit;
    public event ActionEvent.OnAction OnStay;


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
        ActionEvent.Unsubscribe(OnEnter);
        ActionEvent.Unsubscribe(OnStay);
        ActionEvent.Unsubscribe(OnExit);
        Destroy(this.gameObject);
    }



}
