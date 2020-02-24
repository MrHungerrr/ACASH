using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;


public class InputFieldSingle : A_InputField
{

    [HideInInspector]
    private string text_display;


    public override void SetInputField()
    {
        length = 3;
        base.SetInputField();
    }


    public override void Display()
    {
        text_display = text;

        for (int i = text.Length; i < length; i++)
        {
            text_display += "_";
        }

        field.text = text_display;
    }


    public override void Select(bool option)
    {
        base.Select(option);
    }
}
