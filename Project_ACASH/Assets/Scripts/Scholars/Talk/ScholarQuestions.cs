using UnityEngine;
using System.Collections;
using ScholarOptions;

public class ScholarQuestions
{
    private Scholar Scholar;


    public bool question { get; private set; }
    public bool answer { get; private set; }
    public bool question_answered { get; private set; }
    private float question_t;
    public KeyWord question_key { get; private set; }
    private KeyWord answer_key;


    private const float question_const_t = 10f;


    public ScholarQuestions(Scholar scholar)
    {
        this.Scholar = scholar;
        question_key = new KeyWord(scholar.type, "Question");
        answer_key = new KeyWord(scholar.type, "Answer");
    }


    public void Update()
    {
        if (question)
            TryGetAnswer();
    }

    public void Ask(string key)
    {
        question_key *= key;
        Scholar.Talk.Question(question_key);
        question = true;
        question_answered = false;
        question_t = question_const_t;
        answer = false;

        Scholar.Emotions.Change(GetS.faces.Ask);
    }


    private void TryGetAnswer()
    {
        if ((question_t > 0 || Scholar.Senses.T_look_at_us))
        {
            Scholar.Move.SetRotateGoal(Player.get.transform.position);

            if (!Scholar.Talk.talking && !Scholar.Senses.T_look_at_us)
            {
                question_t -= Time.deltaTime;
            }
        }
        else
        {
            QuestionEnd();
        }
    }

    public void Answer(bool answer)
    {
        this.answer = answer;
        question_answered = true;

        QuestionEnd();

        answer_key.Reset();
        answer_key.Answer(answer);


        Scholar.Talk.SayWithoutStop(answer_key);
    }


    private void QuestionEnd()
    {
        PlayerCheat.IsAnswerRight(Scholar, answer);

        Debug.Log("Конец вопроса");
        Scholar.Talk.Stop();
        question = false;

        Scholar.Emotions.Change(GetS.faces.Ussual);
    }



    public void Stop()
    {
        if (question)
        {
            question = false;
            Scholar.TextBox.Clear();
        }
    }



}