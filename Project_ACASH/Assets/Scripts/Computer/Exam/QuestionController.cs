using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Exam;

public class QuestionController : MonoBehaviour
{
    private ExamController Exam;
    private Question question;
    private int question_number;
    private TextMeshProUGUI question_field;
    private TextMeshProUGUI[] answers = new TextMeshProUGUI[4];
    private Image[] arrows = new Image[4];



    public void Setup()
    {
        this.Exam = GetComponent<ExamController>();
        Transform question = transform.Find("Question");

        this.question_field = question.Find("Question").GetComponent<TextMeshProUGUI>();

        for(int i = 0; i < answers.Length; i++)
        {
            answers[i] = question.Find("Answers").Find("Answer_" + i).GetComponentInChildren<TextMeshProUGUI>();
            arrows[i] = question.Find("Answer " + (i + 1)).Find("Arrow").GetComponent<Image>();
        }
    }


    public void SetQuestion(int number)
    {
        try
        {
            question = QuestionHolder.get.questions[number];
            question_number = number;

            question_field.text = question.question;

            for (int i = 0; i < answers.Length; i++)
            {
                Debug.Log((i + 1) + ". " + question.answers[i]);
                answers[i].text = (i + 1) + ". " + question.answers[i];
            }

            SetAnswer(Exam.answers[number]);
        }
        catch
        {
            Debug.LogError("Неправильный номер вопроса - " + number);
        }
    }


    public void SetAnswer(int number)
    {
        Exam.answers[question_number] = number;

        for (int i = 0; i < arrows.Length; i++)
        {
            if(i == number)
            {
                arrows[i].enabled = true;
            }
            else
            {
                arrows[i].enabled = false;
            }
        }
    }

}
