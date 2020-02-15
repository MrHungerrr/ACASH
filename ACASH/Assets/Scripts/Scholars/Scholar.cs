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

    //Базовое
    public string keyWord { get; private set; }
    [HideInInspector]
    public bool executed;




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
    public ScholarTest Test { get; private set; }
    [HideInInspector]
    public ScholarTextBox TextBox { get; private set; }
    [HideInInspector]
    public ScholarTalk Talk { get; private set; }
    [HideInInspector]
    public ScholarComputer Desk { get; set; }
    [HideInInspector]
    public ScholarQuestions Question { get; private set; }
    [HideInInspector]
    public ScholarBulling Bull { get; private set; }
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







    private void Awake()
    {
        this.tag = "Scholar";
        ChangeType(scholarType.ToString());

        TextBox = GetComponent<ScholarTextBox>();
        TextBox.SetupTextBox();

        Emotions = GetComponent<ScholarEmotions>();
        Emotions.SetupEmotions();

        Move = transform.parent.GetComponentInParent<ScholarMove>();
        Move.SetupMove(this);

        Select = GetComponent<ScholarSelect>();
        Select.SetScholarSelect();


        Anim = new ScholarAnim(transform.GetComponentInParent<Animator>());
        Question = new ScholarQuestions(this);
        Location = new ScholarLocation(this);
        Reaction = new ScholarReactions();
        Action = new ScholarActions(this);
        Senses = new ScholarSenses(this);
        Stress = new ScholarStress(this);
        Bull = new ScholarBulling(this);
        Cheat = new ScholarCheat(this);
        Check = new ScholarCheck(this);
        Info = new ScholarInfo(this);
        View = new ScholarView(this);
        Talk = new ScholarTalk(this);
        Test = new ScholarTest();
    }



    void Update()
    {
        if (!executed)
        {
            Test.Update();
            Question.Update();
            Senses.Update();
            Talk.Update();
            Check.Update();
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
        Move.Stop();
    }

    public void Continue()
    {
        Action.Continue();
    }



    //=================================================================================================================================================
    //Исключение

    public void Execute(string key)
    {
        Stop();
        Select.Selectable(false);
        TextBox.Say(keyWord + key);
        Action.DoAction("Execute");
    }

    //========================================================================================================
    //Изменение типа ученика

    public void ChangeType(string t)
    {
        type = t;
        keyWord = type + "_";
    }
}
