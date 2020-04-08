using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalk : MonoBehaviour
{
    [HideInInspector]
    public Scholar scholar { get; private set; }
    private Scholar scholar_execute;
    protected KeyWord key_word = new KeyWord("Teacher");
    public bool talking { get; private set; } = false;


    public event ActionEvent.OnActionBool AnswerStart;
    public event ActionEvent.OnActionBool TalkStart;
    public event ActionEvent.OnActionBool AnswerDone;
    public event ActionEvent.OnActionBool TalkDone;

    public event ActionEvent.OnAction ShoutStart;
    public event ActionEvent.OnAction ExecuteStart;
    public event ActionEvent.OnAction ShoutDone;
    public event ActionEvent.OnAction ExecuteDone;




    public void SetScholar(Scholar Scholar)
    {
        this.scholar = Scholar;
    }

    public void ResetScholar()
    {
        this.scholar = null;
    }



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
        if (ShoutStart != null)
            ShoutStart();

        //Scholar[] scholars = ScholarManager.get.GetVisibleScholars();

        Scholar[] scholars = ScholarManager.get.scholars;


        SubtitleManager.get.Say(key_word);

        yield return new WaitForSeconds(0.4f);

        foreach (Scholar s in scholars)
        {
            if(s.Senses.Teacher.distance < 3f)
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

        if (TalkStart != null)
            TalkStart(strong);

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
        if (AnswerStart != null)
            AnswerStart(answer);

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
        if (ExecuteStart != null)
            ExecuteStart();

        SubtitleManager.get.Say(key_word);
        scholar.Execute.Execute(key_word);

        scholar_execute = scholar;

        SubtitleManager.get.TalkDone += ExecuteEnd;

        while (SubtitleManager.get.act)
        {
            yield return new WaitForEndOfFrame();
        }
    }



    // Лютый костыль
    private void ExecuteEnd()
    {
        SubtitleManager.get.TalkDone -= ExecuteEnd;

        scholar_execute.Execute.LastWord();

        talking = false;

        if (ExecuteDone != null)
            ExecuteDone();
    }
}
