using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asshole : Scholar
{

    void Awake()
    {
        TextBox = transform.Find("Text Box").GetComponent<TextBoxScholar>();
        Emotions = transform.Find("Face").GetComponent<Emotions>();
        GameMan = GameObject.FindObjectOfType<GameManager>();
        Action = GetComponent<ActionsScholar>();
        this.tag = "Asshole";
        keyWord = this.tag + "_";
        IQ_start = 0;
    }

    private void Start()
    {
        StartWrite();
    }



    void Update()
    {
        if (writing)
            WritingTest(UnityEngine.Random.value * 100);
    }


    //Преждевременная реакция на услышынное

    public void HearBulling(bool strong)
    {
        if (strong)
        {
            Emotions.ChangeEmotion("suprised");
        }
        else
        {
            Emotions.ChangeEmotion("suprised");
        }
    }


    //Наезд учителя за списывание/поведение

    public void Bulling(string bullKey, bool strong)
    {
        if (strong)
        {
            Debug.Log("Учитель наезжает");
            Stress(10);
            Emotions.ChangeEmotion("upset", "ussual", 4f);
        }
        else
        {
            Debug.Log("Учитель прикалывается");
            Emotions.ChangeEmotion("happy", "smile", 4f);
        }

        if (IsTeacherBullingRight())
        {
            StartCoroutine(Say(keyWord + bullKey + "_Yes", 0));
        }
        else
        {
            StartCoroutine(Say(keyWord + bullKey + "_No", 1));
        }
    }



    //Наезд учителя за какие-то предметы

    public void Bulling(string bullKey, bool strong, string obj)
    {
        if (strong)
        {
            Debug.Log("Учитель наезжает");
            Stress(10);
            Emotions.ChangeEmotion("upset", "ussual", 4f);
        }
        else
        {
            Debug.Log("Учитель прикалывается");
            Emotions.ChangeEmotion("happy", "smile", 4f);
        }

        if (IsTeacherBullingRight(obj))
        {
            StartCoroutine(Say(keyWord + bullKey + "_Yes", 0));
        }
        else
        {
            StartCoroutine(Say(keyWord + bullKey + "_No", 1));
        }
    }

}
