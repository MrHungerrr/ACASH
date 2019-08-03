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
                case "Dumb":
                    {
                        var scholar = goalObject.GetComponent<Dumb>();
                        if (!scholar.executed)
                            StartCoroutine(BullingForAction(scholar, strong));
                        break;
                    }
                default:
                    {
                        act = false;
                        break;
                    }
    
            }
        }

    }



    //--------------------------------------------------------------------------------------------------------
    //Осторожно, разновидности одного и того же кода. Отличаются лишь скриптами, к которым обращаются.
    //--------------------------------------------------------------------------------------------------------



    //Наезд на мудака



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


            switch (goalTag)
            {
                case "Dumb":
                    {
                        var scholar = goalObject.GetComponent<Dumb>();
                        if (!scholar.executed)
                            StartCoroutine(Execute(scholar));
                        break;
                    }
                case "ScholarsSubject":
                    {
                        var subject = goalObject.GetComponent<ScholarsSubject>();
                        Debug.Log("Fuck");
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
    }




    private IEnumerator Execute(Dumb scholar)
    {

        int nomber = Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        key += nomber;

        SubMan.PlaySubtitle(keyWord + key);

        yield return new WaitForSeconds(1f);

        scholar.HearBulling(true);

        yield return new WaitForSeconds(1f);

        scholar.Execute(key);

        while (!scholar.executed && !act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        act = false;

        SubMan.PlaySubtitle(keyWord + "Thinking_" + key);
    }


    private IEnumerator Execute(ScholarsSubject subject)
    {
        key += subject.name;
        key += Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        SubMan.PlaySubtitle(keyWord + key);
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
        key += subject.name;
        key += Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        SubMan.PlaySubtitle(keyWord + key);
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
