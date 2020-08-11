using UnityEngine;
using Searching;


public class Scholar : MonoBehaviour
#region IInitialization
#if UNITY_EDITOR
    ,IInitialization
#endif
#endregion
{
    public bool Active { get; private set; }



    public ScholarActions Action { get; private set; }
    public ScholarAnimtor Anim { get; private set; }
    public ScholarCheat Cheat { get; private set; }
    public ScholarTalk Talk { get; private set; }
    public ScholarInfo Info { get; private set; }
    public ScholarLocation Location { get; private set; }
    public ScholarObjects Objects { get; private set; }
    public ScholarEmotions Emotions => _emotions;
    public ScholarTextBox TextBox => _textBox;
    public ScholarSelect Select => _select;
    public ScholarSounds Sound => _sound;
    public ScholarBody Body => _body;
    public ScholarMove Move => _move;
    public ScholarComputer Desk => _desk;
    public ClassAgent ClassRoom => _classRoom;



    [SerializeField]
    private ClassAgent _classRoom;

    [SerializeField]
    private ScholarComputer _desk;

    [SerializeField]
    private ScholarTextBox _textBox;

    [SerializeField]
    private ScholarEmotions _emotions;

    [SerializeField]
    private ScholarMove _move;

    [SerializeField]
    private ScholarBody _body;

    [SerializeField]
    private ScholarSelect _select;

    [SerializeField]
    private ScholarSounds _sound;




    #region IInitializator
#if UNITY_EDITOR
    public bool TryInitializate()
    {
        try
        {
            Transform buf = transform.Find("Script Holder");

            _textBox = buf.GetComponent<ScholarTextBox>();
            _emotions = buf.GetComponent<ScholarEmotions>();
            _move = GetComponent<ScholarMove>();
            _body = buf.GetComponent<ScholarBody>();
            _select = GetComponent<ScholarSelect>();
            _sound = buf.GetComponent<ScholarSounds>();

            return true;
        }
        catch
        {
            return false;
        }
    }


    public void SetDesk(ScholarComputer desk)
    {
        _desk = desk;
    }

    public void SetClass(ClassAgent classRoom)
    {
        _classRoom = classRoom;
    }

#endif
    #endregion


    public virtual void Setup()
    {
        Active = false;
        this.tag = "Scholar";

        TextBox.Setup(this);
        Emotions.Setup();
        Move.Setup(this);
        Body.Setup(this);
        Select.Setup(this);
        Sound.Setup(this);

        Anim = new ScholarAnimtor(transform.Find("Scholar").GetComponent<Animator>());
        Info = new ScholarInfo(this);
        Location = new ScholarLocation(this);
        Objects = new ScholarObjects(this);
        Action = new ScholarActions(this);
        Cheat = new ScholarCheat(this);
    }



    public virtual void Reset()
    {
        Active = true;

        Select.Reset();
        Emotions.Reset();
        Talk = new ScholarTalk(this);
        Cheat.Reset();

        Desk?.ResetComputer();

        Body.Enable();
    }



    private void Update()
    {
        if (Active)
        {
            Talk.Update();
            Objects.Update();
        }
    }



    public virtual void Continue()
    {
        if (Active)
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
        Anim.SetAnimation(Animations.Get.animations.Nothing);
        Active = false;
    }


}
