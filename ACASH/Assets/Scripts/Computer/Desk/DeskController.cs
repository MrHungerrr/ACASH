using UnityEngine;


public class DeskController : MonoBehaviour
{

    private ComputerWindows Windows;

    public void SetDeskController()
    {
        Debug.Log("Setup'им DeskController");

        Transform buf = transform.Find("Desk Screen").Find("Desk UI").Find("Canvas");
        Transform screen = buf.Find("Screen");

        Windows = buf.GetComponentInChildren<ComputerWindows>();
        Windows.SetBars(screen.Find("Program Bar").gameObject, screen.Find("Task Bar").gameObject);
        Windows.SetWindows();
    }

    public void Select(string key)
    {
        Windows.Enter(key);
    }

}
