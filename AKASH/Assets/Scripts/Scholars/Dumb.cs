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
        Action = GetComponent<ActionsScholar>();
        this.tag = "Dumb";
        keyWord += this.tag + "_";
        IQ_start = 0;
        StartWrite();
    }



    void Update()
    {
        if(writing)
            WritingTest(UnityEngine.Random.value * 100);
    }

    public void Bulling(string bullType, bool strong)
    {

        if(strong)
        {
            Debug.Log("Учитель наезжает");
            Stress(10);
            StartCoroutine(Say(keyWord + bullType));
        }
        else
        {
            Debug.Log("Учитель прикалывается");
            StartCoroutine(Say(keyWord + bullType));
        }
    }



    IEnumerator Say(string key)
    {
        Stop();
        TextBox.Say(key);
        Debug.Log("Я начал говорить с учителем");
        yield return new WaitForSeconds(1f);

        while (TextBox.IsTalking())
        {
            Debug.Log("Я говорю с учителем");
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Я закончил говорить с учителем");
        if (Probability(0.5))
            Continue();
        else
            StartWrite();
    }





}
