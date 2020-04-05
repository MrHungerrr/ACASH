using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class OverwatchCameraManager : Singleton<OverwatchCameraManager>
{
    private Camera camera;
    [SerializeField]
    private RenderTexture texture;


    public void SetLevel()
    {
        camera = GameObject.FindGameObjectWithTag("OverwatchCamera").GetComponent<Camera>();
        camera.targetTexture = texture;
    }





}
