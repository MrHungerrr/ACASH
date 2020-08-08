using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class OverwatchCameraManager : MonoSingleton<OverwatchCameraManager>
{
    public OverwatchCamera[] CamerasHolders => _camerasHolders;
    public RenderTexture renderTextureReference => _reference;


    private OverwatchCamera[] _camerasHolders;
    [SerializeField]
    private RenderTexture _reference;


    public void SetLevel()
    {
        _camerasHolders = GameObject.FindObjectsOfType<OverwatchCamera>();

        if (_camerasHolders != null)
        {
            for (int i = 0; i < _camerasHolders.Length; i++)
            {
                _camerasHolders[i].Setup();
            }
        }
    }

    public RenderTexture GetTexture(int camera)
    {
        return _camerasHolders[camera].Texture;
    }


    public int GetCameraNumber(int camera)
    {
        if (camera < 0)
        {
            return _camerasHolders.Length - 1;
        }

        if (camera >= _camerasHolders.Length)
        {
            return 0;
        }

        return camera;
    }




}
