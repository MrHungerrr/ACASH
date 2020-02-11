using UnityEngine;
using System.Collections;

public abstract class ActionsBase : MonoBehaviour
{
    protected Scholar Scholar;


    public bool doing;
    protected int actionNum;
    protected int actionNoPlus;

    protected string last_key_action;
    protected string key_action;

    protected bool checking;




    public void Doing(string key)
    { 
        Stop();
        Debug.Log("Я начал делать" + key);
        doing = true;
        actionNum = 0;
        actionNoPlus = 0;
        StartCoroutine(key);
    }

    public void Doing(string key, int i)
    {
        Stop();
        Debug.Log("Я начал делать" + key);
        doing = true;
        actionNum = 0;
        actionNoPlus = 0;
        StartCoroutine(key, i);
    }

    public void Doing(string key, string option)
    {
        Stop();
        Debug.Log("Я начал делать" + key);
        doing = true;
        actionNum = 0;
        actionNoPlus = 0;
        StartCoroutine(key, option);
    }


    public void Stop()
    {
        StopAllCoroutines();
        EndOfDoing();
    }


    protected void EndOfDoing()
    {
        doing = false;
        Говорить скуларАкшинс что закончил
    }

    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Продолжить незаконченное действие

    public void Continue(string key)
    {
        actionNoPlus = 0;
        StartCoroutine(key);
        doing = true;
    }



    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Функция для запоминания этапа действия , если действие прервется

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





    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Зрение

    public void Watch(Vector3 target)
    {
        Scholar.Move.SetSightGoal(BaseGeometry.GetQuaternionTo(Scholar.Move.transform, target));
    }

    public void Watch(float angle)
    {
        Scholar.Move.SetSightGoal(Quaternion.Euler(GetRotation().eulerAngles.x, GetRotation().eulerAngles.y + angle, GetRotation().eulerAngles.z));
    }

    public void Watch(Quaternion targetRotation)
    {
        Scholar.Move.SetSightGoal(targetRotation);
    }

    protected IEnumerator Watching()
    {
        while (Scholar.Move.watching)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    private bool SightIsHere()
    {
        return !Scholar.Move.watching;
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

    public void QuestionBeforeAct(string question, string ActionYes, string ActionNo)
    {
        Scholar.Stop();
        StartCoroutine(Question(question, ActionYes, ActionNo));
    }

    public void QuestionBeforeAct(string question, string ActionYes)
    {
        Scholar.Stop();
        StartCoroutine(Question(question, ActionYes, "Writing"));
    }

    public void JustQuestion(string question)
    {
        Scholar.Stop();
        StartCoroutine(Question(question, "Writing", "Writing"));
    }




    protected IEnumerator Question(string question, string ActionYes, string ActionNo)
    {
        if (CanIContinue())
        {
            if (!Scholar.Senses.T_here)
            {
                CheckForTeacher();
                yield return new WaitForEndOfFrame();

                while (checking)
                {
                    yield return new WaitForEndOfFrame();
                }

                if (Scholar.Senses.T_here)
                {
                    actionNum++;
                    Scholar.Stress.Change(-10);
                    yield return new WaitForSeconds(0.7f);
                }
                else
                {
                    Scholar.Stress.Change(+10);
                    Scholar.Action.StartWriting();
                }
            }
            else
            {
                actionNum++;
            }
        }

        if (CanIContinue())
        {

            while (Scholar.Question.question)
            {
                yield return new WaitForEndOfFrame();
            }

            if (Scholar.Question.question_end)
            {
                if (Scholar.Question.answer)
                {
                    Scholar.Action.Doing(ActionYes);
                }
                else
                {
                    Scholar.Action.Doing(ActionNo);
                }
            }
            else
            {
                Scholar.Action.Doing(ActionNo);
            }
        }
    }





    //=========================================================================================================================================================
    //=========================================================================================================================================================
    // Поиск учителя

    protected void CheckForTeacher()
    {
        checking = true;
        StartCoroutine("Check_For_Teacher");
    }

    public void CheckBeforeAct(string T_here_Action, string T_not_here_Action)
    {
        StartCoroutine(Check(T_here_Action, T_not_here_Action));
    }


    protected IEnumerator Check(string T_here_Action, string T_not_here_Action)
    {
        if (!Scholar.Senses.T_here)
        {
            CheckForTeacher();
            yield return new WaitForEndOfFrame();

            while (checking)
            {
                yield return new WaitForEndOfFrame();
            }

            if (Scholar.Senses.T_here)
            {
                Scholar.Action.Action.Doing(T_here_Action);
            }
            else
            {
                Scholar.Action.Doing(T_not_here_Action);
            }
        }
        else
        {
            Scholar.Action.Doing(T_here_Action);
        }
    }



    protected IEnumerator Check_For_Teacher()
    {
        Quaternion target = Quaternion.Euler(GetRotation().eulerAngles.x, GetRotation().eulerAngles.y + 120, GetRotation().eulerAngles.z);
        Watch(target);

        while (!SightIsHere() && !Scholar.Senses.T_here)
        {
            yield return new WaitForEndOfFrame();
        }

        target = Quaternion.Euler(GetRotation().eulerAngles.x, GetRotation().eulerAngles.y + 120, GetRotation().eulerAngles.z);
        Watch(target);

        while (!SightIsHere() && !Scholar.Senses.T_here)
        {
            yield return new WaitForEndOfFrame();
        }

        target = Quaternion.Euler(GetRotation().eulerAngles.x, GetRotation().eulerAngles.y + 120, GetRotation().eulerAngles.z);
        Watch(target);

        while (!SightIsHere() && !Scholar.Senses.T_here)
        {
            yield return new WaitForEndOfFrame();
        }

        if (Scholar.Senses.T_here)
            Watch(Player.get.transform.position);

        checking = false;
    }


}
