using UnityEngine;
using System.Collections.Generic;


public static class Calculator
{
    public enum operations
    {
        Plus,
        Minus,
        Multiply,
        Divide,
        Mod,
    }

    public static string Calculate(string expression)
    {

        List<CalculatorItem> calculate_objects_buf = new List<CalculatorItem>();


        Recount(expression, ref calculate_objects_buf);

        List<CalculatorItem> calculate_objects = new List<CalculatorItem>();



        for (int i = 0; i < calculate_objects_buf.Count; i++)
        {
            if(IsFirstClassOperation(calculate_objects_buf[i].operation))
            {
                //Debug.Log("До - " + calculate_objects[calculate_objects.Count - 1].number.ToString());
                calculate_objects[calculate_objects.Count-1] += calculate_objects_buf[i];
                //Debug.Log("После - " + calculate_objects[calculate_objects.Count - 1].number.ToString());
            }
            else
            {
                calculate_objects.Add(calculate_objects_buf[i]);
            }
        }


        CalculatorItem result = new CalculatorItem();


        for (int i = 0; i < calculate_objects.Count; i++)
        {
            result += calculate_objects[i];
        }


        return result.number.ToString();
    }




    //Пересчет всех чисел и операций
    public static void Recount(string expression, ref List<CalculatorItem> calculate_objects)
    {
        char buf_operation; 
        string buf_number = "";

        if (expression[0] == '-')
        {
            buf_operation = expression[0];
        }
        else
        {
            buf_number += expression[0];
            buf_operation = '+';
        }


        for(int i = 1; i < expression.Length - 1; i++)
        {
            if (!IsOperation(expression[i]))
            {
                buf_number += expression[i];
            }
            else
            {
                CalculatorItem buf_object = new CalculatorItem(buf_number, buf_operation);
                calculate_objects.Add(buf_object);
                buf_number = "";
                buf_operation = expression[i];
            }
        }

        if (!IsOperation(expression[expression.Length - 1]))
        {
            buf_number += expression[expression.Length - 1];
            CalculatorItem buf_object = new CalculatorItem(buf_number, buf_operation);
            calculate_objects.Add(buf_object);
        }
        else if(buf_number != "")
        {
            CalculatorItem buf_object = new CalculatorItem(buf_number, buf_operation);
            calculate_objects.Add(buf_object);
        }

    }



    public static bool IsOperation(char symbol)
    {
        switch (symbol)
        {
            case '+':
            case '-':
            case '*':
            case ':':
            case '%':
                {
                    return true;
                }
        }

        return false;
    }

    public static bool IsFirstClassOperation(operations operation)
    {
        switch (operation)
        {
            case operations.Multiply:
            case operations.Divide:
            case operations.Mod:
                {
                    return true;
                }
        }

        return false;
    }

    public static operations Operation(char operation)
    {
        switch (operation)
        {
            case '+':
                return operations.Plus;
            case '-':
                return operations.Minus;
            case '*':
                return operations.Multiply;
            case ':':
                return operations.Divide;
            case '%':
                return operations.Mod;
        }

        return operations.Plus;
    }


    public static string Operation(operations operation)
    {
        switch (operation)
        {
            case operations.Plus:
                return "+";
            case operations.Minus:
                return "-";
            case operations.Multiply:
                return "*";
            case operations.Divide:
                return ":";
            case operations.Mod:
                return "%";
        }

        return "+";
    }

}
