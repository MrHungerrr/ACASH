using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Single;

public class PlayerCamera : MonoBehaviour
{
    private CinemachineVirtualCamera _cineCam;

    private const float MOUSE_SENSITIVITY = 1f;
    private const float GAMEPAD_SENSITIVITY = 8f;
    private const float NORM_FOV = 70;
    private const float ZOOM_FOV = 15;

    private Vector2 _rotateInput;
    private Vector2 _rotate;
    private float _xAxisClamp;

    private bool _zoom;
    private bool _zooming;

    private float _FOV;


    public void Setup(Player Player)
    {
        LockCursor(true);
        _xAxisClamp = 0.0f;

        _cineCam = GetComponent<CinemachineVirtualCamera>();
        _cineCam.m_Lens.FieldOfView = NORM_FOV;
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
        if (_zoom || _zooming)
            Zoom();
    }

    private void CameraRotation()
    {
        if (_rotateInput != Vector2.zero)
        {
            RotateCalculate();

            _xAxisClamp += _rotate.y;

            if (_xAxisClamp > 90.0f)
            {
                _xAxisClamp = 90.0f;
                _rotate.y = 0.0f;
                ClampXAxisRotationToValue(270.0f);
            }
            else if (_xAxisClamp < -90.0f)
            {
                _xAxisClamp = -90.0f;
                _rotate.y = 0.0f;
                ClampXAxisRotationToValue(90.0f);
            }

            transform.Rotate(Vector3.left * _rotate.y);
            Player.Instance.Move.AddRotateAngle(_rotate.x);
        }
    }

    private void RotateCalculate()
    {
        switch (InputManager.InputType)
        {
            case InputManager.Input.Keyboard:
                {
                    _rotate = _rotateInput * Time.deltaTime * (MOUSE_SENSITIVITY + (MOUSE_SENSITIVITY * PlayerSettings.SensetivityCoef));
                    break;
                }
            default:
                {
                    _rotate = _rotateInput * Time.deltaTime * (GAMEPAD_SENSITIVITY + (GAMEPAD_SENSITIVITY * PlayerSettings.SensetivityCoef));
                    break;
                }
        }
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }



    public void RotateInput(Vector2 input)
    {
        _rotateInput = input;
    }


    public void Zoom(bool option)
    {
        _zoom = option;
        _zooming = true;

        if (option)
            Player.Instance.Select.Disable(this.GetType());
        else
            Player.Instance.Select.Enable(this.GetType());

    }




    private void Zoom()
    {
        if(_zoom)
        {
            _FOV = Mathf.Lerp(_cineCam.m_Lens.FieldOfView, ZOOM_FOV, 4 * Time.deltaTime);
        }
        else
        {
            _FOV = Mathf.Lerp(_cineCam.m_Lens.FieldOfView, NORM_FOV, 4 * Time.deltaTime);
        }
        _cineCam.m_Lens.FieldOfView = _FOV;


        if((NORM_FOV - _FOV) < 0.01f)
        {
            _zooming = false;
        }
    }

    public void Enable(bool option)
    {
        _cineCam.enabled = option;
    }
}
