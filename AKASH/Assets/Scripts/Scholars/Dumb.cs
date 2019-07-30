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

    public void Bulling(string bullType, bool strong)
    {
        if (strong)
        {
            Debug.Log("Учитель наезжает");
            Stress(10);
            if (IsTeacherBullingRight())
            {
                StartCoroutine(Say(keyWord + bullType + "_Yes", 0));
            }
            else
            {
                StartCoroutine(Say(keyWord + bullType + "_No", 1));
            }
            Emotions.ChangeEmotion("upset", "ussual", 4f);
        }
        else
        {
            Debug.Log("Учитель прикалывается");
            if (IsTeacherBullingRight())
            {
                StartCoroutine(Say(keyWord + bullType + "_Yes", 0));
            }
            else
            {
                StartCoroutine(Say(keyWord + bullType + "_No", 1));
            }
            Emotions.ChangeEmotion("happy", "smile", 4f);
        }
    }

    private bool IsTeacherBullingRight()
    {
        switch(view)
        {
            case "Cheating_":
                {
                    if (cheating)
                        return true;
                    else
                        return false;
                }
            case "Talking_":
                {
                    if (talking)
                        return true;
                    else
                        return false;
                }
            case "Walking_":
                {
                    if (walkingAnswer)
                        return true;
                    else
                        return false;
                }
        }
        return false;
    }

    IEnumerator Say(string key, double probability)
    {
        Stop();
        TextBox.Say(key);
        //Debug.Log("Я начал говорить");
        yield return new WaitForSeconds(1f);

        while (TextBox.IsTalking())
        {
            //Debug.Log("Я говорю");
            yield return new WaitForSeconds(1f);
        }

        //Debug.Log("Я закончил говорить");
        if (Probability(probability))
            Continue();
        else
            StartWrite();
    }





}
