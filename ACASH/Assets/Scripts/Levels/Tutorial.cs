using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using N_BH;

public class Tutorial : Singleton<Tutorial>
{

    private void Awake()
    {
        StartCoroutine(StartLevel());

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
           StartCoroutine(EndLevel());
    }

    private IEnumerator StartLevel()
    {
        while (LevelManager.get.IsLoad())
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(1f);

        SubtitleManager.get.Say("Tutorial_Begining");

        while(SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Elevator.get.Open(false);
    }


    private IEnumerator EndLevel()
    {
        SubtitleManager.get.Say("Tutorial_Ending");

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Elevator.get.Open(true);
    }
}
