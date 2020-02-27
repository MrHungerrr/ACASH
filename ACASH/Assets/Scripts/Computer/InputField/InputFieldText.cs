using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;


public class InputFieldText : A_InputField
{

    private string text_display;


    public override void SetInputField()
    {
        length = 270;
        base.SetInputField();
    }


    public override void Display()
    {
        text_display = "";

        if (text.Length > 0)
        {
            text_display += text[0];

            for (int i = 1; i < text.Length; i++)
            {
                if (i % 3 == 0)
                {
                    if (i % 30 != 0)
                    {
                        text_display += " ";
                    }
                    else
                    {
                        text_display += "\n";
                    }
                }
                text_display += text[i];
            }
        }

        field.text = text_display;
    }


    public override void Select(bool option)
    {
        base.Select(option);
    }
}
