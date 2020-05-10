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

    private string setuped_level;


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
            ExamManager.get.Setup();
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
        StartCoroutine(ReloadGame());
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

        UnsetLevel();
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

    public IEnumerator ReloadGame()
    {
        Menu.get.MenuEnable(false);

        InputManager.get.SwitchGameInput("disable");
        FadeHUDController.get.Fade(true);
        UnsetLevel();

        while (FadeHUDController.get.active)
            yield return new WaitForEndOfFrame();

        LevelManager.get.Reload();

        /* while (!LevelManager.get.IsLoad(level))
         {
             Debug.Log(LevelManager.get.IsLoad(level));
             yield return new WaitForEndOfFrame();
         }
         */
        //END
    }



    public void SwitchLevel(LevelManager.levels level)
    {
        InputManager.get.SwitchGameInput("disable");
        FadeHUDController.get.FastFade(true);
        UnsetLevel();

        LevelManager.get.LoadInstead(level);
    }



    public void StartGame()
    {
        FadeController.get.Fade(false);
        FadeHUDController.get.Fade(false);
        InputManager.get.SwitchGameInput("gameplay");
    }

    public void StartLevel()
    {
        //Debug.LogError("Start Level");
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
        //Debug.LogError("SetLevel");
        ExamManager.get.SetLevel();
        PlaceManager.get.SetLevel();
        ScholarObjectsManager.get.SetLevel();
        ScholarManager.get.SetLevel();
        ScoreManager.get.SetLevel();
        OverwatchCameraManager.get.SetLevel();
        LevelSettings.get.Setup();
        ObjectManager.get.SetLevel();
        ComputerManager.get.Setup();
        TimeManager.get.Setup();
        SoundManager.get.SetLevel();

        if(!test)
            A_Level.get.StartLevel();

        yield return null;
    }

    public void UnsetLevel()
    {
        ExamManager.get.UnsetLevel();
        ObjectManager.get.UnsetLevel();
        SoundManager.get.UnsetLevel();
        ComputerManager.get.Unsetup();
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
