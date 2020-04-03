using System.Collections;
using PlayerOptions;
using UnityEngine;
using Single;

public class Level_2 : Singleton<Level_2>
{




    private void EndLevel()
    {
        FadeHUDController.get.FastFade(true);
        GameManager.get.MainMenu();
    }
}
