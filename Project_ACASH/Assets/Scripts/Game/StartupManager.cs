using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTime;
using GameTime.Action;
using Overwatch;
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
        InputManager.SwitchGameInput(InputManager.GameplayType.FirstPerson);
        OverwatchManager.RecordStart();

        Action action = () =>
        {
            OverwatchManager.RecordStop();
        };

        ActionSchedule.Instance.AddActionInTime(10, action);
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
