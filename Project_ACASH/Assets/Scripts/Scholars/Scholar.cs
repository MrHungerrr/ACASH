using UnityEngine;
using Searching;
using Overwatch.Watchable;


public class Scholar : MonoBehaviour
{
    public bool Active { get; private set; }


    public ClassAgent ClassRoom { get; private set; }
    public ScholarComputer Desk { get; private set; }
    public ScholarActions Action { get; private set; }
    public ScholarAnimtor Anim { get; private set; }
    public ScholarCheat Cheat { get; private set; }
    public ScholarTalk Talk { get; private set; }
    public ScholarInfo Info { get; private set; }
    public ScholarLocation Location { get; private set; }
    public ScholarObjects Objects { get; private set; }
    public ScholarWatchable Watchable { get; private set; }
    public ScholarEmotions Emotions => _emotions;
    public ScholarTextBox TextBox => _textBox;
    public ScholarSelect Select => _select;
    public ScholarSounds Sound => _sound;
    public ScholarBody Body => _body;
    public ScholarMove Move => _move;




    [SerializeField] private ScholarTextBox _textBox;

    [SerializeField] private ScholarEmotions _emotions;

    [SerializeField] private ScholarMove _move;

    [SerializeField] private ScholarBody _body;

    [SerializeField] private ScholarSelect _select;

    [SerializeField] private ScholarSounds _sound;




    public virtual void Setup(ClassAgent classRoom, ScholarComputer desk)
    {
        ClassRoom = classRoom;
        Desk = desk;

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
        Watchable = new ScholarWatchable(this);
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
