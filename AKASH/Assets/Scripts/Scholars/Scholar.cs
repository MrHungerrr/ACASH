using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Scholar : MonoBehaviour
{
    [HideInInspector]
    public int stress;
    private bool cheating;
    [HideInInspector]
    public int nomber;
    [HideInInspector]
    public bool cheatNeed;
    [HideInInspector]
    public bool talking;
    private bool walkingAnswer;
    [HideInInspector]
    public bool question;
    [HideInInspector]
    public string quest;
    [HideInInspector]
    public bool asking;
    [HideInInspector]
    public bool teacher_answer;
    [HideInInspector]
    public bool greeneyes;

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

    [HideInInspector]
    public Dictionary<string, bool> reason = new Dictionary<string, bool>()
    {
        { "Walking_", false },
        { "Talking_", false },
        { "Cheating_", false },
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

        TextBox = transform.parent.transform.parent.GetComponentInChildren<TextBoxScholar>();
        Emotions = transform.parent.transform.parent.GetComponentInChildren<Emotions>();
        Action = transform.parent.transform.GetComponentInParent<ActionsScholar>();
        ScholarMan = GameObject.FindObjectOfType<ScholarManager>();
        ScriptMan = GameObject.FindObjectOfType<ScriptManager>();
        GameMan = GameObject.FindObjectOfType<GameManager>();
        Player = GameObject.FindObjectOfType<PlayerScript>();
        Agent = new ScholarAgent(type, this);

        keyWord = type + "_";
        IQ_start = 0;
    }


    private void Start()
    {
        StartWrite();
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
        quest = keyWord + q;
        teacher_answer = false;
        asking = true;
        question = true;
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
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Мы задали вопрос");
        Selectable(true);
        talking = false;
    }

    public void TeacherPermission(bool answer)
    {
        string pKey = keyWord + quest;

        if (answer)
        {
            pKey += "_Yes";
        }
        else
        {
            pKey += "_No";
            question = false;
        }

        Agent.TeacherPermission(pKey, answer);
        asking = false;
    }

    public void TeacherAnswer(bool answer)
    {
        string buf = "Answer_";
        buf += UnityEngine.Random.Range(0, ScriptMan.linesQuantity[buf]);
        buf = keyWord + buf;

        if (answer)
        {
            buf += "_Yes";
            teacher_answer = true;
        }
        else
        {
            buf += "_No";
        }

        Agent.TeacherAnswer(buf, answer);
        question = false;
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
        StartCoroutine(Execute());
    }

    private IEnumerator Execute()
    {
        Selectable(false);
        executed = true;
        yield return new WaitForSeconds(1f);

        Stop();
        Emotions.ChangeEmotion("dead");
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
        Action.desk = ScholarMan.desks[i].position;
    }



    //========================================================================================================
    //Возможность выбрать объект

    private void Selectable(bool u)
    {
        if (u)
            this.gameObject.layer = 9;
        else
            this.gameObject.layer = 10;
    }



}
