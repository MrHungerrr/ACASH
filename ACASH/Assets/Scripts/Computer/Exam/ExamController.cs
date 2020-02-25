using UnityEngine;
using System.Collections;
using TMPro;
using Exam;

public class ExamController : MonoBehaviour
{
    [HideInInspector]
    public A_Computer computer;
    private bool scholar;
    [HideInInspector]
    public int[] answers = new int[3];


    public void SetExamController(A_Computer computer, bool scholar)
    {
        this.computer = computer;
        this.scholar = scholar;
        Reset();
    }


    public void Reset()
    {
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i] = -1;
        }
    }


    public void Finish()
    {
        if (scholar)
        {
            //Отправить ответы в базу данных;
        }
    }
}
