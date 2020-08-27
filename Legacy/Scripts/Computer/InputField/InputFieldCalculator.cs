using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;


public class InputFieldCalculator : A_InputField
{

    [HideInInspector]
    private bool operation_last_symbol;


    public override void SetInputField()
    {
        length = 14;
        base.SetInputField();
    }

    public override void Reset()
    {
        operation_last_symbol = false;
        base.Reset();
    }

    public void Plus(Calculator.operations operation)
    {
        if (text.Length > 1 || !operation_last_symbol)
        {
            if (operation_last_symbol)
            {
                base.Backspace();
            }

            Plus(Calculator.Operation(operation));
            operation_last_symbol = true;
        }
        else if(text.Length == 0)
        {
            if (operation == Calculator.operations.Minus)
            {
                Plus(Calculator.Operation(operation));
                operation_last_symbol = true;
            }
        }
    }
    public override void Plus(int num)
    {
        operation_last_symbol = false;
        base.Plus(num);
    }

    public override void Backspace()
    {
        base.Backspace();

        if (text.Length > 0 && Calculator.IsOperation(text[text.Length - 1]))
        {
            operation_last_symbol = true;
        }
        else
        {
            operation_last_symbol = false;
        }
    }






    public override void Display()
    {
        field.text = text;
    }


}
