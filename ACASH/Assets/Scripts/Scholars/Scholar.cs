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
    public string type;


    //Базовое
    public bool isLiving;
    [HideInInspector]
    public int number;
    [HideInInspector]
    public string name_1 = "Akim";
    [HideInInspector]
    public string name_2 = "Akimov";
    private string keyWord;
    [HideInInspector]
    public bool executed;
    private bool selectable = true;



    [HideInInspector]
    public bool talking;
    [HideInInspector]
    public bool walking;
    [HideInInspector]
    public bool cheating;
    [HideInInspector]
    public bool writing;



    //Вопросы
    [HideInInspector]
    public bool asking;
    [HideInInspector]
    public string questionKey;
    [HideInInspector]
    public bool teacher_answer;




    //Доп инструмент ы

    [HideInInspector]
    public ScholarActions Action;
    [HideInInspector]
    public ScholarAgent Agent;
    [HideInInspector]
    public ScholarAnim Anim;
    [HideInInspector]
    public ScholarCheat Cheat;
    [HideInInspector]
    public ScholarEmotions Emotions;
    [HideInInspector]
    public ScholarMove Move;
    [HideInInspector]
    public ScholarSenses Senses;
    [HideInInspector]
    public ScholarStress Stress;
    [HideInInspector]
    public ScholarTest Test;
    [HideInInspector]
    public ScholarTextBox TextBox;


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
        Action = GetComponent<ScholarActions>();
        TextBox = GetComponent<ScholarTextBox>();
        Emotions = GetComponent<ScholarEmotions>();
        Move = transform.GetComponentInParent<ScholarMove>();
        Anim = new ScholarAnim(transform.GetComponentInParent<Animator>());
        Senses = new ScholarSenses(this);
        Stress = new ScholarStress(this);
        Cheat = new ScholarCheat(this);
        Test = new ScholarTest();


        ChangeType(scholarType.ToString());
        Selectable(true);
    }



    void Update()
    {
        if (!executed)
        {
            if (writing)
                Agent.Writing();

            Senses.Teacher();

            if (cheating)
                Cheat.CheatingFinish();
        }
    }

    private void FixedUpdate()
    {
        if (!executed)
            Stress.MoodTypeTime();
    }

    public void Stop()
    {
        StopAllCoroutines();
        Action.Stop();
        TextBox.Clear();
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

    public void SayThoughts(string key)
    {
        StartCoroutine(SayingThoughts(key));
    }

    private IEnumerator Saying(string key, double probability_of_continue)
    {
        talking = true;
        Selectable(false);
        TextBox.Say(key);


        while (TextBox.IsTalking())
        {
            Action.SightTo(Player.get.transform.position);
            yield return new WaitForEndOfFrame();
        }

        Selectable(true);
        talking = false;

        if (BaseMath.Probability(probability_of_continue))
            Action.Continue();
        else
            Action.StartWriting();
    }

    private IEnumerator SayingWithoutContinue(string key)
    {
        talking = true;
        Selectable(false);
        TextBox.Say(key);

        while (TextBox.IsTalking())
        {
            Action.SightTo(Player.get.transform.position);
            yield return new WaitForEndOfFrame();
        }

        Selectable(true);
        talking = false;
    }

    private IEnumerator SayingThoughts(string key)
    {
        talking = true;
        TextBox.Say(key);

        while (TextBox.IsTalking())
        {
            yield return new WaitForEndOfFrame();
        }

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

    private IEnumerator WatchingTeacher()
    {
        while (!talking)
        {
            Action.SightTo(Player.get.transform.position);
            yield return new WaitForEndOfFrame();
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
                    if (talking)
                        return true;
                    else
                        return false;
                }
            case "Walking_":
                {
                    if (walking)
                        return true;
                    else
                        return false;
                }
        }
        return false;
    }

    //========================================================================================================
    //Вопросы и ответы

    public void Question(string q)
    {
        questionKey = keyWord +"Question_" + q;
        teacher_answer = false;
        asking = true;
        StartCoroutine(Asking(q));
    }

    private IEnumerator Asking(string key)
    {
        talking = true;
        Selectable(false);
        TextBox.Say(key);

        yield return new WaitForSeconds(1f);

        while (TextBox.IsTalking())
        {
            Action.SightTo(Player.get.transform.position);
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
    //Как выглядит то что делает ученик

    public string GetView()
    {
        if (talking)
        {
            return "Talking_";
        }
        else if (walking)
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
