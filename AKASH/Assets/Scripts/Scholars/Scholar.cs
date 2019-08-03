using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scholar : MonoBehaviour
{
    [HideInInspector]
    public int stress;
    [HideInInspector]
    public bool cheating;
    [HideInInspector]
    public bool walking;
    [HideInInspector]
    public bool talking;
    [HideInInspector]
    public bool walkingAnswer;
    [HideInInspector]
    public TextBoxScholar TextBox;
    [HideInInspector]
    public ActionsScholar Action;
    [HideInInspector]
    public Emotions Emotions;
    [HideInInspector]
    public GameManager GameMan;
    [HideInInspector]
    public PlayerScript Player;
    [HideInInspector]
    public string keyWord;
    [HideInInspector]
    public bool executed;
    private double rnd;
    [HideInInspector]
    public byte behaviourType;
    [HideInInspector]
    public byte moodType;
    [HideInInspector]
    public int threshold_1 = 33;
    [HideInInspector]
    public int threshold_2 = 66;
    [HideInInspector]
    public int IQ_start;
    [HideInInspector]
    public int test;
    [HideInInspector]
    public double test_buf;
    [HideInInspector]
    public float test_bufTime;
    [HideInInspector]
    public string view = "Cheating_";
    [HideInInspector]
    public bool writing;
    [HideInInspector]
    public Dictionary<string, bool> remarks = new Dictionary<string, bool>()
    {
        { "Pen_", false },
        { "Calculator_", false },
        { "Talking_", false },
        { "Cheating_", false },
        { "Walking_", false },
        { "Nothing_", false }
    };





    public void Continue()
    {
        writing = true;
        Action.Continue();
    }

    public void Stop()
    {
        writing = false;
        StopAllCoroutines();
        Action.Stop();
        TextBox.Clear();
    }

    public void StartWrite()
    {
        writing = true;
    }


    public void Stress(int value)
    {
        stress += value;
        if (stress > 100)
            stress = 100;
        if (stress < 0)
            stress = 0;

        ChangeMoodType();
    }


    private void ChangeMoodType()
    {
        if (stress < threshold_1)
            moodType = 0;
        else if (stress < threshold_2)
            moodType = 1;
        else
            moodType = 2;
    }


    public bool Probability(double a)
    {
        rnd = UnityEngine.Random.value;

        if (a >= rnd)
            return true;
        else
            return false;
    }


    public void WritingTest(float value)
    {
        //Debug.Log("Пишу тест");
        if (test_bufTime > 0)
        {
            test_buf += value * Time.deltaTime;
            test_bufTime -= Time.deltaTime;
        }
        else
        {
            test += Convert.ToInt32(test_buf);
            test_bufTime = 1f;
            test_buf = 0;
        }
    }


    public IEnumerator Say(string key, double probability)
    {
        Stop();
        view = "Talking_";
        talking = true;
        TextBox.Say(key);
        //Debug.Log("Я начал говорить");

        yield return new WaitForSeconds(1f);

        while (TextBox.IsTalking())
        {
            //Debug.Log("Я говорю");
            yield return new WaitForSeconds(1f);
        }

        talking = false;

        //Debug.Log("Я закончил говорить");
        if (Probability(probability))
            Continue();
        else
            StartWrite();
    }


    public bool IsTeacherBullingRight()
    {
        switch (view)
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


    public bool IsTeacherBullingRight(string obj)
    {
        Debug.Log(obj);
        if (GameMan.banned[obj])
            return true;
        else
            return false;
    }



    //Исключение
    public IEnumerator Execute()
    {
        yield return new WaitForSeconds(1f);

        Stop();
        Emotions.ChangeEmotion("dead");
        executed = true;
    }
}
