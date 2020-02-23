using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;


public class InputFieldCode : A_InputField
{

    private GameObject arrow;
    [HideInInspector]
    private string text_display;


    public override void SetInputField()
    {
        length = 4;
        arrow = transform.Find("Arrow").gameObject;
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
        arrow.SetActive(option);
    }
}
