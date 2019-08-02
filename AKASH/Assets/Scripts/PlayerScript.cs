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
    private CharacterController charController;
    private LayerMask actLayerMask;
    private InputManager inManager;
    private CameraController camControl;
    private bool disPlayer;

    //Действия
    [HideInInspector]
    public bool doing;
    private bool think;
    [HideInInspector]
    public bool act;
    private bool actReady;
    private float actRange = 4f;
    private string actTag;
    private GameObject actObject;
    private string actText;
    [HideInInspector]
    public GameObject playerCam;
    private SubtitleManager SubMan;
    private ScriptManager ScriptMan;
    private string keyWord = "Teacher_";
    private string key;
    private bool objectIsGoal;
    private GameObject exObject;



    private void Awake()
    {
        playerCam = GameObject.FindWithTag("PlayerCamera");
        horizontalInputName = "Horizontal";
        verticalInputName = "Vertical";
        inManager = GetComponent<InputManager>();
        charController = GetComponent<CharacterController>();
        camControl = playerCam.GetComponent<CameraController>();
        SubMan = GameObject.FindObjectOfType<SubtitleManager>();
        ScriptMan = GameObject.FindObjectOfType<ScriptManager>();
    }

    private void Start()
    {
        actLayerMask = LayerMask.GetMask("Selectable");
    }

    private void Update()
    {
        PlayerMovement();
        Watching();
        Action();
    }


    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(forwardMovement + rightMovement);
    }



    private void Watching()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, actRange, actLayerMask))
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




    private void Action()
    {
        if (doing && actReady && !act)
        {
            Debug.Log("Мы хотим что-то сделать");

            switch (actTag)
            {
                case "Computer":
                    {
                        //actObject.GetComponent<Computer>().Enable();
                        break;
                    }
                case "HandCamera":
                    {

                        break;
                    }
            }


            act = true;
        }
    }




    public void Shout()
    {
        if (!act)
            StartCoroutine(Shouting());
    }

    public IEnumerator Shouting()
    {
        StopThinking();
        act = true;
        key = "Shout_";
        int nomber = Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        key += nomber;
        SubMan.PlaySubtitle(keyWord + key);
        yield return new WaitForSeconds(1f);
        Debug.Log(act);
        while (SubMan.act)
        {
            Debug.Log(act);
            yield return new WaitForSeconds(0.1f);
        }

        act = false;
    }




    public void Bull(bool strong)
    {
        if (!act && actReady)
        {
            StopThinking();
            act = true;
            string goalTag = actTag;
            GameObject goalObject = actObject;

            if (strong)
                key = "Bull_";
            else
                key = "Joke_";


            //Наезды абсолютно одинаковые, switch тут для того, чтобы обращаться к разным скриптам.
            switch (goalTag)
            {
                case "Asshole":
                    {
                        var scholar = goalObject.GetComponent<Asshole>();
                        StartCoroutine(BullingForAction(scholar, strong));
                        break;
                    }
                case "Dumb":
                    {
                        var scholar = goalObject.GetComponent<Dumb>();
                        StartCoroutine(BullingForAction(scholar, strong));
                        break;
                    }
    
            }
        }

    }



    //--------------------------------------------------------------------------------------------------------
    //Осторожно, разновидности одного и того же кода. Отличаются лишь скриптами, к которым обращаются.
    //--------------------------------------------------------------------------------------------------------



    //Наезд на мудака

    public IEnumerator BullingForAction(Asshole scholar, bool strong)
    {
        key += scholar.view;

        if (scholar.remarks[scholar.view])
        {
            if (Probability(0.5))
                key += "Sec_";
        }
        else
            scholar.remarks[scholar.view] = true;

        int nomber = Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        key += nomber;
        SubMan.PlaySubtitle(keyWord + key);

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
            SubMan.PlaySubtitle(keyWord + "Thinking_" + scholar.tag + "_" + Random.Range(0, ScriptMan.linesQuantity[keyWord + "Thinking_"]));
    }



    //Наезд на тупицу

    public IEnumerator BullingForAction(Dumb scholar, bool strong)
    {
        key += scholar.view;

        if (scholar.remarks[scholar.view])
        {
            if (Probability(0.5))
                key += "Sec_";
        }
        else
            scholar.remarks[scholar.view] = true;

        int nomber = Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        key += nomber;
        SubMan.PlaySubtitle(keyWord + key);

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
            SubMan.PlaySubtitle(keyWord + "Thinking_" + scholar.tag + "_" + Random.Range(0, ScriptMan.linesQuantity[keyWord + "Thinking_"]));
    }



    //--------------------------------------------------------------------------------------------------------
    //Конец разновидностей одного и того же кода.
    //--------------------------------------------------------------------------------------------------------


    public void Execute()
    {
        if (!act && actReady)
        {
            StopThinking();
            act = true;
            string goalTag = actTag;
            GameObject goalObject = actObject;

            key = "Execute_";

            if(goalTag == "ScholarsObject")
            {
                //Че-то еще про предмет.
                key += goalObject.name + "_";
                exObject = goalObject;
                goalObject = goalObject.GetComponent<ScholarsObject>().owner;
                goalTag = goalObject.tag;
                objectIsGoal = true;
            }
            else
            {
                objectIsGoal = false;
                //Реализовать выбор(За что выгонять?) Через execute = true  и замедление времени
            }

            //Добавить удаление предметов не школьника


            switch (goalTag)
            {
                case "Asshole":
                    {
                        break;
                    }
                case "Dumb":
                    {
                        var scholar = goalObject.GetComponent<Dumb>();
                        StartCoroutine(Execute(scholar));
                        break;
                    }

            }
        }
    }




    public IEnumerator Execute(Dumb scholar)
    {

        int nomber = Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        key += nomber;
        SubMan.PlaySubtitle(keyWord + key);

        yield return new WaitForSeconds(1f);

        scholar.HearBulling(true);

        while (SubMan.act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (objectIsGoal)
        {
            exObject.GetComponent<ScholarsObject>().Execute();
            scholar.Bulling(key, true);
        }
        else
        {
            scholar.Execute(key);
        }

        act = false;

        while (scholar.TextBox.IsTalking() && !act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        SubMan.PlaySubtitle(keyWord + "Thinking_" + key);
    }



    private void StopThinking()
    {
        if(SubMan.act)
            SubMan.StopSubtitile();
    }

    public void DisableControl(bool status)
    {
        inManager.disPlayer = status;
        playerCam.SetActive(!status);
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
