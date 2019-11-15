using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using N_BH;

public class Tutorial : Singleton<Tutorial>
{

    private void Awake()
    {
        GameManager.get.gameReady = false;
        Debug.Log("Поехали");
    }
}
