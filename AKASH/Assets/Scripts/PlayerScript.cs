using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Управление
    private string horizontalInputName;
    private string verticalInputName;
    [SerializeField]
    private float movementSpeed = 6;
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
    public SubtitleManager SubMan;
    private ScriptManager ScriptMan;
    private string keyWord = "Teacher_";



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
            /*
            if (actTag == "Note")
            {
                Debug.Log("Читаем записку");
                actObject.GetComponent<Note>().StartLook();
            }

            if(actTag == "CodeLock")
            {
                actObject.GetComponentInParent<CodeLock>().SetValue(actObject.name);
            }

            if (actTag == "Door")
            {
                actObject.GetComponent<Door>().DoorInteract(transform.rotation.eulerAngles.y);
            }
            */

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
        string key = "Shout_";
        int nomber = Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        key += nomber;
        SubMan.PlaySubtitle(keyWord + key);
        yield return new WaitForSeconds(1f);
        Debug.Log(act);
        while (SubMan.act)
        {
            Debug.Log(act);
            yield return new WaitForSeconds(1f);
        }

        act = false;
    }

    public void Bull(bool strong)
    {
        if (!act && actReady)
            if(actTag == "Asshole" || actTag == "Dumb")
                StartCoroutine(Bulling(strong));
    }

    public IEnumerator Bulling(bool strong)
    {
        StopThinking();
        act = true;
        string key;

        if (strong)
            key = "Bull_";
        else
            key = "Joke_";


        switch (actTag)
        {
            case "Asshole":
                {
                    //Debug.Log("Орем на мудака");
                    var scholar = actObject.GetComponent<Asshole>();
                    key += scholar.view;
                    if (scholar.remarks[scholar.view])
                        key += "Sec_";
                    else
                        scholar.remarks[scholar.view] = true;

                    int nomber = Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
                    key += nomber;

                    SubMan.PlaySubtitle(keyWord + key);
                    yield return new WaitForSeconds(1f);

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
                    if (!act)
                        SubMan.PlaySubtitle(keyWord + "Thinking_" + scholar.tag + "_" + Random.Range(0, ScriptMan.linesQuantity[keyWord + "Thinking_"]));

                    break;
                }
            case "Dumb":
                {
                    //Debug.Log("Орем на тупицу");
                    var scholar = actObject.GetComponent<Dumb>();
                    key += scholar.view;
                    if (scholar.remarks[scholar.view])
                        key += "Sec_";
                    else
                        scholar.remarks[scholar.view] = true;

                    int nomber = Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
                    key += nomber;

                    SubMan.PlaySubtitle(keyWord + key);
                    yield return new WaitForSeconds(1f);

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
                    if (!act)
                        SubMan.PlaySubtitle(keyWord + "Thinking_" + scholar.tag + "_" + Random.Range(0, ScriptMan.linesQuantity[keyWord + "Thinking_"]));

                    break;
                }
        }



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
}
