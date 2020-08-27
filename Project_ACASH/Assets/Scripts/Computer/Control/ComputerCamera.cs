using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;
using Computers;


public class ComputerCamera : MonoBehaviour
{
    public bool Control => _zoom;


    private const float Y_DELTA = 10;
    private const float X_DELTA = 15;
    private const float NORM_FOV = 60;
    private const float ZOOM_FOV = 20;

    [SerializeField] private CinemachineVirtualCamera _camera;

    private bool _zoom;
    private bool _zooming;
    private bool _moving;

    private Quaternion _normRotation;
    private float _FOV;


    public void  SetLevel()
    {
        _normRotation = _camera.transform.rotation;

        _zoom = false;
        _moving = false;
        _zooming = false;
    }



    public void Enable(bool option)
    {
        if (option)
        {
            _camera.transform.rotation = _normRotation;
            _FOV = NORM_FOV;
            _camera.m_Lens.FieldOfView = _FOV;
        }
        else
        {
            _moving = false;
            _zooming = false;
        }

        Player.Instance.Camera.Enable(!option);
        _camera.enabled = option;
    }


    public void MyUpdate()
    {
        if (_zoom || _zooming)
            Zoom();
        if (!_zoom && _moving)
            CameraMoveToOrigin();
    }


    public void Move(Vector3 movement)
    {
        _camera.transform.Rotate(movement);

        if (Mathf.Abs(_camera.transform.localEulerAngles.y - 180) > Y_DELTA)
        {
            ClampYAxisRotationToValue(180 + Mathf.Sign(_camera.transform.localEulerAngles.y - 180) * Y_DELTA);
        }

        if (_camera.transform.localEulerAngles.x > X_DELTA && _camera.transform.localEulerAngles.x < (360 - X_DELTA))
        {
            ClampXAxisRotationToValue((((int)_camera.transform.localEulerAngles.x / 180) * -(X_DELTA * 2)) + X_DELTA);
        }

        ClampZAxisRotationToValue(0);
    }

    public void SetZoom(bool option)
    {
        if (option)
        {
            _zoom = true;
            _zooming = true;
            _moving = true;
        }
        else
        {
            _zoom = false;
        }
    }


    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = _camera.transform.localEulerAngles;
        eulerRotation.x = value;
        _camera.transform.localEulerAngles = eulerRotation;
    }

    private void ClampYAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = _camera.transform.localEulerAngles;
        eulerRotation.y = value;
        _camera.transform.localEulerAngles = eulerRotation;
    }

    private void ClampZAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = _camera.transform.localEulerAngles;
        eulerRotation.z = value;
        _camera.transform.localEulerAngles = eulerRotation;
    }


    private void CameraMoveToOrigin()
    {    
        _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, _normRotation, 4f * Time.deltaTime);

        if (_camera.transform.rotation == _normRotation)
        {
            _moving = false;
        }
    }



    private void Zoom()
    {
        if (_zoom)
        {
            _FOV = Mathf.Lerp(_camera.m_Lens.FieldOfView, ZOOM_FOV, 4 * Time.deltaTime);
        }
        else
        {
            _FOV = Mathf.Lerp(_camera.m_Lens.FieldOfView, NORM_FOV, 4 * Time.deltaTime);
        }
        _camera.m_Lens.FieldOfView = _FOV;


        if ((NORM_FOV - _FOV) < 0.01f)
        {
            _zooming = false;
        }
    }
}
