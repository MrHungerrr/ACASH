using UnityEngine;


public class Scholar : MonoBehaviour
{

    public bool disabled;
    //Тип ученика
    public ScholarTypes.list scholarType;
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
    [HideInInspector]
    public ScholarSounds Sound { get; private set; }
    [HideInInspector]
    public ScholarBody Body { get; private set; }







    public virtual void Setup()
    {
        active = false;
        this.tag = "Scholar";

        Transform buf = transform.Find("Script Holder");

        TextBox = buf.GetComponent<ScholarTextBox>();
        TextBox.Setup(this);

        Emotions = buf.GetComponent<ScholarEmotions>();
        Emotions.Setup();

        Move = GetComponent<ScholarMove>();
        Move.Setup(this);

        Body = buf.GetComponent<ScholarBody>();
        Body.Setup(this);

        Select = GetComponent<ScholarSelect>();
        Select.Setup(this);

        Sound = buf.GetComponent<ScholarSounds>();
        Sound.Setup(this);


        Anim = new ScholarAnim(transform.Find("Scholar").GetComponent<Animator>());
        Debug.Log(1);
        Question = new ScholarQuestions(this);
        Debug.Log(2);
        Location = new ScholarLocation(this);
        Debug.Log(3);
        Execute = new ScholarExecute(this);
        Debug.Log(4);
        Objects = new ScholarObjects(this);
        Debug.Log(5);
        Reaction = new ScholarReactions();
        Debug.Log(6);
        Action = new ScholarActions(this);
        Debug.Log(7);
        Senses = new ScholarSenses(this);
        Debug.Log(8);
        Stress = new ScholarStress(this);
        Debug.Log(9);
        Conversation = new ScholarConverastion(this);
        Debug.Log(10);
        Cheat = new ScholarCheat(this);
        Debug.Log(11);
        Check = new ScholarCheck(this);
        Debug.Log(12);
        Info = new ScholarInfo(this);
        Debug.Log(13);
        View = new ScholarView(this);
        Debug.Log(14);
        Test = new ScholarExam();
    }



    public virtual void SetNewType(ScholarTypes.list type)
    {
        active = true;
        ChangeType(type);

        Select.Reset();
        Emotions.Reset();
        Execute.Reset();
        Stress.Reset();

        Talk = new ScholarTalk(this);
        Answers = new ScholarAnswers();
        Cheat.Reset();

        if(Desk != null)
            Desk.ResetComputer();
        Body.Enable();
    }

    public void ResetType()
    {
        if (scholarType == ScholarTypes.list.Random)
        {
            int rand = Random.Range(0, ScholarTypes.length);
            scholarType = (ScholarTypes.list)rand;
        }

        SetNewType(scholarType);
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
            Stress.Update();
        }
    }



    public virtual void Continue()
    {
        if (active)
            Action.Continue();
    }

    public void Pause()
    {
        Action.Pause();
        Move.Stop();

        Objects.ThrowOut();
    }


    public void Disable()
    {
        Select.Selectable(false);
        Action.Pause();
        Move.Stop();
        Talk.Stop();
        Objects.ThrowOut();
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
