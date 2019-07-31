using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Dumb : Scholar
{


    void Awake()
    {
        TextBox = transform.Find("Text Box").GetComponent<TextBoxScholar>();
        Emotions = transform.Find("Face").GetComponent<Emotions>();
        GameMan = GameObject.FindObjectOfType<GameManager>();
        Action = GetComponent<ActionsScholar>();
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
        if(writing)
            WritingTest(UnityEngine.Random.value * 100);
    }


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

    public void Bulling(string bullKey, bool strong)
    {
        if (strong)
        {
            Debug.Log("Учитель наезжает");
            Stress(10);
            if (IsTeacherBullingRight())
            {
                StartCoroutine(Say(keyWord + bullKey + "_Yes", 0));
            }
            else
            {
                StartCoroutine(Say(keyWord + bullKey + "_No", 1));
            }
            Emotions.ChangeEmotion("upset", "ussual", 4f);
        }
        else
        {
            Debug.Log("Учитель прикалывается");
            if (IsTeacherBullingRight())
            {
                StartCoroutine(Say(keyWord + bullKey + "_Yes", 0));
            }
            else
            {
                StartCoroutine(Say(keyWord + bullKey + "_No", 1));
            }
            Emotions.ChangeEmotion("happy", "smile", 4f);
        }
    }

    public void Bulling(string bullKey, bool strong, string obj)
    {
        if (strong)
        {
            Debug.Log("Учитель наезжает");
            Stress(10);
            if (IsTeacherBullingRight(obj))
            {
                StartCoroutine(Say(keyWord + bullKey + "_Yes", 0));
            }
            else
            {
                StartCoroutine(Say(keyWord + bullKey + "_No", 1));
            }
            Emotions.ChangeEmotion("upset", "ussual", 4f);
        }
        else
        {
            Debug.Log("Учитель прикалывается");
            if (IsTeacherBullingRight(obj))
            {
                StartCoroutine(Say(keyWord + bullKey + "_Yes", 0));
            }
            else
            {
                StartCoroutine(Say(keyWord + bullKey + "_No", 1));
            }
            Emotions.ChangeEmotion("happy", "smile", 4f);
        }
    }





}
