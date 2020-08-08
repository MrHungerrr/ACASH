using System;


/// <summary>
/// Работает только с системами ниже десятеричной
/// </summary>
public static class DigitSystemConversion
{

    public static int ToDecimal(string number, int system_base)
    {
        if (number == string.Empty)
        {
            return 0;
        }

        char buf;
        int factor = 1;
        int result = 0;
        for(int i = number.Length - 1; i >= 0; i--)
        {
            buf = number[i];
            result += Int32.Parse(buf.ToString()) * factor;
            factor *= system_base;
        }

        return result;
    }


    public static string FromDecimal(int number, int system_base)
    {
        if (number == 0)
        {
            return "0";
        }

        int buf;
        string result = string.Empty;
        bool negative = false;

        if (number < 0)
        {
            negative = true;
            number = Math.Abs(number);
        }

        while(number > 0)
        {
            buf = number % system_base;
            result = buf.ToString() + result;
            number /= system_base;
        }

        if(negative)
        {
            result = "-" + result;
        }

        return result;
    }


    public static string VariousSystems(string number, int from_system_base, int to_system_base)
    {
        int demical = ToDecimal(number, from_system_base);
        return FromDecimal(demical, to_system_base);
    }


}
