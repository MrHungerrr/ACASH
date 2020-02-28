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




    //Доп инструменты
    [HideInInspector]
    public ScholarActions Action { get; private set; }
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
    public ScholarExam Test { get; private set; }
    [HideInInspector]
    public ScholarTextBox TextBox { get; private set; }
    [HideInInspector]
    public ScholarTalk Talk { get; private set; }
    [HideInInspector]
    public ScholarComputer Desk { get; set; }
    [HideInInspector]
    public ScholarQuestions Question { get; private set; }
    [HideInInspector]
    public ScholarConverastion Conversation { get; private set; }
    [HideInInspector]
    public ScholarReactions Reaction { get; private set; }
    [HideInInspector]
    public ScholarInfo Info { get; private set; }
    [HideInInspector]
    public ScholarView View { get; private set; }
    [HideInInspector]
    public ScholarSelect Select { get; private set; }
    [HideInInspector]
    public ScholarLocation Location { get; private set; }
    [HideInInspector]
    public ScholarCheck Check { get; private set; }
    [HideInInspector]
    public ScholarExecute Execute { get; private set; }
    [HideInInspector]
    public ScholarAnswers Answers { get; private set; }
    [HideInInspector]
    public ScholarObjects Objects { get; private set; }







    private void Awake()
    {
        this.tag = "Scholar";

        Answers = new ScholarAnswers();
        ChangeType(scholarType);

        TextBox = GetComponent<ScholarTextBox>();
        TextBox.SetupTextBox();

        Emotions = GetComponent<ScholarEmotions>();
        Emotions.SetupEmotions();

        Move = transform.parent.GetComponentInParent<ScholarMove>();
        Move.SetupMove(this);

        Select = GetComponent<ScholarSelect>();
        Select.SetScholarSelect();


        Anim = new ScholarAnim(transform.parent.GetComponentInChildren<Animator>());
        Question = new ScholarQuestions(this);
        Location = new ScholarLocation(this);
        Execute = new ScholarExecute(this);
        Objects = new ScholarObjects(this);
        Reaction = new ScholarReactions();
        Action = new ScholarActions(this);
        Senses = new ScholarSenses(this);
        Stress = new ScholarStress(this);
        Conversation = new ScholarConverastion(this);
        Cheat = new ScholarCheat(this);
        Check = new ScholarCheck(this);
        Info = new ScholarInfo(this);
        View = new ScholarView(this);
        Talk = new ScholarTalk(this);
        Test = new ScholarExam();

    }



    public void SetType(ScholarTypes.list type)
    {
        ChangeType(type);
    }



    void Update()
    {
        if (!Execute.executed)
        {
            Test.Update();
            Question.Update();
            Senses.Update();
            Talk.Update();
            Check.Update();
            Objects.Update();
        }
    }

    private void FixedUpdate()
    {
        if (!Execute.executed)
            Stress.MoodTypeTimeUpdate();
    }

    public void Stop()
    {
        Action.Stop();
        Move.Stop();
    }

    public void Continue()
    {
        Action.Continue();
    }


    //========================================================================================================
    //Изменение типа ученика

    public void ChangeType(ScholarTypes.list t)
    {
        scholarType = t;

        type = t.ToString();

        if (Talk != null)
            Talk.key_word = new KeyWord(t.ToString());

        if (Answers != null)
            Answers.Reset();
    }
}
