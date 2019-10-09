using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Управление
    private string horizontalInputName;
    private string verticalInputName;
    [SerializeField]
    private float movementSpeed = 6;
    private double rnd;
    private CharacterController CharController;
    private LayerMask actLayerMask;
    private LayerMask sightLayerMask;
    private InputManager InputMan;
    private CameraController CamControl;
    private bool disPlayer;
    [HideInInspector]
    public bool look_closer;

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
    private float actMaxRange = 1f;
    private float actMinRange = 0.4f;
    [HideInInspector]
    public string actTag;
    private GameObject actObject;
    private GameObject selected_scholar;
    private string actText;
    [HideInInspector]
    public GameObject playerCam;
    private SubtitleManager SubMan;
    private ScriptManager ScriptMan;
    private ScholarManager ScholarMan;
    private ScoreSystem Score;
    private string keyWord = "Teacher_";
    private string key;



    private void Awake()
    {
        playerCam = GameObject.FindWithTag("PlayerCamera");
        horizontalInputName = "Horizontal";
        verticalInputName = "Vertical";
        InputMan = GameObject.FindObjectOfType<InputManager>();
        Score = GameObject.FindObjectOfType<ScoreSystem>();
        CharController = GetComponent<CharacterController>();
        CamControl = playerCam.GetComponent<CameraController>();
        SubMan = GameObject.FindObjectOfType<SubtitleManager>();
        ScriptMan = GameObject.FindObjectOfType<ScriptManager>();
        ScholarMan = GameObject.FindObjectOfType<ScholarManager>();
    }

    private void Start()
    {
        actLayerMask = LayerMask.GetMask("Selectable");
        sightLayerMask = LayerMask.GetMask("Sight Layer");
    }

    private void Update()
    {
        if(!disPlayer)
        {
            PlayerMovement();
            Action();
        }
            
        if(!act)
            Watching();
    }



    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;
        if (forwardMovement != Vector3.zero || rightMovement != Vector3.zero)
        {
            CharController.SimpleMove(forwardMovement + rightMovement);
            ScholarMan.Hear(3);
        }
    }


    private void Watching()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, actMaxRange, actLayerMask))
        {
            if (hit.collider.tag == "Scholar")
            {
                if (actObject != hit.collider.gameObject)
                {
                    if (actObject != null)
                    {
                        actObject.GetComponent<ObjectSelect>().Deselect();
                    }
                    actObject = hit.collider.gameObject;
                    actTag = hit.collider.tag;
                    actReady = true;
                    actObject.GetComponent<ObjectSelect>().Select();

                    WhatISee();
                }
            }
            else if((hit.transform.position - transform.position).magnitude < actMinRange)
            {
                if (actObject != hit.collider.gameObject)
                {
                    if (actObject != null)
                    {
                        actObject.GetComponent<ObjectSelect>().Deselect();
                    }
                    actObject = hit.collider.gameObject;
                    actTag = hit.collider.tag;
                    actReady = true;
                    actObject.GetComponent<ObjectSelect>().Select();
                }
            }
            else if (actReady && actObject != null)
            {
                actObject.GetComponent<ObjectSelect>().Deselect();
                actObject = null;
                actReady = false;
            }
        }
        else if (actReady && actObject != null)
        {
            actObject.GetComponent<ObjectSelect>().Deselect();
            actObject = null;
            actReady = false;
        }

        

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, 20, sightLayerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            
            if (hits[i].transform != null)
            {
                Scholar obj = hits[i].transform.parent.GetComponentInChildren<Scholar>();
                obj.T_look_at_us = true;
            }
        }
        
    }



    private void WhatISee()
    {

        if (actObject.GetComponent<Scholar>().asking)
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
        if (doing && actReady && !act)
        {
            Debug.Log("Мы хотим что-то сделать");

            switch (actTag)
            {
                case "Computer":
                    {
                        DisableControl(true);
                        //actObject.GetComponent<Computer_Power>().SwitchPower();
                        Computer_Power.SwitchPower(actObject.transform.GetChild(0).gameObject);
                        break;
                    }
                case "Door":
                    {
                        actObject.GetComponent<Door>().DoorInteract(transform.position);
                        break;
                    }
            }

            act = true;
        }
    }






    public void Shout()
    {
        StartCoroutine(Shouting());
    }

    private IEnumerator Shouting()
    {
        StopThinking();
        ScholarMan.Stress(10);
        act = true;
        key = "Shout_";
        int nomber = Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        key += nomber;
        SubMan.Say(keyWord + key);
        yield return new WaitForSeconds(1f);
        while (SubMan.act)
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
        key += scholar.questionKey;

        if (answer)
            key += "_Yes";
        else
            key += "_No";

        SubMan.Say(keyWord + key);

        yield return new WaitForSeconds(1f);

        scholar.HearBulling(!answer);

        while (SubMan.act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        scholar.TeacherAnswer(answer);
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
        Score.BullScore(scholar, strong);

        key += scholar.GetView();

        if (scholar.remarks[scholar.GetView()])
        {
            if (Probability(0.5))
                key += "Sec_";
        }
        else
            scholar.remarks[scholar.GetView()] = true;

        int nomber = Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        key += nomber;
        SubMan.Say(keyWord + key);

        yield return new WaitForSeconds(1f);

        scholar.HearBulling(strong);


        while (SubMan.act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        scholar.Bulling(key, strong);
        act = false;

        while (scholar.TextBox.IsTalking() && !act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //Добавить вероятность + взгляд
        if (!act && Probability(0.1))
            SubMan.Say(keyWord + "Thinking_" + scholar.tag + "_" + Random.Range(0, ScriptMan.linesQuantity[keyWord + "Thinking_"]));
    }



    //========================================================================================================
    //Наезд на школьника


    public void Execute()
    {
        StopThinking();
        act = true;
        string goalTag = actTag;
        GameObject goalObject = actObject;

        key = "Execute_";


        switch (goalTag)
        {
            case "Scholar":
                {
                    var scholar = goalObject.GetComponent<Scholar>();
                    if (!scholar.executed)
                        StartCoroutine(Execute(scholar));
                    break;
                }
            case "ScholarsSubject":
                {
                    var subject = goalObject.GetComponent<ScholarSubject>();
                    StartCoroutine(Execute(subject));
                    break;
                }
            case "Subject":
                {
                    var subject = goalObject.GetComponent<Subject>();
                    StartCoroutine(Execute(subject));
                    break;
                }
            default:
                {
                    act = false;
                    break;
                }
        }
    }




    private IEnumerator Execute(Scholar scholar)
    {

        int nomber = Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        key += nomber;

        SubMan.Say(keyWord + key);

        yield return new WaitForSeconds(1f);

        scholar.HearBulling(true);

        yield return new WaitForSeconds(1f);

        scholar.Execute(key);

        while (!scholar.executed && !act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        act = false;

        SubMan.Say(keyWord + "Thinking_" + key);
    }


    private IEnumerator Execute(ScholarSubject subject)
    {
        key += subject.name + "_";
        key += Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        SubMan.Say(keyWord + key);
        subject.Execute(key);

        yield return new WaitForSeconds(1f);

        while (SubMan.act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        act = false;
    }

    private IEnumerator Execute(Subject subject)
    {
        key += subject.name + "_";
        key += Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        SubMan.Say(keyWord + key);
        subject.Execute();

        yield return new WaitForSeconds(1f);

        while (SubMan.act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        act = false;
    }


    private void StopThinking()
    {
        if(SubMan.act)
            SubMan.StopSubtitile();
    }

    public void DisableControl(bool status)
    {
        disPlayer = status;
        InputMan.disPlayer = status;
        playerCam.GetComponent<CameraController>().disPlayer = status;
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
}
