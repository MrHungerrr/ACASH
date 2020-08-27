using UnityEngine;
using Searching;
using Overwatch.Memorable;


public class Scholar : MonoBehaviour
{
    public bool Active { get; private set; }


    public ClassAgent ClassRoom { get; private set; }
    public ScholarLocation Location { get; private set; }
    public ScholarMemorable Watchable { get; private set; }
    public ScholarSounds Sound { get; private set; }
    public ScholarSelect Select => _select;
    public ScholarMove Move => _move;




    [SerializeField] private ScholarMove _move;
    [SerializeField] private ScholarSelect _select;





    public virtual void Setup(ClassAgent classRoom, int number)
    {
        ClassRoom = classRoom;

        Active = false;
        this.tag = "Scholar";

        TextBox.Setup(this);
        Emotions.Setup();
        Move.Setup(this);
        Body.Setup(this);
        Select.Setup(this);
        Info.Setup(number, "Egor", "Akimov");


        Anim = new ScholarAnimtor(transform.Find("Scholar").GetComponent<Animator>());
        Sound = new ScholarSounds(this);
        Location = new ScholarLocation(this);
        Objects = new ScholarObjects(this);
        Action = new ScholarActions(this);
        Cheat = new ScholarCheat(this);
        Watchable = new ScholarMemorable(this);
    }



    public virtual void Reset()
    {
        Active = true;

        Select.Reset();
        Emotions.Reset();
        Talk = new ScholarTalk(this);
        Cheat.Reset();

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
        Select.SetSelectable(false);
        Action.Pause();
        Move.Stop();
        Talk.Stop();
        Objects.ThrowOut();
        Anim.SetAnimation(Animations.Get.animations.Nothing);
        Active = false;
    }
}
