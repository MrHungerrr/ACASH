using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class GameManager : Singleton<GameManager>
{

    [HideInInspector]
    public bool game { get; private set; }
    [HideInInspector]
    private bool setuped = false;
    [SerializeField]
    private bool test;


    private void Awake()
    {

        Setup();

        game = false;
        if (!test)
        {
            MainMenu();
        }
        else
        {
            game = true;
            Menu.get.MenuEnable(false);
        }

        FadeController.get.Fade(false);
    }


    private void Setup()
    {
        if (!setuped)
        {
            setuped = true;
            ScoreManager.get.Setup();
            ScholarManager.get.Setup();
        }
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
        StartCoroutine(LoadGame("Tutorial_1"));
    }

    public void Continue()
    {
        //
    }

    public void Restart()
    {
        StartCoroutine(LevelManager.get.current_level);
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

        InputManager.get.SwitchGameInput("disable");

        Menu.get.MainMenu();

        LevelManager.get.UnloadLevels();

        InputManager.get.SwitchGameInput("menu");

        FadeHUDController.get.Fade(false);
        FadeController.get.Fade(false);
    }



    public IEnumerator LoadGame(string level)
    {
        game = true;
        InputManager.get.SwitchGameInput("disable");

        FadeController.get.Fade(true);

        while (FadeController.get.active)
            yield return new WaitForEndOfFrame();

        Menu.get.MenuEnable(false);
        LevelManager.get.UnloadLevels();

        while (LevelManager.get.IsLoad())
            yield return new WaitForEndOfFrame();

        LevelManager.get.LoadFast(level);

        while (!LevelManager.get.IsLoad(level))
            yield return new WaitForEndOfFrame();
        //END
    }



    public void StartGame()
    {
        FadeController.get.Fade(false);
        FadeHUDController.get.Fade(false);
        InputManager.get.SwitchGameInput("gameplay");
    }

    public void StartLevel()
    {
        //В конце концов это можно будет убрать (Костыль на игру без меню)
        if (!setuped)
            Setup();

        InputManager.get.SwitchGameInput("disable");
        FadeHUDController.get.FastFade(true);
        FadeController.get.FastFade(false);

        SetLevel();
    }

    public void SetLevel()
    {
        PlaceManager.get.Setup();
        ScholarObjectsManager.get.SetLevel();
        ScholarManager.get.SetLevel();
        ScoreManager.get.SetLevel();
        ComputerManager.get.Setup();
        TimeManager.get.Setup();
        LevelSettings.get.Setup();
        OverwatchCameraManager.get.SetLevel();
    }



    public void StartExam()
    {
        ExamManager.get.ResetExam();
    }


    public void NewScholars()
    {
        ScholarManager.get.NewScholars();
        ComputerManager.get.SetScholars();
    }


}
