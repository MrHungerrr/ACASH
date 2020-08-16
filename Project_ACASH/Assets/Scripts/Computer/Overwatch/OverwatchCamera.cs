using System;
using UnityEngine;

public class OverwatchCamera : MonoBehaviour
#region IInitialization
#if UNITY_EDITOR
    , IInitialization
#endif
#endregion
{

    public string Name => _name;
    public RenderTexture Texture => _texture;


    [SerializeField]
    private string _name;
    [SerializeField]
    private Camera _camera;

    private RenderTexture _texture;


    #region Initialization
#if UNITY_EDITOR

    public bool AutoInitializate => true;

    public void Initializate()
    {
        _camera = GetComponent<Camera>();

        if (String.IsNullOrEmpty(_name))
        {
            throw new Exception("Пустое имя камеры");
        }
    }

#endif
    #endregion

    public void Setup()
    {
        _texture = new RenderTexture(OverwatchCameraManager.Instance.renderTextureReference);
        _camera.targetTexture = _texture;
    }
}
