using UnityEngine;
using System.Collections;
using System;


public class CalculatorItem
{

    public Calculator.operations operation;
    public FiveDigitInt number;

    public CalculatorItem()
    {
        this.number = new FiveDigitInt();
        this.operation = Calculator.operations.Plus;
    }

    public CalculatorItem(FiveDigitInt number, Calculator.operations operation)
    {
        this.number = number;
        this.operation = operation;
    }

    public CalculatorItem(string number, char operation)
    {
        this.number = new FiveDigitInt(number);
        this.operation = Calculator.Operation(operation);
    }

    private CalculatorItem(CalculatorItem obj_1, CalculatorItem obj_2)
    {
        this.number = Merge(obj_1, obj_2);
        this.operation = obj_1.operation;
    }



    private FiveDigitInt Merge(CalculatorItem obj_1 , CalculatorItem obj_2)
    {
        switch(obj_2.operation)
        {
            case Calculator.operations.Plus:
                {
                    return obj_1.number + obj_2.number;
                }
            case Calculator.operations.Minus:
                {
                    return obj_1.number - obj_2.number;
                }
            case Calculator.operations.Multiply:
                {
                    return obj_1.number * obj_2.number;
                }
            case Calculator.operations.Divide:
                {
                    return obj_1.number / obj_2.number;
                }
            case Calculator.operations.Mod:
                {
                    return obj_1.number % obj_2.number;
                }
        }
        return null;
    }



    public static CalculatorItem operator +(CalculatorItem obj_1, CalculatorItem obj_2)
    {
        return new CalculatorItem(obj_1, obj_2);
    }

}
