using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System;

public class ActionsScholar : MonoBehaviour
{

    private Animator Anim;
    private Scholar Scholar;
    [HideInInspector]
    public NavMeshAgent NavAgent;



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
    [SerializeField]
    private int actionNo;
    [SerializeField]
    private int actionNoPlus;
    [HideInInspector]
    public string keyAction;
    [HideInInspector]
    public string keyAction_now;
    private const string anim = "AnimNumber";


    //Списывание
    private bool ready_for_cheat;
    [HideInInspector]
    public string cheat_string;

    //Дополительные переменные


    private Vector3 destination;
    [HideInInspector]
    public Vector3 home;
    [HideInInspector]
    public Vector3 desk;


    private Dictionary<string, int> animations = new Dictionary<string, int>()
    {
        { "Nothing", 0},
        { "Walking", 1},
        { "Writing", 2},
        { "Cheating", 3},
    };


    private void Awake()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        Anim = transform.Find("Model").GetComponent<Animator>();
        Scholar = transform.GetComponentInChildren<Scholar>();
        keyAction = null;
    }


    private void Start()
    {
        Anim.SetInteger(anim, animations["Nothing"]);
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
        actionNo = 0;
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
        actionNo = 0;
        actionNoPlus = 0;
        complete_before_end = false;
        StartCoroutine(key,i);
        keyAction = key;
        keyAction_now = key;
    }

    public void SimpleDoing(string key)
    {
        Stop();
        Debug.Log("Я начал делать" + key);
        actionNo = 0;
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
            //Debug.Log("Я начал делать Writing");
            keyAction = "Writing";
            keyAction_now = "Writing";
            complete_before_end = false;
            StartCoroutine(keyAction);
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
            Scholar.cheatNeed = false;
        }

        SetDestination(transform.position);
        Anim.SetInteger(anim, animations["Nothing"]);
        Scholar.writing = false;
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

        //Debug.Log("Я думаю");
        Anim.SetInteger(anim, animations["Nothing"]);

        yield return new WaitForSeconds(UnityEngine.Random.Range(4, 5));
        //Debug.Log("Я пишу");

        Anim.SetInteger(anim, animations["Writing"]);

        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 7));

        WhatToDoNext();
    }


    private void WhatToDoNext()
    {
        if (Scholar.cheatNeed)
        {
            Scholar.Agent.CanCheat();
        }
        else
        {
            if (ScholarManager.get.Probability((Scholar.stress / 100) + 0.25))
            {
                StartWriting();
            }
            else
            {
                Scholar.Agent.RandomSimpleAction();
            }
        }
    }



    //=========================================================================================================================================================
    //Перемещение и повороты


    private void SetDestination(Vector3 goal)
    {
        destination = new Vector3(goal.x, transform.position.y, goal.z);
        NavAgent.SetDestination(destination);
        Anim.SetInteger(anim, animations["Walking"]);
        Scholar.walking = true;
    }


    private bool IsHere()
    {
        if ((transform.position - destination).magnitude <= 0.001)
        {
            Anim.SetInteger(anim, animations["Nothing"]);
            Scholar.walking = false;
            return true;
        }
        else
            return false;
    }

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
        Quaternion target = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + angle, transform.rotation.eulerAngles.z);

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
        Quaternion targetRotation = GetQuaternionTo(target);
        targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime);
        transform.rotation = targetRotation;
    }

    public void SightTo(Quaternion target)
    {
        target = Quaternion.Slerp(transform.rotation, target, 3f * Time.deltaTime);
        transform.rotation = target;
    }

    public Quaternion GetQuaternionTo(Vector3 target)
    {
        Vector3 direct = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direct);
        targetRotation.z = transform.rotation.z;
        targetRotation.x = transform.rotation.x;
        return targetRotation;
    }


    private IEnumerator LookingForTeacher()
    {
        watching = true;
        float buf = 2f;
        Quaternion target = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 120, transform.rotation.eulerAngles.z);

        while (buf > 0 && !Scholar.T_here)
        {
            SightTo(target);
            buf -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        buf = 2f;
        target = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 120, transform.rotation.eulerAngles.z);

        while (buf > 0 && !Scholar.T_here)
        {
            SightTo(target);
            buf -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        buf = 2f;
        target = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 120, transform.rotation.eulerAngles.z);

        while (buf > 0 && !Scholar.T_here)
        {
            SightTo(target);
            buf -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        watching = false;
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
        Debug.Log(actionNo == actionNoPlus);
        if(actionNo == actionNoPlus)
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


    public float GetHearDistance(Vector3 goal)
    {
        NavMeshPath path = new NavMeshPath();

        NavAgent.CalculatePath(goal, path);

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = goal;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float buf = 0f;

        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            buf += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return buf;
    }


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
            if ((Scholar.asking || Scholar.talking) && (question_t > 0 || Scholar.T_look_at_us))
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
            if (!Scholar.T_here)
            {

                StartCoroutine(LookingForTeacher());
                yield return new WaitForEndOfFrame();
                while (watching)
                {
                    yield return new WaitForEndOfFrame();
                }

                if (Scholar.T_here)
                {
                    actionNo++;
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
                actionNo++;
            }
        }

        if (CanIContinue())
        {

            while (Question("Question_Permission_1"))
            {
                yield return new WaitForEndOfFrame();
            }

            if (!Scholar.asking)
            {
                if (Scholar.teacher_answer)
                {
                    actionNo++;
                }
                else
                {
                    StartWriting();
                }
            }
            else
            {
                StopQuestion();
            }
        }

        if (CanIContinue())
        {

            while (Question("Question_Toilet_1"))
            {
                yield return new WaitForEndOfFrame();
            }

            if (!Scholar.asking)
            {
                if (Scholar.teacher_answer)
                {
                    actionNo++;
                }
                else
                {
                    StartWriting();
                }
            }
            else
            {
                StopQuestion();
            }
        }

        if (CanIContinue())
        {
            Debug.Log("пошел");
            SetDestination(ScholarManager.get.GetPlace("toilet",0));

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            Watch(ScholarManager.get.GetSightGoal("toilet", 0));

            Debug.Log("Я дошел");
            actionNo++;
        }

        if (CanIContinue())
        {
            yield return new WaitForSeconds(5f);
            Debug.Log("Сделал свои дела");
            actionNo++;
        }

        if (CanIContinue())
        {
            SetDestination(home);

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            actionNo++;
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
            if (!Scholar.T_here)
            {

                StartCoroutine(LookingForTeacher());
                yield return new WaitForEndOfFrame();
                while (watching)
                {
                    yield return new WaitForEndOfFrame();
                }

                if (Scholar.T_here)
                {
                    actionNo++;
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
                actionNo++;
            }
        }


        if (CanIContinue())
        {

            while (Question("Question_Sink_1"))
            {
                yield return new WaitForEndOfFrame();
            }

            if (!Scholar.asking)
            {
                if (Scholar.teacher_answer)
                {
                    actionNo++;
                }
                else
                {
                    StartWriting();
                }
            }
            else
            {
                StopQuestion();
            }
        }

        if (CanIContinue())
        {
            Debug.Log("пошел");
            SetDestination(ScholarManager.get.GetPlace("sink", 0));

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            Watch(ScholarManager.get.GetSightGoal("sink", 0));

            Debug.Log("Я дошел");
            actionNo++;
        }

        if (CanIContinue())
        {
            yield return new WaitForSeconds(5f);
            Debug.Log("Сделал свои дела");
            actionNo++;
        }

        if (CanIContinue())
        {
            SetDestination(home);

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            actionNo++;
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
            if (!Scholar.T_here)
            {

                StartCoroutine(LookingForTeacher());
                yield return new WaitForEndOfFrame();
                while (watching)
                {
                    yield return new WaitForEndOfFrame();
                }

                if (Scholar.T_here)
                {
                    actionNo++;
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
                actionNo++;
            }
        }


        if (CanIContinue())
        {
            while (Question("Question_Air_1"))
            {
                yield return new WaitForEndOfFrame();
            }

            if (!Scholar.asking)
            {
                if (Scholar.teacher_answer)
                {
                    actionNo++;
                }
                else
                {
                    StartWriting();
                }
            }
            else
            {
                StopQuestion();
            }
        }


        if (CanIContinue())
        {
            Debug.Log("пошел");
            SetDestination(ScholarManager.get.GetPlace("outside", 0));

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            Watch(ScholarManager.get.GetSightGoal("outside", 0));

            actionNo++;
        }

        if (CanIContinue())
        {
            yield return new WaitForSeconds(5f);
            Debug.Log("Сделал свои дела");
            actionNo++;
        }

        if (CanIContinue())
        {
            SetDestination(home);

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            actionNo++;
        }


        if (CanIContinue())
        {
            doing = false;
            StartWriting();
        }
    }




    //=========================================================================================================================================================
    //Списывание

    private IEnumerator Cheating_1()
    {

        ScholarManager.get.special_actions_count++;
        Scholar.cheating = true;
        complete_before_end = true;

        Anim.SetInteger(anim, animations["Cheating"]);
        yield return new WaitForSeconds(5f);
        Debug.Log("Сделал свои дела");

        doing = false;
        Scholar.cheatNeed = false;
        Scholar.cheating = false;

        ScholarManager.get.cheating_count--;
        StartWriting();
    }



    private IEnumerator Cheating_Check_1()
    {
        ready_for_cheat = true;
        complete_before_end = true;

        Scholar.SayThoughts("I WANT CHEAT");


        if (!Scholar.T_here)
        {
            StartCoroutine(LookingForTeacher());
            yield return new WaitForEndOfFrame();
            while (watching)
            {
                yield return new WaitForEndOfFrame();
            }


            doing = false;

            if (Scholar.T_here)
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
    //Догадывание

    private IEnumerator Guesses()
    {
        complete_before_end = true;

        Anim.SetInteger(anim, animations["Guesses"]);

        yield return new WaitForSeconds(5f);


        Anim.SetInteger(anim, animations["HasGuessed"]);

        Debug.Log("Я догодалася");

        yield return new WaitForSeconds(1f);

        keyAction = null;
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
        Debug.Log(actionNo);
        Debug.Log(actionNoPlus);
        yield return new WaitForSeconds(1f);
        StartWriting();
    }
}

