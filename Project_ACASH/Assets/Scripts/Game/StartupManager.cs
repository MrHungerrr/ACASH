using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultiTasking;
using PostProcessing;
using Single;

public class StartupManager : MonoSingleton<StartupManager>
{

    private bool setuped = false;


    private void Awake()
    {
        Setup();
    }

    private void Start()
    {
        FuckThisShit();
    }

    private void FuckThisShit()
    {
        GeneralManager.Instance.SetLevel();
        FadeController.Instance.Fade(false);
        FadeHUDController.Instance.Fade(false);
        InputManager.SwitchGameInput(InputManager.GameplayType.FirstPerson);

        TimeManager.Instance.SetTime(300);
    }


    private void Setup()
    {
        if (!setuped)
        {
            setuped = true;
            PostProcessManager.Setup();
            InputManager.Setup();
            LevelDataManager.Setup();
            ScholarManager.Instance.Setup();
            ScholarObjectsManager.Instance.Setup();
            ThreadTaskQueuer.Setup();
        }
    }
}
