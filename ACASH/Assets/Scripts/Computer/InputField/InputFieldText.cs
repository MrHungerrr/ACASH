using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;


public class InputFieldText : A_InputField
{
    public override void SetInputField()
    {
        length = 256;
        base.SetInputField();
    }


    public override void Display()
    {
        field.text = text;
    }


    public override void Select(bool option)
    {
        base.Select(option);
    }
}
