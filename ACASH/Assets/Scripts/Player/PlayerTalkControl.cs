using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalkControl : PlayerTalk
{

    [HideInInspector]
    public bool shout_control;
    [HideInInspector]
    public bool talk_good_control;
    [HideInInspector]
    public bool talk_bad_control;
    [HideInInspector]
    public bool answer_yes_control;
    [HideInInspector]
    public bool answer_no_control;
    [HideInInspector]
    public bool execute_control;




    private void Awake()
    {
        shout_control = false;
        talk_good_control = false;
        talk_bad_control = false;
        answer_yes_control = false;
        answer_no_control = false;
        execute_control = false;
    }


    public override void Shout()
    {
        if(shout_control)
            base.Shout();
    }


    public override void Talk(bool strong)
    {
        if(strong && talk_bad_control)
            base.Talk(strong);

        if (!strong && talk_good_control)
            base.Talk(strong);
    }


    public override void Answer(bool answer)
    {
        if (answer && answer_yes_control)
            base.Answer(answer);

        if (!answer && answer_no_control)
            base.Answer(answer);
    }


    public override void Execute()
    {
        if (execute_control)
            base.Execute();
    }

}
