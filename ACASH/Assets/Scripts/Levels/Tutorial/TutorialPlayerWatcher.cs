public class TutorialPlayerWatcher
{

    private bool start;
    public bool talk { get; private set; }
    public bool talk_good { get; private set; }
    public bool talk_bad { get; private set; }
    public bool answer { get; private set; }
    public bool answer_yes { get; private set; }
    public bool answer_no { get; private set; }
    public bool shout { get; private set; }
    public bool execute { get; private set; }
    public bool done { get; private set; }



    public TutorialPlayerWatcher(bool start = true)
    {
        Setup(start);
    }


    private void Setup(bool start)
    {
        Reset();

        this.start = start;

        if (start)
        {
            Player.get.Talk.TalkStart += PlayerTalk;
            Player.get.Talk.AnswerStart += PlayerAnswer;
            Player.get.Talk.ShoutStart.AddListener(PlayerShout);
            Player.get.Talk.ExecuteStart.AddListener(PlayerExecute);
        }
        else
        {
            Player.get.Talk.TalkDone += PlayerTalk;
            Player.get.Talk.AnswerDone += PlayerAnswer;
            Player.get.Talk.ShoutDone.AddListener(PlayerShout);
            Player.get.Talk.ExecuteDone.AddListener(PlayerExecute);
        }
    }

    public void Unsetup()
    {
        if (start)
        {
            Player.get.Talk.TalkStart -= PlayerTalk;
            Player.get.Talk.AnswerStart -= PlayerAnswer;
            Player.get.Talk.ShoutStart.RemoveListener(PlayerShout);
            Player.get.Talk.ExecuteStart.RemoveListener(PlayerExecute);
        }
        else
        {
            Player.get.Talk.TalkDone -= PlayerTalk;
            Player.get.Talk.AnswerDone -= PlayerAnswer;
            Player.get.Talk.ShoutDone.RemoveListener(PlayerShout);
            Player.get.Talk.ExecuteDone.RemoveListener(PlayerExecute);
        }
    }


    public void Reset()
    {
        talk = false;
        talk_good = false;
        talk_bad = false;
        answer = false;
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

        talk = true;
        done = true;
    }

    private void PlayerAnswer(bool answer)
    {
        if (answer)
            answer_yes = true;
        else
            answer_no = true;

        this.answer = true;
        done = true;
    }


    private void PlayerShout()
    {
        shout = true;
        done = true;
    }

    private void PlayerExecute()
    {
        execute = true;
        done = true;
    }
}
