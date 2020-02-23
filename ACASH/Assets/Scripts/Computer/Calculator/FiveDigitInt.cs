using UnityEngine;
using System.Collections;
using System;


#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
public class FiveDigitInt
{

    private string fivefold_number;
    private int decimal_number;



    public FiveDigitInt()
    {
        this.fivefold_number = "0";
        decimal_number = 0;
    }

    public FiveDigitInt(string fivefold_number)
    {
        this.fivefold_number = fivefold_number;
        decimal_number = DigitSystemConversion.ToDecimal(fivefold_number, 5);
    }

    public FiveDigitInt(int decimal_number)
    {
        this.decimal_number = decimal_number;
        fivefold_number = DigitSystemConversion.FromDecimal(decimal_number, 5);
    }






    public static FiveDigitInt operator +(FiveDigitInt number_1, FiveDigitInt number_2)
    {
        int decimal_number = number_1.decimal_number + number_2.decimal_number;
        return new FiveDigitInt(decimal_number);
    }

    public static FiveDigitInt operator -(FiveDigitInt number_1, FiveDigitInt number_2)
    {
        int decimal_number = number_1.decimal_number - number_2.decimal_number;
        return new FiveDigitInt(decimal_number);
    }

    public static FiveDigitInt operator *(FiveDigitInt number_1, FiveDigitInt number_2)
    {
        int decimal_number = number_1.decimal_number * number_2.decimal_number;
        return new FiveDigitInt(decimal_number);
    }

    public static FiveDigitInt operator /(FiveDigitInt number_1, FiveDigitInt number_2)
    {
        try
        {
            int decimal_number = number_1.decimal_number / number_2.decimal_number;
            return new FiveDigitInt(decimal_number);
        }
        catch
        {
            return new FiveDigitInt(0);
        }
    }

    public static FiveDigitInt operator %(FiveDigitInt number_1, FiveDigitInt number_2)
    {
        try
        {
            int decimal_number = number_1.decimal_number % number_2.decimal_number;
            return new FiveDigitInt(decimal_number);
        }
        catch
        {
            return new FiveDigitInt(0);
        }
    }






    //==============================================================================================
    // Сравнение 

    public static bool operator ==(FiveDigitInt number_1, FiveDigitInt number_2)
    {
        return number_1.decimal_number == number_2.decimal_number;
    }

    public static bool operator !=(FiveDigitInt number_1, FiveDigitInt number_2)
    {
        return number_1.decimal_number != number_2.decimal_number;
    }

    public static bool operator >(FiveDigitInt number_1, FiveDigitInt number_2)
    {
        return number_1.decimal_number > number_2.decimal_number;
    }

    public static bool operator <(FiveDigitInt number_1, FiveDigitInt number_2)
    {
        return number_1.decimal_number < number_2.decimal_number;
    }

    public static bool operator <=(FiveDigitInt number_1, FiveDigitInt number_2)
    {
        return number_1.decimal_number <= number_2.decimal_number;
    }

    public static bool operator >=(FiveDigitInt number_1, FiveDigitInt number_2)
    {
        return number_1.decimal_number >= number_2.decimal_number;
    }






    public override string ToString()
    {
        return fivefold_number;
    }
}
