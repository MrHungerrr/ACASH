using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Scholar : MonoBehaviour
{
    private int stress;
    private bool cheating;
    private bool talking;
    private bool walkingAnswer;
    private bool asking;

    [HideInInspector]
    public TextBoxScholar TextBox;
    [HideInInspector]
    public ActionsScholar Action;
    [HideInInspector]
    public Emotions Emotions;
    private GameManager GameMan;
    private PlayerScript Player;
    private ScholarAgent Agent;


    [HideInInspector]
    public bool executed;
    private double rnd;
    private byte moodType;
    private string type;
    private string keyWord;
    [HideInInspector]
    public int threshold_1 = 33;
    [HideInInspector]
    public int threshold_2 = 66;
    [HideInInspector]
    public int IQ_start;
    [HideInInspector]
    public int test;
    [HideInInspector]
    public double test_buf;
    [HideInInspector]
    public float test_bufTime;
    [HideInInspector]
    public string view = "Cheating_";
    [HideInInspector]
    public bool writing;
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

    public enum list_scholarType
    {
        Dumb,
        Asshole,
        Underdog
    }

    public list_scholarType scholarType;



    private void Awake()
    {
        this.tag = "Scholar";
        type = scholarType.ToString();

        TextBox = transform.parent.GetComponentInChildren<TextBoxScholar>();
        Emotions = transform.parent.GetComponentInChildren<Emotions>();
        Action = transform.GetComponentInParent<ActionsScholar>();
        GameMan = GameObject.FindObjectOfType<GameManager>();
        Player = GameObject.FindObjectOfType<PlayerScript>();
        Agent = new ScholarAgent(type, this);

        keyWord = type + "_";
        IQ_start = 0;
    }

    private void Start()
    {
        StartWrite();
    }



    void Update()
    {

        if (writing)
            WritingTest(UnityEngine.Random.value * 100);
    }

    public void Continue()
    {
        writing = true;
        Debug.Log("Продолжаем");
        Action.Continue();
    }

    public void Stop()
    {
        if (!executed)
        {
            writing = false;
            StopAllCoroutines();
            Action.Stop();
            TextBox.Clear();
        }
    }

    public void StartWrite()
    {
        writing = true;
    }



    //--------------------------------------------------------------------------------------------------------
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



    //--------------------------------------------------------------------------------------------------------
    //Ученик говорит

    public void Say(string key, double probability_of_continue)
    {
        Stop();
        StartCoroutine(Saying(key, probability_of_continue));
    }

    private IEnumerator Saying(string key, double probability_of_continue)
    {
        view = "Talking_";
        talking = true;
        TextBox.Say(key);
        //Debug.Log("Я начал говорить");

        yield return new WaitForSeconds(1f);

        while (TextBox.IsTalking())
        {
            //Debug.Log("Я говорю");
            yield return new WaitForSeconds(1f);
        }

        talking = false;

        Debug.Log("Я закончил говорить");
        if (Probability(probability_of_continue))
            Continue();
        else
            StartWrite();
    }



    //--------------------------------------------------------------------------------------------------------
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


    //--------------------------------------------------------------------------------------------------------
    //Наезд на ученика

    public void HearBulling(bool strong)
    {
        Agent.HearBulling(strong);
    }

    public void Bulling(string bullKey, bool strong)
    {
        Agent.Bulling(keyWord + bullKey, strong);
    }

    public void BullingForSubjects(string bullKey, string obj)
    {
        Agent.BullingForSubjects(keyWord + bullKey, obj);
    }



    //--------------------------------------------------------------------------------------------------------
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



    //--------------------------------------------------------------------------------------------------------
    //Исключение

    public void Execute(string key)
    {
        Stop();
        TextBox.Say(keyWord + key);
        StartCoroutine(Execute());
    }

    private IEnumerator Execute()
    {
        executed = true;
        yield return new WaitForSeconds(1f);

        Stop();
        Emotions.ChangeEmotion("dead");
    }



    //--------------------------------------------------------------------------------------------------------
    //Вероятность

    public bool Probability(double a)
    {
        rnd = UnityEngine.Random.value;

        if (a >= rnd)
            return true;
        else
            return false;
    }


}
