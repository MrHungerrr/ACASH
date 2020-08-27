using UnityEngine;
using System.Collections;
using TMPro;

public class ExamController : MonoBehaviour
{
    [HideInInspector]
    public A_Computer computer;
    private bool scholar;
    [HideInInspector]
    public int[] answers = new int[3];


    public void Setup(ScholarComputer computer)
    {
        this.computer = computer;
        this.scholar = true;
        Reset();
    }

    public void SetExamController(A_Computer computer)
    {
        this.computer = computer;
        this.scholar = false;
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
