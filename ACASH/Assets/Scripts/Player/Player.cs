using System.Collections;
using UnityEngine;
using Single;

public class Player : Singleton<Player>
{

    public PlayerMove Move { get; private set; }


    //Управление
    [HideInInspector]
    public bool look_closer;


    [HideInInspector]
    public bool draw;
    private bool draw_option;

    //Действия
    [HideInInspector]
    public bool doing;
    private bool think;
    [HideInInspector]
    public bool asked;
    private GameObject selected_scholar;
    private string actText;
    private string keyWord = "Teacher_";
    private string key;



    private void Awake()
    {
        Move = GetComponent<PlayerMove>();
        Move.SetupMove();
    }

    private void Start()
    {
        actLayerMask = LayerMask.GetMask("Selectable");
        sightLayerMask = LayerMask.GetMask("Sight Layer");
    }

    private void Update()
    {

        if (!act)
            Watching();

        Action();
    }




    private void WhatISee()
    {
        if (actObject.GetComponent<Scholar>().Question.question)
        {
            asked = true;
        }
        else
        {
            asked = false;
        }
    }




    private void Action()
    {
        if (actReady && !act)
        {
            if (doing)
            {
                act = true;

                switch (actTag)
                {
                    case "Computer":
                        {
                            if (actSpecialOption)
                            {
                                actObject.GetComponent<TeacherComputerController>().Enable(true);
                                act = false;
                            }
                            break;
                        }
                    case "Door":
                        {
                            if (actSpecialOption)
                            {
                                if (typeOfMovement != "crouch")
                                    actObject.GetComponent<Door>().DoorInteract(transform.position);
                                else
                                    actObject.GetComponent<Door>().DoorQuietInteract(transform.position);
                            }
                            break;
                        }
                    case "Elevator":
                        {
                            if (actSpecialOption)
                            {
                                ElevatorController.get.Open();
                            }
                            break;
                        }
                }
            }
        }
    }


    private void SpecialSelectRealtion()
    {
        switch(actTag)
        {
            case "Computer":
                {
                    if (BaseGeometry.LookingAngle(actObject.transform, transform.position) < 70)
                    {
                        actObject.GetComponent<I_ObjectSelect>().Select();
                        actSpecialOption = true;
                    }
                    else
                    {
                        actObject.GetComponent<I_ObjectSelect>().Deselect();
                        actSpecialOption = false;
                    }
                    break;
                }
            case "Door":
                {
                    if (!actObject.GetComponent<Door>().locked)
                    {
                        actObject.GetComponent<I_ObjectSelect>().Select();
                        actSpecialOption = true;
                    }
                    else
                    {
                        actObject.GetComponent<I_ObjectSelect>().Deselect();
                        actSpecialOption = false;
                    }
                    break;
                }
            case "Elevator":
                {
                    if (ElevatorController.get.ready)
                    {
                        actObject.GetComponent<I_ObjectSelect>().Select();
                        actSpecialOption = true;
                    }
                    else
                    {
                        actObject.GetComponent<I_ObjectSelect>().Deselect();
                        actSpecialOption = false;
                    }
                    break;
                }
            default:
                {
                    actObject.GetComponent<I_ObjectSelect>().Select();
                    break;
                }
        }
    }



    public void Shout()
    {
        if (ExamManager.get.exam_part != "prepare")
            StartCoroutine(Shouting());
        else
            StartCoroutine(StartExam());
    }

    private IEnumerator Shouting()
    {
        StopThinking();
        ScholarManager.get.Stress(10);
        act = true;
        key = "Shout_";
        int nomber = Random.Range(0, ScriptManager.get.linesQuantity[keyWord + key]);
        key += nomber;
        SubtitleManager.get.Say(keyWord + key);
        yield return new WaitForSeconds(1f);
        while (SubtitleManager.get.act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        act = false;
    }



    public void Answer(bool answer)
    {
        StopThinking();
        act = true;

        key = "Answer_";

        var scholar = actObject.GetComponent<Scholar>();

        StartCoroutine(Answering(scholar, answer));
    }



    private IEnumerator Answering(Scholar scholar, bool answer)
    {
        key += scholar.Question.main_key;

        if (answer)
            key += "_Yes";
        else
            key += "_No";

        SubtitleManager.get.Say(keyWord + key);

        yield return new WaitForSeconds(1f);

        scholar.Bull.HearBulling(!answer);

        while (SubtitleManager.get.act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        scholar.Question.TeacherAnswer(answer);
        act = false;
    }

    public void Bull(bool strong)
    {
        StopThinking();
        act = true;

        if (strong)
            key = "Bull_";
        else
            key = "Joke_";

        var scholar = actObject.GetComponent<Scholar>();

        if (!scholar.executed)
            StartCoroutine(Bulling(scholar, strong));
    }



    //========================================================================================================
    //Наезд на школьника

    private IEnumerator Bulling(Scholar scholar, bool strong)
    {
        ScoreManager.get.Bull(scholar, strong);

        key += scholar.View.GetView();

        if (scholar.View.remarks[scholar.View.GetView()])
        {
            if (BaseMath.Probability(0.5))
                key += "Sec_";
        }
        else
            scholar.View.remarks[scholar.View.GetView()] = true;

        int nomber = Random.Range(0, ScriptManager.get.linesQuantity[keyWord + key]);
        key += nomber;
        SubtitleManager.get.Say(keyWord + key);

        yield return new WaitForSeconds(1f);

        scholar.Bull.HearBulling(strong);


        while (SubtitleManager.get.act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        scholar.Bull.Bulling(key, strong);
        act = false;

        while (scholar.TextBox.IsTalking() && !act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //Добавить вероятность + взгляд
        if (!act && BaseMath.Probability(0.1))
        {
            SubtitleManager.get.Say(keyWord + "Thinking_" + scholar.tag + "_" + Random.Range(0, ScriptManager.get.linesQuantity[keyWord + "Thinking_"]));
        }
    }



    //========================================================================================================
    //Наезд на школьника


    public void Execute(string reason)
    {
        StopThinking();
        act = true;
        key = "Execute_" + reason + "_";

        var scholar = actObject.GetComponent<Scholar>();

        if (!scholar.executed)
            StartCoroutine(Execute(scholar));
    }




    private IEnumerator Execute(Scholar scholar)
    {

        int nomber = Random.Range(0, ScriptManager.get.linesQuantity[keyWord + key]);
        key += nomber;

        SubtitleManager.get.Say(keyWord + key);

        yield return new WaitForSeconds(1f);

        scholar.Bull.HearBulling(true);

        yield return new WaitForSeconds(1f);

        scholar.Execute(key);

        while (!scholar.executed && !act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        act = false;

        SubtitleManager.get.Say(keyWord + "Thinking_" + key);
    }


    private void StopThinking()
    {
        if(SubtitleManager.get.act)
            SubtitleManager.get.StopSubtitile();
    }


    private IEnumerator StartExam()
    {
        yield return new WaitForEndOfFrame();

        ExamManager.get.StartExam();
    }
}
