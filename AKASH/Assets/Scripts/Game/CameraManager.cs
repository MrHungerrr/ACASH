using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using N_BH;

public class CameraManager : Singleton<CameraManager>
{

    public void Cross(bool option)
    {
        CrossHair.get.Enable(option);
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
            case "doorlock":
                {
                    Cross(false);
                    break;
                }
            case "cutscene":
                {
                    Cross(false);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
