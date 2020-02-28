using UnityEngine;
using ComputerActions;


public class ScholarComputerAIController : MonoBehaviour
{
    private ScholarComputer Comp;
    [HideInInspector]
    public ScholarComputerTyping Typing { get; private set; }




    public void Setup()
    {
        Comp = GetComponent<ScholarComputer>();
        Typing = GetComponent<ScholarComputerTyping>();
        Typing.Setup(Comp);

        Transform screen = transform.Find("Screen").Find("UI").Find("Canvas").Find("Screen");
        screen.Find("Cursor").gameObject.SetActive(false);
    }




    public void Clear()
    {
        if (Comp.Numpad.IsEnabled())
        {
            Comp.Commands.Do(GetC.commands.Reset);
        }
        else
        {
            Debug.LogError("Не включен Numpad");
        }
    }
}
