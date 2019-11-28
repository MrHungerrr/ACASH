using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;

public class GameManager : Singleton<GameManager>
{

    [HideInInspector]
    public bool game;


    private void Awake()
    {
        game = false;
        MainMenu();
        FadeController.get.Fade(false);
    }

    public void MainMenu()
    {
        if (game)
        {
            StartCoroutine(StartMenu());
        }
        else
        {
            Menu.get.MainMenu();
            InputManager.get.SwitchGameInput("menu");
        }

    }

    public void NewGame()
    {
        StartCoroutine(StartGame());
    }

    public void Continue()
    {
        //
    }

    public void Restart()
    {
        //
    }

    public void Quit()
    {
        Application.Quit();
    }


    public IEnumerator StartMenu()
    {
        game = false;

        FadeController.get.Fade(true);

        while (FadeController.get.active)
            yield return new WaitForEndOfFrame();

        Menu.get.MainMenu();
        InputManager.get.SwitchGameInput("menu");
        LevelManager.get.UnloadLevels();

        FadeController.get.Fade(false);
    }



    public IEnumerator StartGame()
    {
        game = true;
        Player.get.transform.position = new Vector3(0, 0.207f, 0);

        FadeController.get.Fade(true);

        while (FadeController.get.active)
            yield return new WaitForEndOfFrame();

        LevelManager.get.LoadFast("Elevator");

        Menu.get.MenuEnable(false);

        while (!LevelManager.get.IsLoad("Elevator"))
            yield return new WaitForEndOfFrame();

        FadeController.get.Fade(false);
        InputManager.get.SwitchGameInput("gameplay");

        while (FadeController.get.active)
            yield return new WaitForEndOfFrame();

        InputManager.get.SwitchGameInput("disable");
        LevelManager.get.LoadFast("Tutorial");

        while (!LevelManager.get.IsLoad("Tutorial"))
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(0.01f);

        InputManager.get.SwitchGameInput("gameplay");
    }



    public void SetupManagers()
    {
        ScholarManager.get.SetLevel();
        ScholarManager.get.SetScholars();
    }


}
