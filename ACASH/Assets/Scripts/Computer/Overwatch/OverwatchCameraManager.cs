using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class OverwatchCameraManager : Singleton<OverwatchCameraManager>
{
    private Camera[] cameras;
    [SerializeField]
    private RenderTexture texture;
    private int index = -1;


    public void SetLevel()
    {
        GameObject[] buf = GameObject.FindGameObjectsWithTag("OverwatchCamera");

        cameras = new Camera[buf.Length];

        for(int i = 0; i< cameras.Length; i++)
        {
            cameras[i] = buf[i].GetComponent<Camera>();
        }

        SetCamera(0);
    }

    private void SetCamera(int index)
    {
        if (this.index != -1)
            cameras[this.index].targetTexture = null;   

        this.index = index;
        cameras[this.index].targetTexture = texture;
    }




}
