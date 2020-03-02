using UnityEngine;
using System.Collections.Generic;

public static class KeyWordManager
{

    public static Dictionary<string, int> quantity = new Dictionary<string, int>()
    {
        //Дикий ор
        { "Shout", 10},


        //Наезды
        { "Bull_Talking", 3},
        { "Bull_Cheating", 3},
        { "Bull_Walking", 3},
        { "Bull_Talking_Sec", 3},
        { "Bull_Cheating_Sec", 3},
        { "Bull_Walking_Sec", 3},


        //Подшучивания
        { "Joke_Talking", 3},
        { "Joke_Cheating", 3},
        { "Joke_Walking", 3},
        { "Joke_Talking_Sec", 3},
        { "Joke_Cheating_Sec", 3},
        { "Joke_Walking_Sec", 3},


        //Казнь
        { "Execute_Walking", 3},
        { "Execute_Cheating", 3},
        { "Execute_Talking", 3},


        //Вопросы
        { "Question_Toilet", 3},
        { "Question_Sink", 3},
        { "Question_Air", 3},


        //Ответы
        { "Answer", 3},
    };



    public static string GetScriptKey(KeyWord key)
    {
        if(key.GetNumber() == -1)
        {
            key += GetRandomFromLinesQuantity(key);
        }

        Debug.Log("key - " + key.GetKey());

        return key.GetFullWord();
    }


    public static int GetLinesQuantity(KeyWord key)
    {
        try
        {
            return quantity[key.GetKey()];
        }
        catch
        {
            Debug.LogError("Нет гребанного quantity key в KeyWordManager");
            return 0;
        }
    }


    public static int GetRandomFromLinesQuantity(KeyWord key)
    {
        int max = GetLinesQuantity(key);
        return Random.Range(0, max);
    }
}
