using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;


public abstract class A_InputField : MonoBehaviour
{

    protected TextMeshProUGUI field;
    [HideInInspector]
    public string text;
    protected int length;
    protected bool select = false;


    public virtual void SetInputField()
    {
        field = transform.GetComponentInChildren<TextMeshProUGUI>();
        Select(false);
    }

    public virtual void Reset()
    {
        text = "";
        Display();
    }

    public virtual void Plus(int num)
    {
        Plus(num.ToString());
    }

    protected void Plus(string symbol)
    {
        if (text.Length < length)
        {
            text += symbol;
            Display();
        }
    }

    public virtual void Backspace()
    {
        if (text.Length > 0)
        {
            text = text.Remove(text.Length - 1);
            Display();
        }
    }

    public abstract void Display();


    public virtual void Select(bool option)
    {
        select = option;
    }
}
