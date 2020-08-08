using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostProcessing;
using Single;

public class GameManager : MonoSingleton<GameManager>
{

    public bool Game => _game;

    private bool _game = true;



    public void MainMenu()
    {
        if (Game)
        {
            StartCoroutine(StartMenu());
        }
        else
        {
            Menu.Instance.MainMenu();
            InputManager.SwitchGameInput(InputManager.GameplayType.Menu);
        }
    }

    public void NewGame()
    {
        StartCoroutine(LoadGame(LevelManager.levels.Tutorial_1));
    }

    public void Continue()
    {
        Menu.Instance.MenuEnable(false);
    }

    public void Restart()
    {
        StartCoroutine(ReloadGame());
    }

    public void Quit()
    {
        Application.Quit();
    }


    private IEnumerator StartMenu()
    {
        _game = false;

        FadeController.Instance.Fade(true);

        while (FadeController.Instance.active)
            yield return new WaitForEndOfFrame();

        InputManager.SwitchGameInput(InputManager.GameplayType.Disable);

        Menu.Instance.MainMenu();

        GeneralManager.Instance.UnsetLevel();
        LevelManager.Instance.UnloadLevels();

        InputManager.SwitchGameInput(InputManager.GameplayType.Menu);

        FadeHUDController.Instance.Fade(false);
        FadeController.Instance.Fade(false);
    }



    private IEnumerator LoadGame(LevelManager.levels level)
    {
        _game = true;
        InputManager.SwitchGameInput(InputManager.GameplayType.Disable);

        FadeController.Instance.Fade(true);

        while (FadeController.Instance.active)
            yield return new WaitForEndOfFrame();

        Menu.Instance.MenuEnable(false);

        LevelManager.Instance.Load(level);

        /* while (!LevelManager.get.IsLoad(level))
         {
             Debug.Log(LevelManager.get.IsLoad(level));
             yield return new WaitForEndOfFrame();
         }
         */
        //END
    }

    private IEnumerator ReloadGame()
    {
        Menu.Instance.MenuEnable(false);

        InputManager.SwitchGameInput(InputManager.GameplayType.Disable);
        FadeHUDController.Instance.Fade(true);
        GeneralManager.Instance.UnsetLevel();

        while (FadeHUDController.Instance.active)
            yield return new WaitForEndOfFrame();

        LevelManager.Instance.Reload();
    }



    public void SwitchLevel(LevelManager.levels level)
    {
        InputManager.SwitchGameInput(InputManager.GameplayType.Disable);
        FadeHUDController.Instance.FastFade(true);

        GeneralManager.Instance.UnsetLevel();

        LevelManager.Instance.LoadInstead(level);
    }



    public void StartGame()
    {
        FadeController.Instance.Fade(false);
        FadeHUDController.Instance.Fade(false);
        InputManager.SwitchGameInput(InputManager.GameplayType.FirstPerson);
    }

    public void StartLevel()
    {
        InputManager.SwitchGameInput(InputManager.GameplayType.Disable);
        FadeHUDController.Instance.FastFade(true);
        FadeController.Instance.FastFade(false);

        GeneralManager.Instance.SetLevel();
    }


    public void StartExam()
    {
        ExamManager.Instance.ResetExam();
    }


    public void NewScholars()
    {
        ScholarManager.Instance.NewScholars();
    }


}
