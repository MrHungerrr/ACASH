using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class GameManager : Singleton<GameManager>
{

    [HideInInspector]
    public bool game;
    [SerializeField]
    private bool test;


    private void Awake()
    {
        game = false;
        if (!test)
        {
            MainMenu();
        }
        else
        {
            game = true;
            Menu.get.MenuEnable(false);
            InputManager.get.SwitchGameInput("gameplay");
            SetLevelForTest();
        }
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
        StartCoroutine(RestartGame());
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
        InputManager.get.SwitchGameInput("disable");

        FadeController.get.Fade(true);

        while (FadeController.get.active)
            yield return new WaitForEndOfFrame();

        Menu.get.MenuEnable(false);
        Player.get.transform.position = new Vector3(0f, 0.2f, 0f);
        LevelManager.get.LoadFast("Elevator");
        LevelManager.get.LoadFast("Tutorial");

        while (!LevelManager.get.IsLoad("Elevator") || !LevelManager.get.IsLoad("Tutorial"))
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(0.1f);

        FadeController.get.Fade(false);
        InputManager.get.SwitchGameInput("gameplay");
    }

    public IEnumerator RestartGame()
    {
        game = true;
        InputManager.get.SwitchGameInput("disable");

        FadeController.get.Fade(true);

        while (FadeController.get.active)
            yield return new WaitForEndOfFrame();

        Menu.get.MenuEnable(false);
        Player.get.transform.position = new Vector3(0f, 0.2f, 0f);
        LevelManager.get.UnloadLevels();

        while (LevelManager.get.IsLoad())
            yield return new WaitForEndOfFrame();

        LevelManager.get.LoadFast("Elevator");
        LevelManager.get.LoadFast("Tutorial");

        while (!LevelManager.get.IsLoad("Elevator") || !LevelManager.get.IsLoad("Tutorial"))
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(0.1f);

        FadeController.get.Fade(false);
        InputManager.get.SwitchGameInput("gameplay");
    }



    public void SetLevelForTest()
    {
        PlaceManager.get.Setup();
        ScholarObjectsManager.get.Setup();
        ScholarManager.get.Setup();
        ScoreManager.get.Setup();
        ComputerManager.get.Setup();
        TimeManager.get.Setup();
        LevelSettings.get.Setup();
        //StartLevel();
    }

    public void SetLevel()
    {
        PlaceManager.get.Setup();
        ScholarObjectsManager.get.Setup();
        ScholarManager.get.Setup();
        ScoreManager.get.Setup();
        ComputerManager.get.Setup();
        TimeManager.get.Setup();
        LevelSettings.get.Setup();
    }



    public void StartLevel()
    {
        ExamManager.get.ResetExam();
    }

    public void ResetLevel()
    {
        PlaceManager.get.ResetLevel();
    }


    public void NewScholars()
    {
        ScholarManager.get.NewScholars();
        ComputerManager.get.SetScholars();
    }


}
