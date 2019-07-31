using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asshole : Scholar
{

    void Awake()
    {
        TextBox = transform.Find("Text Box").GetComponent<TextBoxScholar>();
        Emotions = transform.Find("Face").GetComponent<Emotions>();
        Action = GetComponent<ActionsScholar>();
        this.tag = "Asshole";
        keyWord += this.tag + "_";
        IQ_start = 0;
    }


    void Update()
    {

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
            StartCoroutine(Say(keyWord + bullType, 1));
            Emotions.ChangeEmotion("upset", "ussual", 4f);
        }
        else
        {
            Debug.Log("Учитель прикалывается");
            StartCoroutine(Say(keyWord + bullType, 1));
            Emotions.ChangeEmotion("happy", "smile", 4f);
        }
    }
}
