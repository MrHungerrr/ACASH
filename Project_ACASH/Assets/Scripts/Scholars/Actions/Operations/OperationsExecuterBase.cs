﻿using UnityEngine;
using System.Collections;
using Animations;
using ScholarOptions;

public abstract class OperationsExecuterBase : MonoBehaviour
{
    protected Scholar Scholar;
    protected OperationsManager Manager;

    public delegate bool VerifyDelegate();

    [HideInInspector]
    public bool doing;

    /*
    protected int actionNum;
    protected int actionNoPlus;
    */


    public void SetOperationsExecuter(Scholar Scholar, OperationsManager Manager)
    {
        this.Scholar = Scholar;
        this.Manager = Manager;
    }


    public void Do(string key)
    {
        Stop();
        //Debug.Log("Я начал делать" + key);
        doing = true;
        StartCoroutine(key);
    }

    public void Do(string key, int i)
    {
        Stop();
        //Debug.Log("Я начал делать " + key);
        doing = true;
        StartCoroutine(key, i);
    }

    public void Do(string key, string option)
    {
        Stop();
        //Debug.Log("Я начал делать" + key);
        doing = true;
        StartCoroutine(key, option);
    }



    public void Stop()
    {
        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        StopAllCoroutines();
    }


    protected void OperationEnd()
    {
        doing = false;
        Manager.OperationDone();
    }


    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Продолжить незаконченное действие

    /* public void Continue(string key)
     {
         actionNoPlus = 0;
         StartCoroutine(key);
         doing = true;
     }
     */



    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Функция для запоминания этапа действия , если действие прервется
    /*
    protected bool CanIContinue()
    {
        if (actionNum == actionNoPlus)
        {
            actionNoPlus++;
            return true;
        }
        else
        {
            actionNoPlus++;
            return false;
        }
    }
    */




    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Зрение

    protected void WatchTo(PlaceManager.place place, int index)
    {
        Watch(PlaceManager.get.GetSightGoal(place, index));
    }

    protected void Watch(Vector3 target)
    {
        Scholar.Move.SetRotateGoal(target);
    }

    protected void Watch(float angle)
    {
        Scholar.Move.SetRotateGoal(angle);
    }

    protected void Watch(Quaternion targetRotation)
    {
        Scholar.Move.SetRotateGoal(targetRotation);
    }

    protected bool SightIsHere()
    {
        return !Scholar.Move.rotating;
    }






    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Местоположение и передвижение

    public void GoTo(PlaceManager.place place, int index)
    {
        Stop();
        //Debug.Log("Я начал делать" + key);
        doing = true;
        StartCoroutine(Go_To(place, index));
    }

    public void GoTo(PlaceManager.place place)
    {
        int index = PlaceManager.get.GetRandomFreePlaceIndex(place);
        GoTo(place, index);
    }

    protected bool IsHere()
    {
        return !Scholar.Move.walking;
    }

    protected Quaternion GetRotation()
    {
        return Scholar.Move.Rotation();
    }

    protected Vector3 GetPosition()
    {
        return Scholar.Move.Position();
    }





    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Вопрос



    protected IEnumerator Question(string question)
    {
        Scholar.Question.Ask(question);

        while (Scholar.Question.question || Scholar.Talk.talking)
        {
            yield return new WaitForEndOfFrame();
        }

        if (Scholar.Question.question_answered)
        {
            if (Scholar.Question.answer)
            {
                //Учитель ответил положительно
            }
            else
            {
                //Учитель ответил отрицательно
            }
        }
        else
        {
            //Учитель не ответил
        }

        OperationEnd();
    }





    //=========================================================================================================================================================
    //=========================================================================================================================================================
    // Поиск учителя


    protected IEnumerator Check()
    {
        if (!Scholar.Senses.T_here)
        {
            Scholar.Check.StartCheck();

            Debug.Log("Проверяю учителя");

            while (Scholar.Check.checking)
            {
                yield return new WaitForEndOfFrame();
            }


            if (Scholar.Senses.T_here)
            {
                // Учитель здесь
            }
            else
            {
                // Учитель не здесь
            }
        }
        else
        {
            // Учитель здесь
        }

        OperationEnd();
    }






    //=========================================================================================================================================================
    //=========================================================================================================================================================
    // Ожидание


    protected IEnumerator Wait(int option)
    {
        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        yield return new WaitForSeconds(option);

        OperationEnd();
    }





    //=========================================================================================================================================================
    //=========================================================================================================================================================
    // Начало и конец списывание


    protected IEnumerator StartCheat()
    {
        Scholar.Cheat.StartCheat();
        yield return new WaitForEndOfFrame();

        OperationEnd();
    }

    protected IEnumerator EndCheat()
    {
        Scholar.Cheat.EndCheat();
        yield return new WaitForEndOfFrame();

        OperationEnd();
    }





    //=========================================================================================================================================================
    //=========================================================================================================================================================
    // Проверка, можно ли продолжать цепочку действий

    public bool VerifyToiletAreFree()
    {
        return PlaceManager.get.IsFree(PlaceManager.place.Toilet);
    }

    public bool VerifySinkAreFree()
    {
        return PlaceManager.get.IsFree(PlaceManager.place.Sink);
    }

    public bool VerifyOutsideAreFree()
    {
        return PlaceManager.get.IsFree(PlaceManager.place.Outside);
    }

    public bool VerifyTeacherIsHere()
    {
        return Scholar.Senses.T_here;
    }

    public bool VerifyAnswer()
    {
        return Scholar.Question.answer;
    }

    public void Verify(VerifyDelegate typeOfCheck, bool need_condition)
    {
        if (typeOfCheck() == need_condition)
            OperationEnd();
        else
            Manager.ActionEnd();
    }





    //=========================================================================================================================================================
    //Исключение

    private IEnumerator Execute()
    {
        yield return new WaitForSeconds(2f);


        if (Scholar.Execute.executed)
        {
            Scholar.Talk.SayWithoutStop(Scholar.Execute.execute_key);
        }
        else
        {
            Scholar.Execute.LastWord();
        }
    }



    //=========================================================================================================================================================
    // Идти домой
    private IEnumerator Go_To(PlaceManager.place place, int index)
    {

        Scholar.Location.ChangeLocation(place, index);

        while (!IsHere())
            yield return new WaitForEndOfFrame();

        WatchTo(PlaceManager.place.Home, Scholar.Info.number);
        Scholar.Body.Disable();

        yield return new WaitForEndOfFrame();

        Scholar.Move.RB.isKinematic = false;

        OperationEnd();
    }

}