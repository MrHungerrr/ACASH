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
    [HideInInspector]
    public ScholarHUD HUD { get; private set; }







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
        Question = new ScholarQuestions(this);
        Info = new ScholarInfo(this);
        Location = new ScholarLocation(this);
        Execute = new ScholarExecute(this);
        Objects = new ScholarObjects(this);
        Reaction = new ScholarReactions();
        Action = new ScholarActions(this);
        HUD = new ScholarHUD(this);
        Senses = new ScholarSenses(this);
        Stress = new ScholarStress(this);
        Conversation = new ScholarConverastion(this);
        Cheat = new ScholarCheat(this);
        Check = new ScholarCheck(this);
        View = new ScholarView(this);
        Test = new ScholarExam(this);
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

        if(HUD != null)
            HUD.Update();
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
