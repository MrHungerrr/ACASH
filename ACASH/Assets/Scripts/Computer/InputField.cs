using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;


public class InputField : MonoBehaviour
{

    TextMeshProUGUI field;
    [HideInInspector]
    public string text;
    private string text_display;
    private const int length = 4;
    private bool select = false;


    public void SetInputField()
    {
        field = transform.GetComponentInChildren<TextMeshProUGUI>();
        Select(false);
    }

    public void Reset()
    {
        if (text.Length > 0)
        {
            text = "";
            Display();
        }
    }

    public void Plus(int num)
    {
        if (text.Length < 4)
        {
            text += num.ToString();
            Display();
        }
    }

    public void Backspace()
    {
        if (text.Length > 0)
        {
            text = text.Remove(text.Length - 1);
            Display();
        }
    }

    public void Display()
    {
        text_display = text;

        for (int i = text.Length; i < length; i++)
        {
            text_display += "_";
        }

        field.text = text_display;
    }


    public void Select(bool option)
    {
        select = option;
    }
}
