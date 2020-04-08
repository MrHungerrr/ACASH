using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Single;

public class OverwatchCameraController: MonoBehaviour
{
    RawImage image;
    private int index = -1;


    public void Setup()
    {
        image = transform.Find("Overwatch").GetComponentInChildren<RawImage>();

        if(OverwatchCameraManager.get.count == 0)
        {
            transform.Find("Overwatch").Find("Camera Right").gameObject.SetActive(false);
            transform.Find("Overwatch").Find("Camera Left").gameObject.SetActive(false);
        }

        SetCamera(0);
    }

    private void SetCamera(int index)
    {
        index = OverwatchCameraManager.get.GetCameraNumber(index);
        image.texture = OverwatchCameraManager.get.GetTexture(index);
        this.index = index;
    }


    public void ChangeCameraRight()
    {
        SetCamera(index + 1);
    }

    public void ChangeCameraLeft()
    {
        SetCamera(index - 1);
    }

}
