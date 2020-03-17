using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Single;

public class TutorialPlayerWatcher: MonoBehaviour
{

    [HideInInspector]
    public bool talk_good { get; private set; }
    [HideInInspector]
    public bool talk_bad { get; private set; }
    [HideInInspector]
    public bool answer_yes { get; private set; }
    [HideInInspector]
    public bool answer_no { get; private set; }
    [HideInInspector]
    public bool shout { get; private set; }
    [HideInInspector]
    public bool execute { get; private set; }
    [HideInInspector]
    public bool done { get; private set; }




    public void Setup()
    {
        Reset();

        Player.get.Talk.TalkDone += PlayerTalk;
        Player.get.Talk.AnswerDone += PlayerAnswer;
        Player.get.Talk.ShoutDone += PlayerShout;
        Player.get.Talk.ExecuteDone += PlayerExecute;
    }


    public void Reset()
    {
        talk_good = false;
        talk_bad = false;
        answer_yes = false;
        answer_no = false;
        shout = false;
        execute = false;
        done = false;
    }



    private void PlayerTalk(bool strong)
    {
        if (strong)
            talk_bad = true;
        else
            talk_good = true;

        PlayerDoneSomething();
    }

    private void PlayerAnswer(bool strong)
    {
        if (strong)
            answer_no = true;
        else
            answer_yes = true;

        PlayerDoneSomething();
    }


    private void PlayerShout()
    {
        shout = true;

        PlayerDoneSomething();
    }

    private void PlayerExecute()
    {
        execute = true;
        PlayerDoneSomething();
    }


    private void PlayerDoneSomething()
    {
        done = true;
    }
}
