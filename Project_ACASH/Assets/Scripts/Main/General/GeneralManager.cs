using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostProcessing;
using Places;
using GameTime;
using MultiTasking;
using Single;
using Overwatch;
using Computers;
using Supervision;

public class GeneralManager: MonoSingleton<GeneralManager>
{


    [SerializeField] private bool test;


    public void SetLevel()
    {
        TimeManager.Instance.SetLevel();
        ThreadTaskQueuer.SetLevel();
        ExamManager.Instance.SetLevel();
        PlaceManager.SetLevel();
        OverwatchManager.SetLevel();
        ScholarObjectsManager.Instance.SetLevel();
        ScholarManager.Instance.SetLevel();
        ClassManager.Instance.SetLevel();
        ComputerManager.Instance.SetLevel();
        TeacherProgramsManager.Instance.SetLevel();

        //SupervisionManager.Instance.SetLevel();
        //if (!test)
        //    A_Level.Instance.StartLevel();
    }

    public void UnsetLevel()
    {
        ExamManager.Instance.UnsetLevel();
        AudioManager.Instance.UnsetLevel();
    }
}
