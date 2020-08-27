using UnityEngine;
using System;

public class DictionaryWord
{
    public int word_number;
    public string word_code;


    public DictionaryWord(int word_number)
    {
        if (word_number < 0 || word_number >= AlphabetInfo.word_count)
        {
            Debug.LogError("В Dictionary Word подано неправильное число - " + word_number);
        }
        else
        {
            this.word_number = word_number;
            word_code = IntToWord(this.word_number);
        }
    }



    private static string IntToWord(int number)
    {
        int max_limit = 0;
        int low_limit;
        int size = 0;

        for (int i = 1; i < AlphabetInfo.max_length + 1; i++)
        {
            low_limit = max_limit;
            max_limit = low_limit + (int)Math.Pow(AlphabetInfo.size, i);


            if (number < max_limit)
            {
                number -= low_limit;
                size = i;
                break;
            }
        }

        string result = DigitSystemConversion.FromDecimal(number, AlphabetInfo.size);


        for(int i = result.Length; i < size; i++)
        {
            result = "0" + result;
        }

        return result;
    }
}
