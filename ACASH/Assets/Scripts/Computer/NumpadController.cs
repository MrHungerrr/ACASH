using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class NumpadController : MonoBehaviour
{
    [HideInInspector]
    public A_InputField input_field { get; private set;}

    public bool IsEnabled()
    {
        return gameObject.activeSelf;
    }

    public void Enable(bool option)
    {
        this.gameObject.SetActive(option);
    }

    public void Set(A_InputField new_input)
    {
        if (input_field != null)
            input_field.Select(false);

        input_field = new_input;
        input_field.Select(true);
    }

    public void Reset()
    {
        if (input_field != null)
            input_field.Reset();
    }

    public void Plus(int num)
    {
        if(input_field != null)
            input_field.Plus(num);
    }

    public void Backspace()
    {
        if (input_field != null)
            input_field.Backspace();
    }
}
