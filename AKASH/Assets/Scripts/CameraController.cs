using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private string mouseXInputName, mouseYInputName;
    private float mouseSensitivity = 100;

    private Transform playerBody;
    private CinemachineVirtualCamera cineCam;
    private float xAxisClamp;
    [HideInInspector]
    public bool zoom;
    [HideInInspector]
    public bool zooming;
    private float normFOV = 50;
    private float zoomFOV = 15;
    private float FOV;



    private void Awake()
    {
        mouseXInputName = "Mouse X";
        mouseYInputName = "Mouse Y";
        LockCursor();
        xAxisClamp = 0.0f;
        
        cineCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        playerBody = GameObject.Find("Player").transform;
    }


    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraRotation();
        if (zoom || zooming)
            Zoom();
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }

    private void Zoom()
    {
        if(zoom)
        {
            FOV = Mathf.Lerp(cineCam.m_Lens.FieldOfView, zoomFOV, 4 * Time.deltaTime);
        }
        else
        {
            FOV = Mathf.Lerp(cineCam.m_Lens.FieldOfView, normFOV, 4 * Time.deltaTime);
        }
        cineCam.m_Lens.FieldOfView = FOV;


        if((normFOV - FOV) < 0.1f)
        {
            zooming = false;
        }
    }
}
