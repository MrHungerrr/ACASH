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


    private bool doing;
    private bool watching;
    private bool complete_before_end;
    private int actionNo;
    [HideInInspector]
    public string keyAction;
    public string keyAction_now;


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
        Anim.SetInteger("AnimNom", animations["Nothing"]);
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



    //=========================================================================================================================================================
    //Начать писать

    public void StartWriting()
    {
        Stop();
        Debug.Log("Я начал делать Writing");
        keyAction = "Writing";
        keyAction_now = "Writing";
        actionNo = 0;
        complete_before_end = false;
        StartCoroutine(keyAction);
    }



    //=========================================================================================================================================================
    //Остановиться

    public void Stop()
    {
        StopCoroutine(keyAction);

        if (complete_before_end)
            keyAction = "Writing";

        SetDestination(transform.position);
        Anim.SetInteger("AnimNom", animations["Nothing"]);
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

        Debug.Log("Я думаю");
        Anim.SetInteger("AnimNom", animations["Nothing"]);

        yield return new WaitForSeconds(UnityEngine.Random.Range(4, 5));
        Debug.Log("Я пишу");

        Anim.SetInteger("AnimNom", animations["Writing"]);

        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 7));


        StartWriting();
       /* if (Scholar.cheatNeed)
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
        */
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
        Anim.SetInteger("AnimNom", animations["Walking"]);
    }


    private bool IsHere()
    {
        if ((transform.position - destination).magnitude <= 0.001)
        {
            Anim.SetInteger("AnimNom", animations["Nothing"]);
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


    public bool WatchBool(Vector3 target)
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

    private Quaternion GetQuaternionTo(Vector3 target)
    {
        Vector3 direct = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direct);
        targetRotation.z = transform.rotation.z;
        targetRotation.x = transform.rotation.x;
        return targetRotation;
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
        if (actionNo == 0)
        {
            if (!Scholar.asking)
            {
                Scholar.Question("Question_Permission_1");

                Debug.Log("Я спросил");
            }

            while (Scholar.asking || Scholar.talking)
            {
                yield return new WaitForEndOfFrame();
                Debug.Log("Я жду разрешения");
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

                Debug.Log("Я спросил");
            }

            while (Scholar.asking || Scholar.asking || Scholar.talking)
            {
                yield return new WaitForSeconds(1f);
                Debug.Log("Я жду разрешения");
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
            Debug.Log("Я подождал");
            actionNo++;
        }

        if (actionNo == 4)
        {
            StartWriting();
         /*   SetDestination(home);
            Debug.Log("Я пошел домой");

            while (!IsHere())
                yield return new WaitForSeconds(1f);

            Debug.Log("Я дома");
            actionNo++;
            */
        }

      /*  if (actionNo == 5)
        {
            keyAction = null;
            doing = false;
            StartWriting();
        }
        */

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

        keyAction = null;
        doing = false;
        StartWriting();
    }


    private IEnumerator Execute()
    {
        Scholar.Selectable(false);
        Scholar.executed = true;
        yield return new WaitForSeconds(1f);

        Scholar.Emotions.ChangeEmotion("dead");
        Scholar.Stop();
    }
}

