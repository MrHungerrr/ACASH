using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalk : MonoBehaviour
{
    [HideInInspector]
    public Scholar scholar { get; set; }
    private KeyWord key_word = new KeyWord("Teacher");
    public bool talking { get; private set; } = false;


    private void BeginOfTalk()
    {
        StopThinking();
        key_word.Reset();
        talking = true;
    }





    private void StopThinking()
    {
        if (SubtitleManager.get.act)
            SubtitleManager.get.StopSubtitile();
    }



    public void Shout()
    {
        BeginOfTalk();
        key_word += "Shout";

        StartCoroutine(Shouting());
    }





    //========================================================================================================
    //Крик на всех школьников

    private IEnumerator Shouting()
    {
        ScholarManager.get.Shout(10);

        SubtitleManager.get.Say(key_word);

        while (SubtitleManager.get.act)
        {
            yield return new WaitForEndOfFrame();
        }

        talking = false;
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

    public void Talk(bool strong)
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

    private IEnumerator Talking(Scholar scholar, bool strong)
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
    }







    //========================================================================================================
    //Ответ на вопрос

    public void Answer(bool answer)
    {
        BeginOfTalk();
        key_word += "Answer";
        PlayerCheat.IsAnswerRight(scholar, answer);

        StartCoroutine(Answering(scholar, answer));
    }



    private IEnumerator Answering(Scholar scholar, bool answer)
    {
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

        while (scholar.Talk.talking)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);
    }






    //========================================================================================================
    //Наезд на школьника

    public void Execute()
    {
        BeginOfTalk();
        key_word += "Execute";

        PlayerCheat.IsExecuteRight(scholar);

        StartCoroutine(Execute(scholar));
    }


    private IEnumerator Execute(Scholar scholar)
    {
        SubtitleManager.get.Say(key_word);
        scholar.Execute.Execute(key_word);

        while (SubtitleManager.get.act)
        {
            yield return new WaitForEndOfFrame();
        }

        talking = false;
    }
}
