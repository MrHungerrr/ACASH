using UnityEngine;
using System.Collections.Generic;

public class DictionaryData
{

    static DictionaryItem[] words;

    private static int FindUniqueNumber(ref bool[] busy_numbers, int number)
    {
        if (busy_numbers[number])
            return FindUniqueNumber(ref busy_numbers, number);
        else
        {
            busy_numbers[number] = true;
            return number;
        }
    }

    public static void NewDictionary()
    {
        bool[] busy_numbers = new bool[AlphabetInfo.word_count];
        List<int> numbers_list = new List<int>();
        int number;

        for (int i = 0; i < 125; i++)
        {
            number = Random.Range(0, AlphabetInfo.word_count);
            number = FindUniqueNumber(ref busy_numbers, number);
            numbers_list.Add(number);
        }

        List<DictionaryItem> words_list = new List<DictionaryItem>();
        DictionaryItem item;
        string word;

        for (int i = 0; i < 125; i++)
        {
            word = ""; //Чтение строчки из файла Words
            item = new DictionaryItem(word, numbers_list[i]);
            words_list.Add(item);
        }

        words = words_list.ToArray();
    }



}
