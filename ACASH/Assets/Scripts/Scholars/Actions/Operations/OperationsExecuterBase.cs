using UnityEngine;
using System.Collections;

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
        Debug.Log("Я начал делать" + key);
        doing = true;
        StartCoroutine(key);
    }

    public void Do(string key, int i)
    {
        Stop();
        Debug.Log("Я начал делать" + key);
        doing = true;
        StartCoroutine(key, i);
    }

    public void Do(string key, string option)
    {
        Stop();
        Debug.Log("Я начал делать" + key);
        doing = true;
        StartCoroutine(key, option);
    }


    public void Stop()
    {
        StopAllCoroutines();
    }


    protected void OperationDone()
    {
        doing = false;
        Manager.OpeartionDone();
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

    public void Watch(Vector3 target)
    {
        Scholar.Move.SetRotateGoal(target);
    }

    public void Watch(float angle)
    {
        Scholar.Move.SetRotateGoal(Quaternion.Euler(GetRotation().eulerAngles.x, GetRotation().eulerAngles.y + angle, GetRotation().eulerAngles.z));
    }

    public void Watch(Quaternion targetRotation)
    {
        Scholar.Move.SetRotateGoal(targetRotation);
    }

    protected IEnumerator Watching()
    {
        while (Scholar.Move.rotating)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    protected bool SightIsHere()
    {
        return !Scholar.Move.rotating;
    }






    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Местоположение и передвижение

    protected void SetDestination(Vector3 goal)
    {
        Scholar.Move.SetDestination(goal);
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

        while (Scholar.Question.question)
        {
            yield return new WaitForEndOfFrame();
        }

        if (Scholar.Question.question_end)
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

        OperationDone();
    }





    //=========================================================================================================================================================
    //=========================================================================================================================================================
    // Поиск учителя


    protected IEnumerator Check()
    {
        if (!Scholar.Senses.T_here)
        {
            Scholar.Move.CheckForTeacher();

            while (Scholar.Move.checking)
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

        OperationDone();
    }







    //=========================================================================================================================================================
    //=========================================================================================================================================================
    // Проверка, можно ли продолжать цепочку действий

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
            OperationDone();
        else
            Manager.EndOfAction();
    }
}
