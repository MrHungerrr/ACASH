using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Scholar : MonoBehaviour
{
    //Тип ученика
    public enum list_scholarType
    {
        Dumb,
        Asshole,
        Underdog
    }

    public list_scholarType scholarType;


    //Базовое
    public bool isLiving;
    [HideInInspector]
    public int nomber;
    [HideInInspector]
    public string type;
    private string keyWord;
    [HideInInspector]
    public bool executed;
    [HideInInspector]
    public bool greeneyes;


    [HideInInspector]
    public bool talking;
    private bool cheating;
    [HideInInspector]
    public bool cheatNeed;
    [HideInInspector]
    public string view;
    [HideInInspector]
    public bool writing;

    //Вопросы
    private bool walkingAnswer;
    [HideInInspector]
    public bool asking;
    [HideInInspector]
    public string questionKey;
    [HideInInspector]
    public bool teacher_answer;

    //Доп инструмент ы
    [HideInInspector]
    public TextBoxScholar TextBox;
    [HideInInspector]
    public ActionsScholar Action;
    [HideInInspector]
    public Emotions Emotions;
    private GameManager GameMan;
    private ScriptManager ScriptMan;
    private PlayerScript Player;
    [HideInInspector]
    public ScholarAgent Agent;
    private ScholarManager ScholarMan;



    private double rnd;
    private bool selectable = true;

    //Стресс и настроение
    [HideInInspector]
    public int stress;
    [HideInInspector]
    public int threshold_1 = 33;
    [HideInInspector]
    public int threshold_2 = 66;
    private byte moodType;


    //Написание теста
    [HideInInspector]
    public int IQ_start;
    [HideInInspector]
    public int test;
    [HideInInspector]
    public double test_buf;
    [HideInInspector]
    public float test_bufTime;


    //Список замечаний, которые уже были сделаны.
    [HideInInspector]
    public Dictionary<string, bool> remarks = new Dictionary<string, bool>()
    {
        { "Pen_", false },
        { "Calculator_", false },
        { "Talking_", false },
        { "Cheating_", false },
        { "Walking_", false },
        { "Nothing_", false }
    };


    //Список причин, по которым можно удалить ученика
    [HideInInspector]
    public Dictionary<string, bool> reason = new Dictionary<string, bool>()
    {
        { "Walking_", false },
        { "Talking_", false },
        { "Cheating_", false },
    };









    private void Awake()
    {
        this.tag = "Scholar";

        TextBox = transform.parent.transform.parent.GetComponentInChildren<TextBoxScholar>();
        Emotions = transform.parent.transform.parent.GetComponentInChildren<Emotions>();
        Action = transform.parent.transform.GetComponentInParent<ActionsScholar>();
        ScholarMan = GameObject.FindObjectOfType<ScholarManager>();
        ScriptMan = GameObject.FindObjectOfType<ScriptManager>();
        GameMan = GameObject.FindObjectOfType<GameManager>();
        Player = GameObject.FindObjectOfType<PlayerScript>();

        ChangeType(scholarType.ToString());
        IQ_start = 0;
    }


    private void Start()
    {
        //StartWrite();
        view = "Cheating_";
        Action.Doing("Toilet_1");
    }



    void Update()
    {
        if (writing)
            Agent.Writing();
    }

    private void FixedUpdate()
    {
        if (!cheatNeed)
            Agent.CheatNeed();
    }

    public void Continue()
    {
        Action.Continue();
        Debug.Log("Продолжаем");
    }

    public void Stop()
    {
        if (!executed)
        {
            StopAllCoroutines();
            Action.Stop();
            TextBox.Clear();
        }
    }

    public void StartWrite()
    {
        Action.StartWriting();
    }



    //========================================================================================================
    //Поднятие стресса

    public void Stress(int value)
    {
        stress += value;
        if (stress > 100)
            stress = 100;
        if (stress < 0)
            stress = 0;

        ChangeMoodType();
    }

    private void ChangeMoodType()
    {
        if (stress < threshold_1)
            moodType = 0;
        else if (stress < threshold_2)
            moodType = 1;
        else
            moodType = 2;
    }



    public string GetMoodType()
    {
        switch(moodType)
        {
            case 0:
                {
                    return "chill";
                }
            case 1:
                {
                    return "normal";
                }
            case 2:
                {
                    return "panic";
                }
            default:
                {
                    return "";
                }
        }
    }



    public void WritingTest(float value)
    {
        //Debug.Log("Пишу тест");
        if (test_bufTime > 0)
        {
            test_buf += value * Time.deltaTime;
            test_bufTime -= Time.deltaTime;
        }
        else
        {
            test += Convert.ToInt32(test_buf);
            test_bufTime = 1f;
            test_buf = 0;
        }
    }



    //========================================================================================================
    //Ученик говорит

    public void Say(string key, double probability_of_continue)
    {
        Stop();
        StartCoroutine(Saying(key, probability_of_continue));
    }

    public void Say(string key)
    {
        Stop();
        StartCoroutine(Saying(key, 0));
    }

    public void SayWithoutContinue(string key)
    {
        StartCoroutine(SayingWithoutContinue(key));
    }

    private IEnumerator Saying(string key, double probability_of_continue)
    {
        view = "Talking_";
        talking = true;
        Selectable(false);
        TextBox.Say(key);


        while (TextBox.IsTalking())
        {
            Action.Watch(Player.transform.position);
            yield return new WaitForEndOfFrame();
        }

        Selectable(true);
        talking = false;

        if (Probability(probability_of_continue))
            Continue();
        else
            StartWrite();
    }

    private IEnumerator SayingWithoutContinue(string key)
    {
        view = "Talking_";
        talking = true;
        Selectable(false);
        TextBox.Say(key);

        while (TextBox.IsTalking())
        {
            Action.Watch(Player.transform.position);
            yield return new WaitForEndOfFrame();
        }

        Selectable(true);
        talking = false;
    }



    //========================================================================================================
    //Ответ учителю

    public void Answer(string key, double prob_cont_right, double prob_cont_false)
    {
        if (IsTeacherBullingRight())
        {
            Say(key + "_Yes", prob_cont_right);
        }
        else
        {
            Say(key + "_No", prob_cont_false);
        }
    }

    public void Answer(string key, string obj, double prob_cont_right, double prob_cont_false)
    {
        if (IsTeacherBullingRight(obj))
        {
            Say(key + "_Yes", prob_cont_right);
        }
        else
        {
            Say(key + "_No", prob_cont_false);
        }
    }



    //========================================================================================================
    //Наезд на ученика

    public void HearBulling(bool strong)
    {
        Agent.HearBulling(strong);
        StartCoroutine(WatchingTeacher());
    }

    public void Bulling(string bullKey, bool strong)
    {
        Agent.Bulling(keyWord + bullKey, strong);
    }

    public void BullingForSubjects(string bullKey, string obj)
    {
        Agent.BullingForSubjects(keyWord + bullKey, obj);
    }

    private IEnumerator WatchingTeacher()
    {
        while (!talking)
        {
            Action.Watch(Player.transform.position);
            yield return new WaitForEndOfFrame();
        }
    }



    //========================================================================================================
    //Прав ли учитель?

    public bool IsTeacherBullingRight()
    {
        switch (view)
        {
            case "Cheating_":
                {
                    if (cheating)
                        return true;
                    else
                        return false;
                }
            case "Talking_":
                {
                    if (talking)
                        return true;
                    else
                        return false;
                }
            case "Walking_":
                {
                    if (walkingAnswer)
                        return true;
                    else
                        return false;
                }
        }
        return false;
    }

    public bool IsTeacherBullingRight(string obj)
    {
        Debug.Log(obj);
        if (GameMan.banned[obj])
            return true;
        else
            return false;
    }



    //========================================================================================================
    //Вопросы и ответы

    public void Question(string q)
    {
        questionKey = keyWord + q;
        teacher_answer = false;
        asking = true;
        StartCoroutine(Asking(q));
    }

    private IEnumerator Asking(string key)
    {
        view = "Talking_";
        talking = true;
        Selectable(false);
        TextBox.Question(key);

        yield return new WaitForSeconds(1f);

        while (TextBox.IsTalking())
        {
            Action.Watch(Player.transform.position);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Мы задали вопрос");
        Selectable(true);
        talking = false;
    }

    public void TeacherAnswer(bool answer)
    {
        //string buf = "Answer_";
        //buf += UnityEngine.Random.Range(0, ScriptMan.linesQuantity[buf]);
        string key = questionKey;

        if (answer)
        {
            key += "_Yes";
            teacher_answer = true;
        }
        else
        {
            key += "_No";
        }

        Agent.TeacherAnswer(key, answer);
        asking = false;
    }



    //=================================================================================================================================================
    //Учитель кричит на ученика

    public void Shout()
    {

    }



    //=================================================================================================================================================
    //Исключение

    public void Execute(string key)
    {
        Stop();
        TextBox.Say(keyWord + key);
        Action.Doing("Execute");
    }





    //========================================================================================================
    //Вероятность

    public bool Probability(double a)
    {
        rnd = UnityEngine.Random.value;

        if (a >= rnd)
            return true;
        else
            return false;
    }



    //========================================================================================================
    //Присвоить номер ученику

    public void SetNomber(int i)
    {
        nomber = i;
        Action.home = ScholarMan.desks[0, i].position;
        Action.desk = ScholarMan.desks[1, i].position;
    }



    //========================================================================================================
    //Возможность выбрать объект

    public void Selectable(bool u)
    {

        if (u)
        {
            selectable = true;
            StartCoroutine(SetSelectable());
        }
        else
        {
            this.gameObject.layer = 10;
            selectable = false;
        }
    }



    private IEnumerator SetSelectable()
    {
        yield return new WaitForSeconds(0.1f);
        if (selectable)
        {
            this.gameObject.layer = 9;
        }
    }



    //========================================================================================================
    //Изменение типа ученика

    public void ChangeType(string t)
    {
        type = t;
        Debug.Log(t);
        Agent = new ScholarAgent(type, this);
        keyWord = type + "_";
    }


}
