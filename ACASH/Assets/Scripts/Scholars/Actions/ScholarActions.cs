using System.Collections;
using UnityEngine;

public class ScholarActions : MonoBehaviour
{

    private Scholar Scholar;



    [HideInInspector]
    public bool doing;
    [HideInInspector]
    public bool can_i_do_smth_else;
    private float doing_t;
    private const float doing_const_t = 10f;
    private float question_t;
    private const float question_const_t = 10f;
    private bool q_bool;
    private bool watching;
    private bool complete_before_end;
    private bool answering;
    private int actionNum;
    private int actionNoPlus;
    [HideInInspector]
    public string keyAction;
    [HideInInspector]
    public string keyAction_now;


    //Списывание
    private bool ready_for_cheat;
    [HideInInspector]
    public string cheat_string;

    //Дополительные переменные



    [HideInInspector]
    public Vector3 home;
    [HideInInspector]
    public Vector3 desk;




    private void Awake()
    {
        Scholar = GetComponent<Scholar>();
        keyAction = null;
    }


    private void Start()
    {
        question_t = question_const_t;
        doing_t = doing_const_t;
        q_bool = true;
    }

    private void Update()
    {
        CanIDoSomethingElse();
    }


    //=========================================================================================================================================================
    //Начать действие

    public void Doing(string key)
    {

        Stop();
        Debug.Log("Я начал делать" + key);
        doing = true;
        can_i_do_smth_else = false;
        actionNum = 0;
        actionNoPlus = 0;
        complete_before_end = false;
        StartCoroutine(key);
        keyAction = key;
        keyAction_now = key;
    }

    public void Doing(string key, int i)
    {

        Stop();
        Debug.Log("Я начал делать" + key);
        doing = true;
        can_i_do_smth_else = false;
        actionNum = 0;
        actionNoPlus = 0;
        complete_before_end = false;
        StartCoroutine(key, i);
        keyAction = key;
        keyAction_now = key;
    }

    public void SimpleDoing(string key)
    {
        Stop();
        Debug.Log("Я начал делать" + key);
        actionNum = 0;
        actionNoPlus = 0;
        complete_before_end = false;
        StartCoroutine(key);
        keyAction = key;
        keyAction_now = key;
    }



    //=========================================================================================================================================================
    //Специальные действия

    public void StartWriting()
    {
        if (!Scholar.executed && Scholar.isLiving)
        {
            Stop();
            Debug.Log("Я начал делать Writing");
            keyAction = "Writing";
            keyAction_now = "Writing";
            complete_before_end = false;
            StartCoroutine(Writing());
        }
    }

    public void StopCheating()
    {
        if (!Scholar.executed && Scholar.isLiving)
        {
            //Прописать остановку читерства
        }
    }

    //=========================================================================================================================================================
    //Остановиться

    public void Stop()
    {
        StopAllCoroutines();

        /*if (keyAction != null)
            StopCoroutine(keyAction);
            */

        if (complete_before_end)
            keyAction = "Writing";

        if (Scholar.cheating)
        {
            ScholarManager.get.cheating_count--;
            Scholar.cheating = false;
            Scholar.Cheat.cheatNeed = false;
        }

        Scholar.writing = false;
        Scholar.Move.Stop();
        keyAction_now = "Nothing";
        doing = false;
    }



    //=========================================================================================================================================================
    //Продолжить незаконченное действие

    public void Continue()
    {
        if (keyAction != null)
        {
            Debug.Log(keyAction);
            if (keyAction == "Writing")
            {
                StartWriting();
            }
            else
            {
                actionNoPlus = 0;
                StartCoroutine(keyAction);
                keyAction_now = keyAction;
                doing = true;
            }
        }
    }



    //=========================================================================================================================================================
    // Основной действие школьника - написание экзамена.

    private IEnumerator Writing()
    {
        SetDestination(home);

        while (!IsHere())
            yield return new WaitForEndOfFrame();

        Watch(desk);

        Scholar.writing = true;

        Debug.Log("Я думаю");
        Scholar.Anim.SetAnimation("Nothing");

        yield return new WaitForSeconds(UnityEngine.Random.Range(4, 5));
        Debug.Log("Я пишу");
        Scholar.Anim.SetAnimation("Writing");

        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 7));

        WhatToDoNext();
    }


    private void WhatToDoNext()
    {
        /*
        if (Scholar.Cheat.cheatNeed)
        {
            Scholar.Agent.CanCheat();
        }
        else
        {
            if (BaseMath.Probability((Scholar.Stress.value / 100) + 0.25))
            {
                StartWriting();
            }
            else
            {
                Scholar.Agent.RandomSimpleAction();
            }
        }
        */
    }



    //=========================================================================================================================================================
    //Перемещение и повороты




    //=========================================================================================================================================================
    //Зрение


    private void Watch(Vector3 target)
    {
        StartCoroutine(Watching(target));
    }

    private void Watch(float angle)
    {
        StartCoroutine(Watching(angle));
    }

    private IEnumerator Watching(Vector3 target)
    {
        watching = true;
        float buf = 2f;
        while (buf > 0)
        {
            SightTo(target);
            buf -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        watching = false;
    }

    private IEnumerator Watching(float angle)
    {
        watching = true;
        float buf = 2f;
        Quaternion target = Quaternion.Euler(GetRotation().eulerAngles.x, GetRotation().eulerAngles.y + angle, GetRotation().eulerAngles.z);

        while (buf > 0)
        {
            SightTo(target);
            buf -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        watching = false;
    }

    public void SpecialWatch(Vector3 target)
    {
        Stop();
        StartCoroutine(SpecialWatching(target));
    }

    private IEnumerator SpecialWatching(Vector3 target)
    {
        StartCoroutine(Watching(target));
        yield return new WaitForSeconds(2f);
        Continue();
    }

    public void SightTo(Vector3 target)
    {
        Quaternion targetRotation = BaseGeometry.GetQuaternionTo(Scholar.Move.transform, target);
        targetRotation = Quaternion.Slerp(GetRotation(), targetRotation, 3f * Time.deltaTime);
        Scholar.Move.Rotation(targetRotation);
    }

    public void SightTo(Quaternion target)
    {
        target = Quaternion.Slerp(GetRotation(), target, 3f * Time.deltaTime);
        Scholar.Move.Rotation(target);
    }




    /* public bool WatchBool(Vector3 target)
     {
         Quaternion targetRotation = GetQuaternionTo(target);
         targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime);
         transform.rotation = targetRotation;

         if (transform.rotation == targetRotation)
         {
             Debug.Log("O daaa");
             return true;
         }
         else
             return false;
     }

     */




    //=========================================================================================================================================================
    //Дополнительные функции

    private bool CanIContinue()
    {
        Debug.Log(actionNum == actionNoPlus);
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


    private void CanIDoSomethingElse()
    {
        if (!can_i_do_smth_else && !doing)
        {
            if (doing_t > 0)
            {
                doing_t -= Time.deltaTime;
            }
            else
            {
                //Debug.Log("Я могу опять что-то делать!");
                can_i_do_smth_else = true;
                doing_t = doing_const_t;
            }
        }
        else
        {
            doing_t = doing_const_t;
        }
    }


    private void SetDestination(Vector3 goal)
    {
        Scholar.Move.SetDestination(goal);
    }

    private bool IsHere()
    {
        return Scholar.Move.IsHere();
    }

    private Quaternion GetRotation()
    {
        return Scholar.Move.Rotation();
    }

    private Vector3 GetPosition()
    {
        return Scholar.Move.Position();
    }


    //=========================================================================================================================================================
    //Вопрос

    private bool Question(string key)
    {
        if (q_bool)
        {
            if (!Scholar.asking)
            {
                Scholar.Question(key);
                q_bool = false;
            }
            return true;
        }
        else
        {
            if ((Scholar.asking || Scholar.talking) && (question_t > 0 || Scholar.Senses.T_look_at_us))
            {
                SightTo(Player.get.transform.position);
                question_t -= Time.deltaTime;
                return true;
            }
            else
            {
                Debug.Log("Конец вопроса");
                question_t = question_const_t;
                q_bool = true;
                return false;
            }
        }
    }

    private void StopQuestion()
    {
        Scholar.Agent.StopQuestion();
    }

    public void QuestionBeforeAct(string question, string ActionYes, string ActionNo)
    {
        StartCoroutine(Question(question, ActionYes, ActionNo));
    }

    public void QuestionBeforeAct(string question, string ActionYes)
    {
        StartCoroutine(Question(question, ActionYes, "Writing"));
    }
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Список всех действий доступных ученикам.
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================



    //=========================================================================================================================================================
    // Вопрос

    private IEnumerator Question(string question, string ActionYes, string ActionNo)
    {
        if (CanIContinue())
        {
            if (!Scholar.Senses.T_here)
            {
                CheckForTeacher();
                yield return new WaitForEndOfFrame();
                while (watching)
                {
                    yield return new WaitForEndOfFrame();
                }

                if (Scholar.Senses.T_here)
                {
                    actionNum++;
                    Scholar.Emotions.ChangeEmotion("suprised", "happy", 1f);
                    yield return new WaitForSeconds(0.7f);
                }
                else
                {
                    Scholar.Emotions.ChangeEmotion("sad", "ussual", 2f);
                    StartWriting();
                }
            }
            else
            {
                actionNum++;
            }
        }

        if (CanIContinue())
        {

            while (Question(question))
            {
                yield return new WaitForEndOfFrame();
            }

            if (!Scholar.asking)
            {
                if (Scholar.teacher_answer)
                {
                    Doing(ActionYes);
                }
                else
                {
                    Doing(ActionNo);
                }
            }
            else
            {
                Doing(ActionNo);
            }
        }
    }



    //=========================================================================================================================================================
    // Поиск учителя

    private void CheckForTeacher()
    {
        Scholar.Agent.CheckForTeacher();
    }

    public void CheckBeforeAct(string T_here_Action, string T_not_here_Action)
    {
        StartCoroutine(Check(T_here_Action, T_not_here_Action));
    }


    private IEnumerator Check(string T_here_Action, string T_not_here_Action)
    {
        if (!Scholar.Senses.T_here)
        {
            CheckForTeacher();
            yield return new WaitForEndOfFrame();
            while (watching)
            {
                yield return new WaitForEndOfFrame();
            }

            if (Scholar.Senses.T_here)
            {
                Doing(T_here_Action);
            }
            else
            {
                Doing(T_not_here_Action);
            }
        }
        else
        {
            Doing(T_here_Action);
        }
    }
    

    //=========================================================================================================================================================
    // Проверка тупицы на учителя

    private IEnumerator Dumb_Check_For_Teacher()
    {
        watching = true;
        float buf = 2f;
        Quaternion target = Quaternion.Euler(GetRotation().eulerAngles.x, GetRotation().eulerAngles.y + 120, GetRotation().eulerAngles.z);

        while (buf > 0 && !Scholar.Senses.T_here)
        {
            SightTo(target);
            buf -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        buf = 2f;
        target = Quaternion.Euler(GetRotation().eulerAngles.x, GetRotation().eulerAngles.y + 120, GetRotation().eulerAngles.z);

        while (buf > 0 && !Scholar.Senses.T_here)
        {
            SightTo(target);
            buf -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        buf = 2f;
        target = Quaternion.Euler(GetRotation().eulerAngles.x, GetRotation().eulerAngles.y + 120, GetRotation().eulerAngles.z);

        while (buf > 0 && !Scholar.Senses.T_here)
        {
            SightTo(target);
            buf -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        watching = false;
    }




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
            SetDestination(home);

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            actionNum++;
        }

        if (CanIContinue())
        {
            doing = false;
            StartWriting();
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
            SetDestination(home);

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            actionNum++;
        }

        if (CanIContinue())
        {
            doing = false;
            StartWriting();
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
            SetDestination(home);

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            actionNum++;
        }


        if (CanIContinue())
        {
            doing = false;
            StartWriting();
        }
    }




    //=========================================================================================================================================================
    //Думание вслух

    private IEnumerator Think_Aloud_1()
    {
        Scholar.Anim.SetAnimation("Think_Aloud");
        Scholar.SayThoughts("Think_Aloud_1");
        yield return new WaitForSeconds(5f);

        keyAction = null;
        StartWriting();
    }




    //=========================================================================================================================================================
    //Догадывание

    private IEnumerator Guesses_1()
    {
        complete_before_end = true;

        Scholar.Anim.SetAnimation("Guesses");

        yield return new WaitForSeconds(5f);


        Scholar.Anim.SetAnimation("HasGuessed");

        Debug.Log("Я догодалася");

        yield return new WaitForSeconds(1f);

        keyAction = null;
        StartWriting();
    }




    //=========================================================================================================================================================
    //Списывание

    private IEnumerator Cheating_1()
    {

        Scholar.cheating = true;
        complete_before_end = true;

        Scholar.Anim.SetAnimation("Cheating");
        yield return new WaitForSeconds(5f);
        Debug.Log("Сделал свои дела");

        doing = false;
        Scholar.Cheat.cheatNeed = false;
        Scholar.cheating = false;

        ScholarManager.get.cheating_count--;
        StartWriting();
    }



    private IEnumerator Cheating_Check_1()
    {
        ready_for_cheat = true;
        complete_before_end = true;

        Scholar.SayThoughts("I WANT CHEAT");


        if (!Scholar.Senses.T_here)
        {
            CheckForTeacher();
            yield return new WaitForEndOfFrame();
            while (watching)
            {
                yield return new WaitForEndOfFrame();
            }


            doing = false;

            if (Scholar.Senses.T_here)
            {
                ready_for_cheat = false;
                Debug.Log("Нихуя");
                Scholar.Emotions.ChangeEmotion("suprised", "upset", 1f);
                yield return new WaitForSeconds(0.7f);
            }
        }
        else
        {
            ready_for_cheat = false;
        }

        CheatingContinue();
    }



    private void CheatingContinue()
    {
        if (ready_for_cheat)
            Doing(cheat_string);
        else
            StartWriting();
    }



    //=========================================================================================================================================================
    //Домой



    private IEnumerator Go_Home()
    {
        SetDestination(home);

        while (!IsHere())
            yield return new WaitForEndOfFrame();

        Watch(desk);

        keyAction = null;
        doing = false;
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
    //Отложенный старт

    private IEnumerator Delay_Start()
    {
        yield return new WaitForSeconds(1f);
        StartWriting();
    }
}

