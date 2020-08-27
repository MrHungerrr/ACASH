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
using Computers;
using Supervision;

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
        InputManager.SwitchGameInput(InputManager.GameplayType.FirstPerson);

        OverwatchManager.RecordStart();

        Action action = () =>
        {
            OverwatchManager.RecordStop();
        };

        ActionSchedule.Instance.AddActionInTime(20, action);
    }


    private void Setup()
    {
        if (!setuped)
        {
            setuped = true;
            AudioManager.Instance.Setup();
            PostProcessManager.Setup();
            InputManager.Setup();
            OverwatchManager.Setup();
            ScholarManager.Instance.Setup();
            ScholarObjectsManager.Instance.Setup();
            ThreadTaskQueuer.Setup();
            ComputerManager.Instance.Setup();
            MenuManager.Instance.Setup();
        }
    }
}
