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

    private void Awake()
    {
        playerCam = GameObject.FindWithTag("PlayerCamera");
        horizontalInputName = "Horizontal";
        verticalInputName = "Vertical";
        inManager = GetComponent<InputManager>();
        charController = GetComponent<CharacterController>();
        camControl = playerCam.GetComponent<CameraController>();
        SubMan = GameObject.FindObjectOfType<SubtitleManager>();
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

        if (Physics.Raycast(ray, out hit, actRange, actLayerMask) && !act)
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
        SubMan.PlaySubtitle("big-red-button");
    }

   public void DisableControl(bool status)
    {
        inManager.disPlayer = status;
        playerCam.SetActive(!status);
    }
}
