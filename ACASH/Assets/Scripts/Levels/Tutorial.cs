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
        ElevatorController.get.Close();

        while (LevelManager.get.IsLoad())
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(1f);

        GameManager.get.SetupManagers();
        SubtitleManager.get.Say("Tutorial_Begining");

        while(SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();


        ElevatorController.get.Ready();

        //Эту строчку потом нужно будет убрать!
        ElevatorController.get.Open();
    }


    private IEnumerator EndLevel()
    {
        SubtitleManager.get.Say("Tutorial_Ending");

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Elevator.get.Open();
    }
}
