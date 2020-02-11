using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Scholar : MonoBehaviour
{
    //Тип ученика
    [SerializeField] 
    private ScholarTypes.list scholarType;
    [HideInInspector]
    public string type { get; private set; }
    private bool writing;

    //Базовое

    public string keyWord { get; private set; }
    [HideInInspector]
    public bool executed;
    private bool selectable = true;




    //Доп инструменты
    [HideInInspector]
    public ScholarActions Action { get; private set; }
    [HideInInspector]
    public ScholarAgent Agent { get; private set; }
    [HideInInspector]
    public ScholarAnim Anim { get; private set; }
    [HideInInspector]
    public ScholarCheat Cheat { get; private set; }
    [HideInInspector]
    public ScholarEmotions Emotions { get; private set; }
    [HideInInspector]
    public ScholarMove Move { get; private set; }
    [HideInInspector]
    public ScholarSenses Senses { get; private set; }
    [HideInInspector]
    public ScholarStress Stress { get; private set; }
    [HideInInspector]
    public ScholarTest Test { get; private set; }
    [HideInInspector]
    public ScholarTextBox TextBox { get; private set; }
    [HideInInspector]
    public ScholarTalk Talk { get; private set; }
    [HideInInspector]
    public ScholarComputer Desk { get; private set; }
    [HideInInspector]
    public ScholarQuestions Question { get; private set; }
    [HideInInspector]
    public ScholarBulling Bull { get; private set; }





    //Список замечаний, которые уже были сделаны.
    [HideInInspector]
    public Dictionary<string, bool> remarks = new Dictionary<string, bool>()
    {
        { "Talking_", false },
        { "Cheating_", false },
        { "Walking_", false },
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
        Anim = new ScholarAnim(transform.GetComponentInParent<Animator>());
        Question = new ScholarQuestions(this);
        Action = new ScholarActions(this);
        Senses = new ScholarSenses(this);
        Stress = new ScholarStress(this);
        Cheat = new ScholarCheat(this);
        Talk = new ScholarTalk(this);
        Test = new ScholarTest();

        TextBox = GetComponent<ScholarTextBox>();
        TextBox.SetupTextBox();

        Emotions = GetComponent<ScholarEmotions>();
        Emotions.SetupEmotions();

        Move = transform.GetComponentInParent<ScholarMove>();
        Move.SetupMove(this);

        ChangeType(scholarType.ToString());
        Selectable(true);
    }



    void Update()
    {
        if (!executed)
        {
            Test.Update();
            Question.Update();
            Senses.Update();
            Talk.Update();
        }
    }

    private void FixedUpdate()
    {
        if (!executed)
            Stress.MoodTypeTimeUpdate();
    }

    public void Stop()
    {
        Action.Stop();

    }





    //========================================================================================================
    //Наезд на ученика

    public void HearBulling(bool strong)
    {
        if (strong)
        {
            Emotions.ChangeEmotion("suprised");
        }
        else
        {
            Emotions.ChangeEmotion("suprised");
        }
    }

    public void Bulling(string key, bool strong)
    {
        if (strong)
        {
            Stress.Change(10);
            Emotions.ChangeEmotion("upset", "ussual", 4f);
        }
        else
        {
            Emotions.ChangeEmotion("happy", "smile", 4f);
        }

        Answer(key);
    }

    public void Answer(string key)
    {
        if (IsTeacherBullingRight())
        {
            Talk.Say(key + "_Yes");
        }
        else
        {
            Talk.Say(key + "_No");
        }
    }



    //========================================================================================================
    //Прав ли учитель?

    public bool IsTeacherBullingRight()
    {
        switch (GetView())
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
                    if (Talk.talking)
                        return true;
                    else
                        return false;
                }
            case "Walking_":
                {
                    if (Move.walking /*или на улице)*/
                        return true;
                    else
                        return false;
                }
        }
        return false;
    }

    //========================================================================================================
    //Вопросы и ответы



    //=================================================================================================================================================
    //Учитель кричит на ученика

    public void Shout()
    {

    }





    //=================================================================================================================================================
    //Как выглядит то что делает ученик

    public string GetView()
    {
        if (Talk.talking)
        {
            return "Talking_";
        }
        else if (Move.walking)
        {
            return "Walking_";
        }
        else
        {
            return "Cheating_";
        }
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
    //Присвоить номер ученику

    public void SetNumber(int i)
    {
        number = i;
        Action.home = ScholarManager.get.desks[0, i].position;
        Action.desk = ScholarManager.get.desks[1, i].position;
        TextBox.Number(i);
        Desk = DeskManager.get.desks[i];
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
        Agent = new ScholarAgent(type, this);
        keyWord = type + "_";
    }
}
