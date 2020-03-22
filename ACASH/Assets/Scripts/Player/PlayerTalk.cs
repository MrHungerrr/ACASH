using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalk : MonoBehaviour
{
    [HideInInspector]
    public Scholar scholar { get; set; }
    protected KeyWord key_word = new KeyWord("Teacher");
    public bool talking { get; private set; } = false;



    public delegate void BoolDone(bool option);
    public event BoolDone AnswerDone;
    public event BoolDone TalkDone;

    public delegate void OnDone();
    public event OnDone ShoutDone;
    public event OnDone ExecuteDone;



    protected void BeginOfTalk()
    {
        StopThinking();
        key_word.Reset();
        talking = true;
    }

    protected void StopThinking()
    {


        if (SubtitleManager.get.act)
            SubtitleManager.get.StopSubtitile();
    }



    public virtual void Shout()
    {
        BeginOfTalk();
        key_word += "Shout";

        StartCoroutine(Shouting());
    }



    //========================================================================================================
    //Крик на всех школьников

    protected IEnumerator Shouting()
    {

        Scholar[] scholars = ScholarManager.get.GetVisibleScholars();

        SubtitleManager.get.Say(key_word);

        yield return new WaitForSeconds(0.4f);

        foreach (Scholar s in scholars)
        {
            s.Conversation.Shout();
        }

        while (SubtitleManager.get.act)
        {
            yield return new WaitForEndOfFrame();
        }

        talking = false;

        if (ShoutDone != null)
            ShoutDone();
    }



    public void TalkGood()
    {
        if (scholar.Question.question)
            Answer(true);
        else
            Talk(false);
    }

    public void TalkBad()
    {
        if (scholar.Question.question)
            Answer(false);
        else
            Talk(true);
    }

    public virtual void Talk(bool strong)
    {
        BeginOfTalk();

        if (strong)
            key_word += "Bull";
        else
            key_word += "Joke";

        StartCoroutine(Talking(scholar, strong));
    }





    //========================================================================================================
    //Наезд на школьника

    protected IEnumerator Talking(Scholar scholar, bool strong)
    {
        // Подлежит изменению (View) \\

        key_word += scholar.View.GetView().ToString();

        //Было ли сделано уже такое замечание?
        if (scholar.View.GetRemarksOnView())
        {
            if (BaseMath.Probability(0.5))
                key_word += "Sec";
        }
        // Подлежит изменению \\

        SubtitleManager.get.Say(key_word);

        yield return new WaitForSeconds(1f);

        scholar.Conversation.HearTeacherTalking(strong);

        while (SubtitleManager.get.act)
        {
            yield return new WaitForEndOfFrame();
        }

        scholar.Conversation.Listening(key_word, strong);
        talking = false;

        if (TalkDone != null)
            TalkDone(strong);
    }







    //========================================================================================================
    //Ответ на вопрос

    public virtual void Answer(bool answer)
    {
        BeginOfTalk();
        key_word += "Answer";
        PlayerCheat.IsAnswerRight(scholar, answer);

        StartCoroutine(Answering(scholar, answer));
    }



    protected IEnumerator Answering(Scholar scholar, bool answer)
    {
        key_word += scholar.type;
        key_word += scholar.Question.question_key;
        key_word.Answer(answer);

        SubtitleManager.get.Say(key_word);

        yield return new WaitForSeconds(1f);

        scholar.Conversation.HearTeacherTalking(!answer);

        while (SubtitleManager.get.act)
        {
            yield return new WaitForEndOfFrame();
        }

        scholar.Question.Answer(answer);    
        talking = false;

        if(AnswerDone != null)
            AnswerDone(answer);

        while (scholar.Talk.talking)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);
    }






    //========================================================================================================
    //Наезд на школьника

    public virtual void Execute()
    {
        BeginOfTalk();
        key_word += "Execute";

        PlayerCheat.IsExecuteRight(scholar);

        StartCoroutine(Execute(scholar));
    }


    protected IEnumerator Execute(Scholar scholar)
    {
        SubtitleManager.get.Say(key_word);
        scholar.Execute.Execute(key_word);

        while (SubtitleManager.get.act)
        {
            yield return new WaitForEndOfFrame();
        }

        talking = false;


        if (ExecuteDone != null)
            ExecuteDone();
    }
}
