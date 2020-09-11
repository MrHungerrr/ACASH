using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostProcessing;
using UnityTools.Single;
using Exam;

public class GameManager : MonoSingleton<GameManager>
{

    public bool Game => _game;

    private bool _game = true;



    public void MainMenu()
    {
    }

    public void NewGame()
    {
        StartCoroutine(LoadGame(LevelManager.levels.Tutorial_1));
    }

    public void Continue()
    {
        MenuManager.Instance.Enable(false);
    }

    public void Restart()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }



    private IEnumerator LoadGame(LevelManager.levels level)
    {
        _game = true;
        InputManager.SwitchGameInput(InputManager.GameplayType.Disable);

        FadeController.Instance.Fade(true);

        while (FadeController.Instance.active)
            yield return new WaitForEndOfFrame();

        MenuManager.Instance.Enable(false);

        LevelManager.Instance.Load(level);

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
        InputManager.SwitchGameInput(InputManager.GameplayType.Disable);

        GeneralManager.Instance.UnsetLevel();

        LevelManager.Instance.LoadInstead(level);
    }



    public void StartGame()
    {
        FadeController.Instance.Fade(false);
        InputManager.SwitchGameInput(InputManager.GameplayType.FirstPerson);
    }

    public void StartLevel()
    {
        InputManager.SwitchGameInput(InputManager.GameplayType.Disable);
        FadeController.Instance.FastFade(false);

        GeneralManager.Instance.SetLevel();
    }


    public void StartExam()
    {
        ExamManager.Instance.ResetExam();
    }
}
