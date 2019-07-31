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
    private string key;
    private string typeOfObj;
    private bool thisIsObj;



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

            switch (goalTag)
            {
                case "Pen":
                    {
                        typeOfObj = goalTag + "_";
                        key += typeOfObj;
                        goalObject = goalObject.transform.parent.transform.parent.gameObject;
                        goalTag = goalObject.tag;
                        thisIsObj = true;
                        break;
                    }
            }


            //Наезды абсолютно одинаковые, switch тут для того, чтобы обращаться к разным скриптам.
            switch (goalTag)
            {
                case "Asshole":
                    {
                        var scholar = goalObject.GetComponent<Asshole>();
                        if (thisIsObj)
                            StartCoroutine(BullingForAction(scholar, strong));
                        else
                            StartCoroutine(BullingForAction(scholar, strong));
                        break;
                    }
                case "Dumb":
                    {
                        var scholar = goalObject.GetComponent<Dumb>();
                        if (thisIsObj)
                            StartCoroutine(BullingForAction(scholar, strong));
                        else
                            StartCoroutine(BullingForAction(scholar, strong));
                        break;
                    }
    
            }
        }

    }


    //Наезд на мудака
    public IEnumerator BullingForAction(Asshole scholar, bool strong)
    {
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
        if (!act && scholar.Probability(0.1))
            SubMan.PlaySubtitle(keyWord + "Thinking_" + scholar.tag + "_" + Random.Range(0, ScriptMan.linesQuantity[keyWord + "Thinking_"]));
    }


    //Наезд на тупицу
    public IEnumerator BullingForAction(Dumb scholar, bool strong)
    {
        key += scholar.view;

        if (scholar.remarks[scholar.view])
            key += "Sec_";
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
        if (!act && scholar.Probability(0.1))
            SubMan.PlaySubtitle(keyWord + "Thinking_" + scholar.tag + "_" + Random.Range(0, ScriptMan.linesQuantity[keyWord + "Thinking_"]));
    }

    public IEnumerator BullingForObject(Asshole scholar, bool strong)
    {

        if (scholar.remarks[typeOfObj])
            key += "Sec_";
        else
            scholar.remarks[typeOfObj] = true;

        int nomber = Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        key += nomber;

        SubMan.PlaySubtitle(keyWord + key);
        yield return new WaitForSeconds(1f);

        while (SubMan.act)
        {
            yield return new WaitForSeconds(0.1f);
        }
        scholar.Bulling(key, strong, typeOfObj);
        act = false;

        while (scholar.TextBox.IsTalking() && !act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //Добавить вероятность + взгляд
        if (!act && scholar.Probability(0.1))
            SubMan.PlaySubtitle(keyWord + "Thinking_" + scholar.tag + "_" + Random.Range(0, ScriptMan.linesQuantity[keyWord + "Thinking_"]));
    }

    public IEnumerator BullingForObject(Dumb scholar, bool strong)
    {

        if (scholar.remarks[typeOfObj])
            key += "Sec_";
        else
            scholar.remarks[typeOfObj] = true;

        int nomber = Random.Range(0, ScriptMan.linesQuantity[keyWord + key]);
        key += nomber;

        SubMan.PlaySubtitle(keyWord + key);
        yield return new WaitForSeconds(1f);

        while (SubMan.act)
        {
            yield return new WaitForSeconds(0.1f);
        }
        scholar.Bulling(key, strong, typeOfObj);
        act = false;

        while (scholar.TextBox.IsTalking() && !act)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //Добавить вероятность + взгляд
        if (!act && scholar.Probability(0.1))
            SubMan.PlaySubtitle(keyWord + "Thinking_" + scholar.tag + "_" + Random.Range(0, ScriptMan.linesQuantity[keyWord + "Thinking_"]));
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
