using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;

public class CameraManager : Singleton<CameraManager>
{
    private CrossHair CrossHair;

    private void Awake()
    {
        CrossHair = GameObject.FindObjectOfType<CrossHair>();
    }


    public void Cross(bool u)
    {
        CrossHair.enabled = u;
    }



    public void GameplayType()
    {
        switch (InputManager.get.gameType)
        {
            case "gameplay":
                {
                    Cross(true);
                    break;
                }
            case "menu":
                {
                    Cross(false);
                    break;
                }
            case "computer":
                {
                    Cross(false);
                    break;
                }
            case "cutscene":
                {
                    Cross(false);
                    break;
                }
        }
    }
}
