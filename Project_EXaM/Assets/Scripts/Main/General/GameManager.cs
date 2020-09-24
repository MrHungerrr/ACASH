using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostProcessing;
using Vkimow.Unity.Tools.Single;
using Exam;

public class GameManager : MonoSingleton<GameManager>
{
    public enum GameState
    {
        Main,
        Menu,
        Cutscene,
        Load
    }

    public bool Game => _game;

    private bool _game = false;




    public void SetGame(bool option)
    {
        _game = option;
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
    }
}
