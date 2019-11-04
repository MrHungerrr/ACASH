﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using N_BH;

public class PlayerCamera : Singleton<PlayerCamera>
{
    private float coefSensitivity = 5;
    private const float mouseSensitivity = 2;
    private const float gamepadSensitivity = 35; 

    private Transform playerBody;
    private CinemachineVirtualCamera cineCam;
    private float xAxisClamp;
    [HideInInspector]
    public Vector2 rotateInput;
    private Vector2 rotate;
    [HideInInspector]
    public bool zoom;
    [HideInInspector]
    public bool zooming;
    private float normFOV = 70;
    private float zoomFOV = 15;
    private float FOV;
    [HideInInspector]
    public bool disPlayer = false;



    private void Awake()
    {
        disPlayer = false;
        LockCursor(true);
        xAxisClamp = 0.0f;
        
        cineCam = GetComponent<CinemachineVirtualCamera>();
        cineCam.m_Lens.FieldOfView = normFOV;
    }

    private void Start()
    {
        playerBody = GameObject.Find("Player").transform;
    }


    public void LockCursor(bool status)
    {
        if (status)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (!disPlayer)
        {
            CameraRotation();
            if (zoom || zooming)
                Zoom();
        }
    }

    private void CameraRotation()
    {

        switch (InputManager.get.inputType)
        {
            case "keyboard":
                {
                    rotate = rotateInput * Time.deltaTime * (mouseSensitivity + (mouseSensitivity * coefSensitivity));
                    break;
                }
            default:
                {
                    rotate = rotateInput * Time.deltaTime * (gamepadSensitivity + (gamepadSensitivity * coefSensitivity));
                    break;
                }
        }




        xAxisClamp += rotate.y;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            rotate.y = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            rotate.y = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * rotate.y);
        playerBody.Rotate(Vector3.up * rotate.x);
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


        if((normFOV - FOV) < 0.01f)
        {
            zooming = false;
        }
    }

    public void Sensitivity(int coef)
    {
        coefSensitivity = coef;
    }
}
