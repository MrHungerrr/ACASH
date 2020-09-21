using UnityEngine;
using System.Collections;
using TMPro;

public class TextController : MonoBehaviour
{
    public InputFieldText Input { get; private set; }


    public void Setup()
    {
        Transform text = transform.Find("Text");
        Input = text.GetComponentInChildren<InputFieldText>();
        Input.SetInputField();
    }

    public void Reset()
    {
        Input.Reset();
    }

}
