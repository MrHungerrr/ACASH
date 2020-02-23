using UnityEngine;
using System.Collections;
using TMPro;

public class CalculatorController : MonoBehaviour
{


    [HideInInspector]
    public InputFieldCalculator input { get; private set; }

    private TextMeshProUGUI answer;

    private string answer_number;
    private const int answer_length = 14;


    public void SetCalculator()
    {
        Transform calculator = transform.Find("Calculator");

        input = calculator.Find("Input Calculator").GetComponent<InputFieldCalculator>();
        input.SetInputField();

        answer = calculator.Find("Answer").Find("Number").GetComponent<TextMeshProUGUI>();

        Reset();
    }


    public void Reset()
    {
        input.Reset();
        SetAnswer("0");
    }

    public void Operation(Calculator.operations operation)
    {
        input.Plus(operation);
    }


    private void SetAnswer(string fivefold_number)
    {
        if (fivefold_number.Length <= answer_length)
        {
            answer_number = fivefold_number;
            answer.text = fivefold_number;
        }
        else
        {
            answer.text = "ERROR";
        }
    }




    public void Calculate()
    {
        SetAnswer(Calculator.Calculate(input.text));
    }
     
}
