using UnityEngine;
using System.Collections;
using System;


#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
public class FiveDigitInt
{

    private string fivefold_number;
    private int decimal_number;



    public FiveDigitInt(bool random)
    {
        if(random)
        {
            decimal_number = UnityEngine.Random.Range(0, 624);
            this.fivefold_number = DigitSystemConversion.FromDecimal(decimal_number, 5);
        }
    }

    public FiveDigitInt(int low_range, int high_range)
    {
        decimal_number = UnityEngine.Random.Range(low_range, high_range + 1);
        this.fivefold_number = DigitSystemConversion.FromDecimal(decimal_number, 5);
    }

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





    public static FiveDigitInt R_Range(int low_range, int high_range)
    {
        int decimal_number = UnityEngine.Random.Range(low_range, high_range + 1);
        return new FiveDigitInt(decimal_number);
    }

    public static FiveDigitInt R_Max(int max_number)
    {
        int decimal_number = UnityEngine.Random.Range(0, max_number + 1);
        return new FiveDigitInt(decimal_number);
    }

    public static FiveDigitInt R_Count(int count_of_digits)
    {
        int low_range = AmountOfNumbers(count_of_digits - 1);
        int high_range = AmountOfNumbers(count_of_digits);

        return R_Range(low_range, high_range);
    }


    public static int AmountOfNumbers(int count_of_digits)
    {
        if (count_of_digits == 0)
            return 0;

        int result = 1;

        for(int i = 0; i < count_of_digits; i++)
        {
            result *= 5;
        }

        return result;
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



    public static bool operator ==(FiveDigitInt number_1, int number_2)
    {
        return number_1.decimal_number == number_2;
    }

    public static bool operator !=(FiveDigitInt number_1, int number_2)
    {
        return number_1.decimal_number != number_2;
    }

    public static bool operator >(FiveDigitInt number_1, int number_2)
    {
        return number_1.decimal_number > number_2;
    }

    public static bool operator <(FiveDigitInt number_1, int number_2)
    {
        return number_1.decimal_number < number_2;
    }

    public static bool operator <=(FiveDigitInt number_1, int number_2)
    {
        return number_1.decimal_number <= number_2;
    }

    public static bool operator >=(FiveDigitInt number_1, int number_2)
    {
        return number_1.decimal_number >= number_2;
    }






    public override string ToString()
    {
        return fivefold_number;
    }
}
