using UnityEngine;
using System.Collections;

public class OperationsExecuter : OperationsExecuterBase
{

    //=========================================================================================================================================================
    // Основной действие школьника - написание экзамена.

    private IEnumerator Writing()
    {
        SetDestination(Scholar.Action.home);

        while (!IsHere())
            yield return new WaitForEndOfFrame();

        Watch(Scholar.Action.desk);

        Scholar.Test.writing = true;

        Debug.Log("Я думаю");
        Scholar.Anim.SetAnimation("Nothing");

        yield return new WaitForSeconds(Random.Range(4, 5));
        Debug.Log("Я пишу");
        Scholar.Anim.SetAnimation("Writing");

        yield return new WaitForSeconds(Random.Range(2, 7));

        EndDo();
    }






    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Список всех действий доступных ученикам.
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================

    //=========================================================================================================================================================
    //Выход в туалет


    private IEnumerator Toilet_1()
    {
        if (CanIContinue())
        {
            Debug.Log("пошел");
            SetDestination(ScholarManager.get.GetPlace("toilet", 0));

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            Watch(ScholarManager.get.GetSightGoal("toilet", 0));

            Debug.Log("Я дошел");
            actionNum++;
        }

        if (CanIContinue())
        {
            Scholar.Anim.SetAnimation("Toilet");
            yield return new WaitForSeconds(5f);
            Debug.Log("Сделал свои дела");
            actionNum++;
        }

        if (CanIContinue())
        {
            SetDestination(Scholar.Action.home);

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            actionNum++;
        }

        if (CanIContinue())
        {
            EndDo();
        }
    }




    //=========================================================================================================================================================
    //Выход в туалет

    private IEnumerator Sink_1()
    {
        if (CanIContinue())
        {
            Debug.Log("пошел");
            SetDestination(ScholarManager.get.GetPlace("sink", 0));

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            Watch(ScholarManager.get.GetSightGoal("sink", 0));

            Debug.Log("Я дошел");
            actionNum++;
        }

        if (CanIContinue())
        {
            Scholar.Anim.SetAnimation("Sink");
            yield return new WaitForSeconds(5f);
            Debug.Log("Сделал свои дела");
            actionNum++;
        }

        if (CanIContinue())
        {
            SetDestination(Scholar.Action.home);

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            actionNum++;
        }

        if (CanIContinue())
        {
            EndDo();
        }
    }




    //=========================================================================================================================================================
    //Выход подышать воздухом

    private IEnumerator Air_1()
    {

        if (CanIContinue())
        {
            Debug.Log("пошел");
            SetDestination(ScholarManager.get.GetPlace("outside", 0));

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            Watch(ScholarManager.get.GetSightGoal("outside", 0));

            actionNum++;
        }

        if (CanIContinue())
        {
            Scholar.Anim.SetAnimation("Think_Outside");
            yield return new WaitForSeconds(5f);
            Debug.Log("Сделал свои дела");
            actionNum++;
        }

        if (CanIContinue())
        {
            SetDestination(Scholar.Action.home);

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            actionNum++;
        }


        if (CanIContinue())
        {
            EndDo();
        }
    }




    //=========================================================================================================================================================
    //Думание вслух

    private IEnumerator Think_Aloud_1()
    {
        Scholar.Anim.SetAnimation("Think_Aloud_1");
        Scholar.Talk.SayThoughts("Think_Aloud_1");

        yield return new WaitForSeconds(8f);

        EndDo();
    }




    //=========================================================================================================================================================
    //Догадывание

    private IEnumerator Guesses_1()
    {
        Scholar.Anim.SetAnimation("Guesses");

        yield return new WaitForSeconds(5f);

        Scholar.Anim.SetAnimation("HasGuessed");
        Debug.Log("Я догодалася");

        yield return new WaitForSeconds(1f);

        EndDo();
    }




    //=========================================================================================================================================================
    //Использование различных программ

    private IEnumerator Program_1(string program)
    {
        Scholar.Anim.SetAnimation("Working_On_Computer");
        Scholar.Desk.Select(program);

        yield return new WaitForSeconds(8f);

        EndDo();
    }





    //=========================================================================================================================================================
    //Домой

    private IEnumerator Go_Home()
    {
        SetDestination(Scholar.Action.home);

        while (!IsHere())
            yield return new WaitForEndOfFrame();

        Watch(Scholar.Action.desk);

        EndDo();
    }



    //=========================================================================================================================================================
    //Исключение

    private IEnumerator Execute()
    {
        Scholar.Selectable(false);
        Scholar.executed = true;
        yield return new WaitForSeconds(1f);

        Scholar.Emotions.ChangeEmotion("dead");
        Scholar.Stop();
    }















































    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Списывание
    //=========================================================================================================================================================




}
