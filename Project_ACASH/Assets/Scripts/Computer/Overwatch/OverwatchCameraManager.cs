using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class OverwatchCameraManager : Singleton<OverwatchCameraManager>
{
    private Camera[] cameras;
    [SerializeField]
    private RenderTexture reference;
    private RenderTexture[] textures;
    [HideInInspector]
    public int count;


    public void SetLevel()
    {
        GameObject[] buf = GameObject.FindGameObjectsWithTag("OverwatchCamera");

        count = buf.Length;

        cameras = new Camera[count];
        textures = new RenderTexture[count];


        for (int i = 0; i < count; i++)
        {
            cameras[i] = buf[i].GetComponent<Camera>();
            textures[i] = new RenderTexture(reference);
            cameras[i].targetTexture = textures[i];
        }
    }

    public RenderTexture GetTexture(int camera)
    {
        return textures[camera];
    }


    public int GetCameraNumber(int camera)
    {
        if (camera < 0)
        {
            return count - 1;
        }

        if (camera >= count)
        {
            return 0;
        }

        return camera;
    }




}
