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
    private NavMeshAgent NavAgent;
    private ScholarManager ScholarMan;


    [HideInInspector]
    public bool ready;
    [HideInInspector]
    public bool doing;
    private bool watching;
    private bool complete_before_end;
    private bool ready_for_cheat;
    [HideInInspector]
    public string cheat_string;
    private int actionNo;
    [HideInInspector]
    public string keyAction;
    [HideInInspector]
    public string keyAction_now;
    private const string anim = "AnimNumber";


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
        ScholarMan = GameObject.FindObjectOfType<ScholarManager>();
    }


    private void Start()
    {
        Anim.SetInteger(anim, animations["Nothing"]);
        home = desk + Vector3.back;
    }




    //=========================================================================================================================================================
    //Начать действие

    public void Doing(string key)
    {

        Stop();
        Debug.Log("Я начал делать" + key);
        doing = true;
        actionNo = 0;
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
        actionNo = 0;
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
        complete_before_end = false;
        StartCoroutine(key);
        keyAction = key;
        keyAction_now = key;
    }



    //=========================================================================================================================================================
    //Начать писать

    public void StartWriting()
    {
        if (!Scholar.executed && Scholar.isLiving)
        {
            Stop();
            Debug.Log("Я начал делать Writing");
            keyAction = "Writing";
            keyAction_now = "Writing";
            actionNo = 0;
            complete_before_end = false;
            StartCoroutine(keyAction);
        }
    }

    //=========================================================================================================================================================
    //Остановиться

    public void Stop()
    {
        if (keyAction != null)
            StopCoroutine(keyAction);

        if (complete_before_end)
            keyAction = "Writing";

        if (Scholar.cheating)
        {
            ScholarMan.cheating_count--;
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
            if (keyAction == "Writing")
            {
                StartWriting();
            }
            else
            {
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
            if (ScholarMan.Probability((Scholar.stress / 100) + 0.25))
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
    //Вероятность



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

    private IEnumerator Watching(Vector3 target)
    {
        float buf = 2f;
        while (buf > 0)
        {
            SightTo(target);
            buf -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void SightTo(Vector3 target)
    {
        Quaternion targetRotation = GetQuaternionTo(target);
        targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime);
        transform.rotation = targetRotation;
    }



    private Quaternion GetQuaternionTo(Vector3 target)
    {
        Vector3 direct = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direct);
        targetRotation.z = transform.rotation.z;
        targetRotation.x = transform.rotation.x;
        return targetRotation;
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
        if (actionNo == 0)
        {
            if (!Scholar.asking)
            {
                Scholar.Question("Question_Permission_1");
            }

            while (Scholar.asking || Scholar.talking)
            {
                yield return new WaitForEndOfFrame();
            }

            if (Scholar.teacher_answer)
            {
                actionNo++;
            }
            else
            {
                StartWriting();
            }
        }

        if (actionNo == 1)
        {
            if (!Scholar.asking)
            {
                Scholar.Question("Question_Toilet_1");
            }

            while (Scholar.asking || Scholar.asking || Scholar.talking)
            {
                yield return new WaitForEndOfFrame();
            }

            if (Scholar.teacher_answer)
            {
                actionNo++;
            }
            else
            {
                StartWriting();
            }
        }

        if (actionNo == 2)
        {
            SetDestination(ScholarMan.GetPlace("toilet",0));

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            Watch(ScholarMan.GetSightGoal("toilet", 0));

            Debug.Log("Я дошел");
            actionNo++;
        }

        if (actionNo == 3)
        {
            yield return new WaitForSeconds(5f);
            Debug.Log("Сделал свои дела");
            actionNo++;
        }

        if (actionNo == 4)
        {
            SetDestination(home);

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            actionNo++;
        }

        if (actionNo == 5)
        {
            doing = false;
            StartWriting();
        }
    }




    //=========================================================================================================================================================
    //Выход в туалет

    private IEnumerator Sink_1()
    {

        if (actionNo == 0)
        {
            if (!Scholar.asking)
            {
                Scholar.Question("Question_Sink_1");
            }

            while (Scholar.asking || Scholar.asking || Scholar.talking)
            {
                yield return new WaitForEndOfFrame();
            }

            if (Scholar.teacher_answer)
            {
                actionNo++;
            }
            else
            {
                StartWriting();
            }
        }

        if (actionNo == 1)
        {
            SetDestination(ScholarMan.GetPlace("sink", 0));

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            Watch(ScholarMan.GetSightGoal("sink", 0));

            Debug.Log("Я дошел");
            actionNo++;
        }

        if (actionNo == 2)
        {
            yield return new WaitForSeconds(5f);
            Debug.Log("Сделал свои дела");
            actionNo++;
        }

        if (actionNo == 3)
        {
            SetDestination(home);

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            actionNo++;
        }

        if (actionNo == 4)
        {
            doing = false;
            StartWriting();
        }
    }



    //=========================================================================================================================================================
    //Выход подышать воздухом

    private IEnumerator Air_1()
    {
        if (actionNo == 0)
        {
            if (!Scholar.asking)
            {
                Scholar.Question("Question_Air_1");
            }

            while (Scholar.asking || Scholar.talking)
            {
                yield return new WaitForEndOfFrame();
            }

            if (Scholar.teacher_answer)
            {
                actionNo++;
            }
            else
            {
                StartWriting();
            }
        }


        if (actionNo == 1)
        {
            SetDestination(ScholarMan.GetPlace("outside", 0));

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            Watch(ScholarMan.GetSightGoal("outside", 0));

            Debug.Log("Я дошел");
            actionNo++;
        }

        if (actionNo == 2)
        {
            yield return new WaitForSeconds(5f);
            Debug.Log("Сделал свои дела");
            actionNo++;
        }

        if (actionNo == 3)
        {
            SetDestination(home);

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            actionNo++;
        }

        if (actionNo == 4)
        {
            doing = false;
            StartWriting();
        }
    }




    //=========================================================================================================================================================
    //Списывание

    private IEnumerator Cheating_1()
    {
        ScholarMan.special_actions_count++;
        Scholar.cheating = true;
        complete_before_end = true;

        Anim.SetInteger(anim, animations["Cheating"]);

        yield return new WaitForSeconds(5f);
        Debug.Log("Сделал свои дела");

        doing = false;
        Scholar.cheatNeed = false;
        Scholar.cheating = false;

        ScholarMan.cheating_count--;
        StartWriting();
    }


    private IEnumerator Cheating_Check_1()
    {
        ready_for_cheat = true;
        complete_before_end = true;

        Anim.SetInteger(anim, animations["Cheating"]);
        Scholar.SayThoughts("I WANT CHEAT");

        yield return new WaitForSeconds(5f);

        doing = false;

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

        ready = true;
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

    private IEnumerator Delay_Start()
    {
        yield return new WaitForSeconds(1f);
        StartWriting();
    }
}

