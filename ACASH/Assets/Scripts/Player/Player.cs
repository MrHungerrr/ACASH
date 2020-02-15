using System.Collections;
using UnityEngine;
using Single;

public class Player : Singleton<Player>
{
    //Управление
    [HideInInspector]
    public Vector2 moveInput;
    private Vector2 move;
    [HideInInspector]
    public string typeOfMovement;
    private float crouchSpeed = 35f;
    private float normalSpeed = 65f;
    private float runSpeed = 100f;
    private float crouchSound = 0.5f;
    private float normalSound = 3f;
    private float runSound = 5f;
    private float movementSpeed;
    private float movementSound;
    private double rnd;
    private CharacterController CharController;
    private LayerMask actLayerMask;
    private LayerMask sightLayerMask;
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
    [HideInInspector]
    public bool act;
    [HideInInspector]
    public bool actReady;
    private float actMaxRange = 2f;
    private float actMinRange = 0.6f;
    [HideInInspector]
    public string actTag;
    [HideInInspector]
    public GameObject actObject;
    [HideInInspector]
    public bool actSpecialOption;
    private GameObject selected_scholar;
    private string actText;
    private string keyWord = "Teacher_";
    private string key;



    private void Awake()
    {
        CharController = GetComponent<CharacterController>();
        draw = false;
    }

    private void Start()
    {
        actLayerMask = LayerMask.GetMask("Selectable");
        sightLayerMask = LayerMask.GetMask("Sight Layer");
        SwitchMove("normal");
    }

    private void Update()
    {

        PlayerMovement();

        if (!act)
            Watching();

        Action();

            


        if (draw && actTag == "DeskBlock")
            Drawing(draw_option);
    }


    public void SwitchMove(string type)
    {
        typeOfMovement = type;

        switch(type)
        {
            case "normal":
                {
                    movementSpeed = normalSpeed;
                    movementSound = normalSound;
                    break;
                }
            case "run":
                {
                    movementSpeed = runSpeed;
                    movementSound = runSound;
                    break;
                }
            case "crouch":
                {
                    //Присесть на корточки
                    movementSpeed = crouchSpeed;
                    movementSound = crouchSound;
                    break;
                }
        }
    }

    private void PlayerMovement()
    {
        move = moveInput.normalized * movementSpeed * Time.deltaTime;

        Vector3 forwardMovement = transform.forward * move.y;
        Vector3 rightMovement = transform.right * move.x;

        if (forwardMovement != Vector3.zero || rightMovement != Vector3.zero)
        {
            CharController.SimpleMove(forwardMovement + rightMovement);
            ScholarManager.get.Hear(movementSound);
        }
    }


    private void Watching()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        I_ObjectSelect obj_select;


        //Рейкаст интерактивных объектов
        if (Physics.Raycast(ray, out hit, actMaxRange, actLayerMask))
        {
            //Школьники
            if (hit.collider.tag == "Scholar")
            {
                if (actObject != hit.collider.gameObject)
                {
                    if (actObject != null)
                    {
                        actObject.GetComponent<I_ObjectSelect>().Deselect();
                    }
                    actObject = hit.collider.gameObject;
                    actTag = hit.collider.tag;
                    actReady = true;
                    actObject.GetComponent<I_ObjectSelect>().Select();

                    WhatISee();
                }
            }
            //Все остальное
            else if((hit.transform.position - transform.position).magnitude < actMinRange)
            {
                if (actObject != hit.collider.gameObject)
                {
                    if (actObject != null)
                    {
                        if(actObject.TryGetComponent<I_ObjectSelect>(out obj_select))
                            obj_select.Deselect();
                    }
                    actObject = hit.collider.gameObject;
                    actTag = hit.collider.tag;
                    actReady = true;

                    if (actObject.TryGetComponent<I_ObjectSelect>(out obj_select))
                            SpecialSelectRealtion();
                }
                else
                {
                    SpecialSelectRealtion();
                }
            }
            else if (actReady && actObject != null)
            {
                if (actObject.TryGetComponent<I_ObjectSelect>(out obj_select))
                    obj_select.Deselect();

                actObject = null;
                actReady = false;
                actTag = null;
            }
        }
        else if (actReady && actObject != null)
        {
            if (actObject.TryGetComponent<I_ObjectSelect>(out obj_select))
                obj_select.Deselect();

            actObject = null;
            actReady = false;
            actTag = null;
        }

        CrossHair.get.SelectHair();

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, 20, sightLayerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            
            if (hits[i].transform != null)
            {
                Scholar scholar = hits[i].transform.parent.GetComponentInChildren<Scholar>();
                scholar.Senses.ISeeYou();
            }
        }
        
    }



    private float LookingAngle(Vector3 lookingTo, Transform Who)
    {
        lookingTo.y = Who.transform.position.y;
        lookingTo = lookingTo - Who.transform.position;
        return Vector3.Angle(lookingTo, Who.forward);
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
                    if (LookingAngle(transform.position, actObject.transform) < 70)
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
            if (Probability(0.5))
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
        if (!act && Probability(0.1))
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
    


    //Вероятность

    public bool Probability(double a)
    {
        rnd = Random.value;

        if (a >= rnd)
            return true;
        else
            return false;
    }


    public void Draw(bool option)
    {
        draw = option;
        draw_option = true;
    }

    public void UnDraw(bool option)
    {
        draw = option;
        draw_option = false;
    }


    private void Drawing(bool option)
    {
        if (actTag == "DeskBlock")
            actObject.GetComponent<DeskBlock>().Draw(option);
    }


    private IEnumerator StartExam()
    {
        yield return new WaitForEndOfFrame();

        ExamManager.get.StartExam();
    }
}
