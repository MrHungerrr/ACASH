using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerTalk : MonoBehaviour
{
    [HideInInspector]
    public Scholar scholar { get; private set; }
    private Scholar scholar_execute;
    protected KeyWord key_word = new KeyWord("Teacher");
    public bool talking { get; private set; } = false;


    //Надо будет заменить
    public event ActionEvent.OnActionBool AnswerStart;
    public event ActionEvent.OnActionBool TalkStart;
    public event ActionEvent.OnActionBool AnswerDone;
    public event ActionEvent.OnActionBool TalkDone;

    public UnityEvent ShoutStart;
    public UnityEvent ExecuteStart;
    public UnityEvent ShoutDone;
    public UnityEvent ExecuteDone;




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


        if (SubtitleManager.Instance.act)
            SubtitleManager.Instance.StopSubtitile();
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
        ShoutStart.Invoke();

        //Scholar[] scholars = ScholarManager.get.GetVisibleScholars();

        Scholar[] scholars = ScholarManager.Instance.scholars;


        SubtitleManager.Instance.Say(key_word);

        yield return new WaitForSeconds(0.4f);

        foreach (Scholar s in scholars)
        {
            if(s.Senses.Teacher.distance < 3f)
                s.Conversation.Shout();
        }

        while (SubtitleManager.Instance.act)
        {
            yield return new WaitForEndOfFrame();
        }

        talking = false;

        ShoutDone.Invoke();
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

        SubtitleManager.Instance.Say(key_word);

        yield return new WaitForSeconds(1f);

        scholar.Conversation.HearTeacherTalking(strong);

        while (SubtitleManager.Instance.act)
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

        StartCoroutine(Answering(scholar, answer));
    }



    protected IEnumerator Answering(Scholar scholar, bool answer)
    {
        if (AnswerStart != null)
            AnswerStart(answer);

        key_word += scholar.type;
        key_word += scholar.Question.question_key;
        key_word.Answer(answer);

        SubtitleManager.Instance.Say(key_word);

        yield return new WaitForSeconds(1f);

        scholar.Conversation.HearTeacherTalking(!answer);

        while (SubtitleManager.Instance.act)
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
        ExecuteStart.Invoke();

        SubtitleManager.Instance.Say(key_word);
        scholar.Execute.Execute(key_word);

        scholar_execute = scholar;

        SubtitleManager.Instance.TalkDone += ExecuteEnd;

        while (SubtitleManager.Instance.act)
        {
            yield return new WaitForEndOfFrame();
        }
    }



    // Лютый костыль
    private void ExecuteEnd()
    {
        SubtitleManager.Instance.TalkDone -= ExecuteEnd;

        scholar_execute.Execute.LastWord();

        talking = false;

        ExecuteDone.Invoke();
    }
}
