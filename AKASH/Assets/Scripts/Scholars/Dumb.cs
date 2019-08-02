using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Dumb : Scholar
{


    private void Awake()
    {
        TextBox = transform.parent.GetComponentInChildren<TextBoxScholar>();
        Emotions = transform.parent.GetComponentInChildren<Emotions>();
        GameMan = GameObject.FindObjectOfType<GameManager>();
        Action = transform.GetComponentInParent<ActionsScholar>();
        this.tag = "Dumb";
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
            Action.Doing("Toilet_1");
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
