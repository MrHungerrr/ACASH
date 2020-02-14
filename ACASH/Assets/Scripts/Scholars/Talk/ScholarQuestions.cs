using UnityEngine;
using System.Collections;

public class ScholarQuestions
{
    private Scholar Scholar;
    public bool question { get; private set; }
    public bool answer { get; private set; }
    public bool question_answered { get; private set; }
    private float question_t;
    public string main_key { get; private set; }
    private string question_key;
    private string answer_key;


    private const float question_const_t = 10f;


    public ScholarQuestions(Scholar scholar)
    {
        this.Scholar = scholar;
        question_key = "Question_";
        answer_key = "Answer_";
    }


    public void Update()
    {
        if (question)
            TryGetAnswer();
    }

    public void Ask(string key)
    {
        main_key = key;
        Scholar.Talk.Question(question_key + key);
        question = true;
        question_answered = false;
        question_t = question_const_t;
        answer = false;
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

    public void TeacherAnswer(bool answer)
    {
        this.answer = answer;
        question_answered = true;

        string key;

        if (answer)
            key = "_Yes";
        else
            key = "_No";

        Scholar.Talk.SayWithoutStop(answer_key + key);

        QuestionEnd();
    }


    private void QuestionEnd()
    {
        Debug.Log("Конец вопроса");
        question = false;
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