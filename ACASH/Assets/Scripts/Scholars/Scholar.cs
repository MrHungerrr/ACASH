using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Scholar : MonoBehaviour
{
    public bool handControl;
    //Тип ученика
    [SerializeField] 
    private ScholarTypes.list scholarType;
    [HideInInspector]
    public string type { get; private set; }

    [HideInInspector]
    public bool active { get; private set; }




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







    public void Setup()
    {
        this.tag = "Scholar";

        TextBox = GetComponent<ScholarTextBox>();
        TextBox.Setup();

        Emotions = GetComponent<ScholarEmotions>();
        Emotions.Setup();

        Move = transform.parent.GetComponentInParent<ScholarMove>();
        Move.Setup(this);

        Select = GetComponent<ScholarSelect>();
        Select.Setup(this);


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
        Test = new ScholarExam();

        if(handControl)
            SetType(scholarType);
    }



    public void SetType(ScholarTypes.list type)
    {
        active = true;
        ChangeType(type);

        Talk = new ScholarTalk(this);
        Answers = new ScholarAnswers();
    }



    void Update()
    {
        if (active)
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
        if (active)
            Stress.MoodTypeTimeUpdate();
    }



    public void Continue()
    {
        Action.Continue();
    }

    public void Pause()
    {
        Action.Pause();
        Move.Stop();
    }

    public void Disable()
    {
        Action.Disable();
        Move.Stop();
        Talk.Stop();
        Anim.SetAnimation(Animations.GetA.animations.Nothing);
        active = false;
    }




    //========================================================================================================
    //Изменение типа ученика

    public void ChangeType(ScholarTypes.list t)
    {
        scholarType = t;
        type = t.ToString();
    }
}
