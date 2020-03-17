using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Single;

public class PlayerCamera : MonoBehaviour
{
    private float coefSensitivity = 5;
    private const float mouseSensitivity = 2;
    private const float gamepadSensitivity = 35; 

    private Player Player;
    private CinemachineVirtualCamera cineCam;
    private float xAxisClamp;
    [HideInInspector]
    public Vector2 rotateInput;
    private Vector2 rotate;
    [HideInInspector]
    public bool zoom { get; private set; } = false;
    private bool zooming;
    private float normFOV = 70;
    private float zoomFOV = 15;
    private float FOV;




    public void Setup(Player Player)
    {
        LockCursor(true);
        xAxisClamp = 0.0f;

        cineCam = GetComponent<CinemachineVirtualCamera>();
        cineCam.m_Lens.FieldOfView = normFOV;

        this.Player = Player;
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
        CameraRotation();
        if (zoom || zooming)
            Zoom();
    }

    private void CameraRotation()
    {
        if (rotateInput != Vector2.zero)
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
            Player.Move.rotateAngle += rotate.x;
        }
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }



    public void Zoom(bool option)
    {
        zoom = option;
        zooming = true;

        if (option)
            Player.Select.Disable(this.GetType());
        else
            Player.Select.Enable(this.GetType());

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

    public void Enable(bool option)
    {
        cineCam.enabled = option;
    }
}
