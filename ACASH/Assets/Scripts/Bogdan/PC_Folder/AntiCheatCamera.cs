using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiCheatCamera : MonoBehaviour
{

    private GameObject playerCam;
    public GameObject[] anti_cheat_cameras;
    public static int i = 0;
    public Lable_Script ls;
    public GameObject desctopUI;

    void Start()
    {
        anti_cheat_cameras = GameObject.FindGameObjectsWithTag("Camera");
        foreach (GameObject camera in anti_cheat_cameras)
        {
            camera.SetActive(false);
        }
        playerCam = GameObject.FindGameObjectWithTag("PlayerCamera");
        //GetComponent<AntiCheatCamera>().enabled = false;
    }

    public void AсtiveAntiCheatCamers()
    {
        desctopUI.SetActive(false);
        anti_cheat_cameras[i].SetActive(true);
        playerCam.SetActive(false);
    }

    public void DisableAntiCheatCamers()
    {
        anti_cheat_cameras[i].SetActive(false);
        playerCam.SetActive(true);
        desctopUI.SetActive(true);
        ls.lable.SetActive(false);
    }

    public void SwapCamer(int a)
    {
        anti_cheat_cameras[i].SetActive(false);
        if (i + a == anti_cheat_cameras.Length)
            i = 0;
        else if (i + a == -1)
            i = anti_cheat_cameras.Length;

        i += a;
        anti_cheat_cameras[i].SetActive(true);
    }


}
