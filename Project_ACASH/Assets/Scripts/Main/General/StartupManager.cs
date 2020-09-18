using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTime;
using GameTime.Action;
using Overwatch;
using MultiTasking;
using PostProcessing;
using Vkimow.Unity.Tools.Single;
using Computers;
using Exam;
using GOAP;
using AI.Scholars;
using System.Threading;

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

        ActionInTime.Create(20, action);
        ExamManager.Instance.ResetExam(10, 0, 2);
        ExamStarter.Instance.Interact();
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
            ThreadTaskQueuer.Setup();
            ComputerManager.Instance.Setup();
            MenuManager.Instance.Setup();
            ScholarManager.Instance.Setup();
        }
    }
}
