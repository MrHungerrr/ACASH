using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalkControl : PlayerTalk
{

    [HideInInspector]
    public bool all_controll;
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
        AllowAll();
    }


    public void AllowAll()
    {
        all_controll = true;
        shout_control = true;
        talk_good_control = true;
        talk_bad_control = true;
        answer_yes_control = true;
        answer_no_control = true;
        execute_control = true;
    }

    public void DenyAll()
    {
        all_controll = false;
        shout_control = false;
        talk_good_control = false;
        talk_bad_control = false;
        answer_yes_control = false;
        answer_no_control = false;
        execute_control = false;
    }


    public override void Shout()
    {
        if(shout_control && all_controll)
            base.Shout();
    }


    public override void Talk(bool strong)
    {
        if(strong && talk_bad_control && all_controll)
            base.Talk(strong);

        if (!strong && talk_good_control && all_controll)
            base.Talk(strong);
    }


    public override void Answer(bool answer)
    {
        if (answer && answer_yes_control && all_controll)
            base.Answer(answer);

        if (!answer && answer_no_control && all_controll)
            base.Answer(answer);
    }


    public override void Execute()
    {
        if (execute_control && all_controll)
            base.Execute();
    }

}
