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
        StartCoroutine(LoadGame(LevelManager.levels.Tutorial_1));
    }

    public void Continue()
    {
        //
    }

    public void Restart()
    {
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



    public IEnumerator LoadGame(LevelManager.levels level)
    {
        game = true;
        InputManager.get.SwitchGameInput("disable");

        FadeController.get.Fade(true);

        while (FadeController.get.active)
            yield return new WaitForEndOfFrame();

        Menu.get.MenuEnable(false);

        LevelManager.get.Load(level);

        /* while (!LevelManager.get.IsLoad(level))
         {
             Debug.Log(LevelManager.get.IsLoad(level));
             yield return new WaitForEndOfFrame();
         }
         */
        //END
    }



    public IEnumerator SwitchLevel(LevelManager.levels level)
    {


        InputManager.get.SwitchGameInput("disable");
        FadeHUDController.get.FastFade(true);
        UnsetLevel();

        LevelManager.get.LoadInstead(level);

        yield return null;
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
        Debug.LogError("Start Level");
        //В конце концов это можно будет убрать (Костыль на игру без меню)
        if (!setuped)
            Setup();

        InputManager.get.SwitchGameInput("disable");
        FadeHUDController.get.FastFade(true);
        FadeController.get.FastFade(false);

       StartCoroutine(SetLevel());
    }

    

    public IEnumerator SetLevel()
    {
        Debug.LogError("SetLevel");

        PlaceManager.get.Setup();
        ScholarObjectsManager.get.SetLevel();
        ScholarManager.get.SetLevel();
        yield return new WaitForEndOfFrame();

        ScoreManager.get.SetLevel();
        yield return new WaitForEndOfFrame();

        ComputerManager.get.Setup();
        yield return new WaitForEndOfFrame();

        TimeManager.get.Setup();
        yield return new WaitForEndOfFrame();

        LevelSettings.get.Setup();
        yield return new WaitForEndOfFrame();

        OverwatchCameraManager.get.SetLevel();
        yield return new WaitForEndOfFrame();

        SoundManager.get.SetLevel();
        yield return new WaitForEndOfFrame();

        ObjectManager.get.SetLevel();
        yield return new WaitForEndOfFrame();

        A_Level.get.StartLevel();
    }

    public void UnsetLevel()
    {
        ObjectManager.get.UnsetLevel();
        SoundManager.get.UnsetLevel();
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
