using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System;

public class ActionsScholar : MonoBehaviour
{
    private NavMeshAgent NavAgent;
    private Scholar Scholar;
    private bool doing;
    private bool watching;
    [HideInInspector]
    public string keyWord;
    private int actionNo;
    public Transform toilet;
    private Vector3 destination;
    private Vector3 home;
    [HideInInspector]
    public Vector3 desk;
    private Animator Anim;
    private bool complete_before_end;
    private Dictionary<string, int> animations = new Dictionary<string, int>()
    {
        { "Nothing", 0},
        { "Walking", 1},
        { "Thinking", 2},
    };


    private void Awake()
    {
        home = transform.position;
        desk = home + Vector3.back;
        NavAgent = GetComponent<NavMeshAgent>();
        Scholar = transform.GetComponentInChildren<Scholar>();
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
        keyWord = key;

    }



    //=========================================================================================================================================================
    //Начать писать

    public void StartWriting()
    {
        Stop();
        Debug.Log("Я начал делать Writing");
        keyWord = "Writing";
        actionNo = 0;
        complete_before_end = false;
        StartCoroutine(keyWord);
    }



    //=========================================================================================================================================================
    //Остановиться

    public void Stop()
    {
        if(keyWord != null)
            StopCoroutine(keyWord);



        if (complete_before_end)
            keyWord = "Writing";

        SetDestination(transform.position);
        Scholar.writing = false;
        doing = false;
    }



    //=========================================================================================================================================================
    //Продолжить незаконченное действие

    public void Continue()
    {
        if (keyWord != null)
        {
            if (keyWord == "Writing")
            {
                StartWriting();
            }
            else
            {
                StartCoroutine(keyWord);
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
            yield return new WaitForSeconds(1f);

        while (WatchBool(desk))
            yield return new WaitForEndOfFrame();

        Scholar.writing = true;

        Anim.SetInteger("AnimNom", animations["Thinking"]);

        yield return new WaitForSeconds(UnityEngine.Random.Range(4, 15));

        Anim.SetInteger("AnimNom", animations["Writing"]);

        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 7));

        if (Scholar.cheatNeed)
		{
            Scholar.Agent.CanCheat();
        }
        else
        {
            if (Probability(Scholar.stress/100))
            {
                Scholar.Agent.RandomAction();
            }
            else
            {
                StartWriting();
            }
        }
    }



    //=========================================================================================================================================================
    //Вероятность

    public bool Probability(double a)
    {
        float rnd = UnityEngine.Random.value;

        if (a >= rnd)
            return true;
        else
            return false;
    }



    //=========================================================================================================================================================
    //Перемещение и повороты

    private void SetDestination(Vector3 goal)
    {
        destination = new Vector3(goal.x, transform.position.y, goal.z);
        NavAgent.SetDestination(destination);
    }


    private bool IsHere()
    {
        if ((transform.position - destination).magnitude <= 0.001)
            return true;
        else
            return false;
    }


    public void Watch(Vector3 target)
    {
        Vector3 direct = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direct);
        targetRotation.z = transform.rotation.z;
        targetRotation.x = transform.rotation.x;
        targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime);
        transform.rotation = targetRotation;
    }


    public bool WatchBool(Vector3 target)
    {
        Vector3 direct = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direct);
        targetRotation.z = transform.rotation.z;
        targetRotation.x = transform.rotation.x;
        targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime);
        transform.rotation = targetRotation;

        if (targetRotation == transform.rotation)
            return true;
        else
            return false;
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
        if (actionNo == 0 && !Scholar.question)
        {

            Scholar.Question("Question_Toilet_1");

            Debug.Log("Я спросил");
        }

        while (Scholar.question || Scholar.asking || Scholar.talking)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Я жду разрешения");
        }

        if (Scholar.teacher_answer)
        {
            actionNo++;
        }

        if (actionNo == 1)
        {
            SetDestination(toilet.position);

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            while (WatchBool(toilet.position + Vector3.forward))
            {
                yield return new WaitForEndOfFrame();
            }

            Debug.Log("Я дошел");
            actionNo++;
        }

        if (actionNo == 2)
        {
            yield return new WaitForSeconds(5f);
            Debug.Log("Я подождал");
            actionNo++;
        }

        if (actionNo == 3)
        {
            SetDestination(home);
            Debug.Log("Я пошел домой");

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            Debug.Log("Я дома");
            actionNo++;
        }
        keyWord = null;
        doing = false;
        StartWriting();
    }



    //=========================================================================================================================================================
    //Догадывание

    private IEnumerator Guesses()
    {
        complete_before_end = true;

        Anim.SetInteger("AnimNom", animations["Guesses"]);

        yield return new WaitForSeconds(5f);


        Anim.SetInteger("AnimNom", animations["HasGuessed"]);

        Debug.Log("Я догодалася");

        yield return new WaitForSeconds(1f);

        keyWord = null;
        doing = false;
        StartWriting();
    }
}

