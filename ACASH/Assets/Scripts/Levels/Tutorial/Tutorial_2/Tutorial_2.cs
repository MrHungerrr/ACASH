using System.Collections;
using PlayerOptions;
using UnityEngine;
using Single;

public class Tutorial_2 : Singleton<Tutorial_2>
{

    private KeyWord key = new KeyWord("Tutorial");
    private KeyWord key_mistake = new KeyWord("Tutorial", "Mistake");



    private void Awake()
    {

    }


    private IEnumerator StartLevel()
    {
        yield return new WaitForEndOfFrame();
    }


    //===================================================================================================================================
    // Первая комната
    //===================================================================================================================================

    private void StartFirstRoom()
    {
        StartCoroutine(First_Room());
    }


    private IEnumerator First_Room()
    {
        yield return new WaitForEndOfFrame();


        //Надо сказать что-то резкое ученику


        //Надо поднять стресс до 100
        //Напоминание

        //Надо сказать ученику чуть по-мягче
        //Вам нужно сказать по-мягче, а не жестко

        //Надо поднять стресс до 30
        //Напоминание
        //Вам нужно поднимать стресс не жесткими высказываниями
    }


    private void StartSecondRoom()
    {
        StartCoroutine(Second_Room());
    }

    private IEnumerator Second_Room()
    {
        yield return new WaitForEndOfFrame();


        //Надо подойти к ученику и посмотреть на монитор
        //Напоминание


        //Надо отойти от ученика но продолжать смотреть
        //Напоминание


        //Надо зайти в компьютер
        //Напоминание


        //Зайдите в программу SS
        //Напоминание


        //Держите оптимальный стресс у всех учеников
        //Напоминание
        //Обратный отсчёт


        //Конец
    }


    private IEnumerator EndLevel()
    {
        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Elevator.get.Open();
    }
}
