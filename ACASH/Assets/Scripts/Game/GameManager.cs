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
        StartCoroutine(LoadGame());
    }

    public void Continue()
    {
        //
    }

    public void Restart()
    {
        StartCoroutine(LoadGame());
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



    public IEnumerator LoadGame()
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

        LevelManager.get.LoadFast("Tutorial");

        while (!LevelManager.get.IsLoad("Tutorial"))
            yield return new WaitForEndOfFrame();

        //END
    }



    public void StartGame()
    {
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
