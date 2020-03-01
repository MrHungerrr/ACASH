using UnityEngine;
using System.Collections;

public class ScholarAnswers
{

    public int[] answers { get; set; } = new int[3];


    public ScholarAnswers()
    {
        Reset();
    }


    private void Reset()
    {
        for(int i = 0; i< answers.Length; i++)
        {
            answers[i] = -1;
        }
    }
}
