using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;


public class InputField : MonoBehaviour
{

    TextMeshProUGUI field;
    private string text;
    private string text_display;
    private const int length = 4;


    private void SetInputField()
    {
        field = transform.GetComponentInChildren<TextMeshProUGUI>();
    }


    public void Reset()
    {
        
    }

    public void Plus(int num)
    {
        if (text.Length < 4)
        {
            text += num.ToString();
            Set();
        }
    }

    public void Backspace()
    {
        if (text.Length > 0)
        {
            text = text.Remove(text.Length - 1);
            Set();
        }
    }


    private void Set()
    {

            
    }
}
