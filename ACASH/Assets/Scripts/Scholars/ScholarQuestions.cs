using UnityEngine;
using System.Collections;

public class ScholarQuestions
{
    private Scholar Scholar;
    public bool question { get; private set; }
    public bool answer { get; private set; }
    public bool question_end { get; private set; }
    private float question_t;
    private string base_key;
    private string question_key;
    private string answer_key;


    private const float question_const_t = 10f;


    public ScholarQuestions(Scholar scholar)
    {
        this.Scholar = scholar;
        question_key = Scholar.keyWord + "Question_";
        answer_key = Scholar.keyWord + "Answer_";
    }


    public void Update()
    {
        if (question)
            TryGetAnswer();
    }

    public void TeacherAnswer(bool answer)
    {
        this.answer = answer;
        question_end = true;

        string key = base_key;

        if (answer)
            key += "_Yes";
        else
            key += "_No";

        Scholar.Talk.Say(answer_key + key);
    }

    public void Ask(string key)
    {
        base_key = key;
        Scholar.Talk.Say(question_key + key);
        question = true;
        question_end = false;
        answer = false;
    }

    private void TryGetAnswer()
    {
        if ((!question_end) && (question_t > 0 || Scholar.Senses.T_look_at_us))
        {
            Scholar.Move.SetRotateGoal(Player.get.transform.position);

            if (!Scholar.Talk.talking && !Scholar.Senses.T_look_at_us)
            {
                question_t -= Time.deltaTime;
            }
        }
        else
        {
            Debug.Log("Конец вопроса");
            question_t = question_const_t;
            question = false;
        }
    }


    public void StopQuestion()
    {
        Scholar.asking = false;
        Scholar.TextBox.Clear();
    }
}