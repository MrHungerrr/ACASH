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
    public ScholarComputer Desk { get; private set; }
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
    public ScholarSelect Select{ get; private set; }








    private void Awake()
    {
        this.tag = "Scholar";
        ChangeType(scholarType.ToString());

        Anim = new ScholarAnim(transform.GetComponentInParent<Animator>());
        Question = new ScholarQuestions(this);
        Reaction = new ScholarReactions();
        Bull = new ScholarBulling();
        Action = new ScholarActions(this);
        Senses = new ScholarSenses(this);
        Stress = new ScholarStress(this);
        Cheat = new ScholarCheat(this);
        Talk = new ScholarTalk(this);
        Test = new ScholarTest();
        Info = new ScholarInfo();
        View = new ScholarView();

        TextBox = GetComponent<ScholarTextBox>();
        TextBox.SetupTextBox();

        Emotions = GetComponent<ScholarEmotions>();
        Emotions.SetupEmotions();

        Move = transform.GetComponentInParent<ScholarMove>();
        Move.SetupMove(this);

        Select = GetComponent<ScholarSelect>();
        Select.SetScholarSelect();
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
    //Присвоить номер ученику

    public void SetNumber(int i)
    {
        Info.SetNumber(i);
        TextBox.Number(i);
        Desk = DeskManager.get.desks[i];
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
