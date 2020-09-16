using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostProcessing;
using Places;
using GameTime;
using MultiTasking;
using Vkimow.Unity.Tools.Single;
using Overwatch;
using Computers;
using AI.Scholars;
using Exam;

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
        ScholarManager.Instance.SetLevel();
        ClassManager.Instance.SetLevel();
        ComputerManager.Instance.SetLevel();

        //TeacherRoomManager.Instance.SetLevel();

        //if (!test)
        //    A_Level.Instance.StartLevel();
    }

    public void UnsetLevel()
    {
        ExamManager.Instance.UnsetLevel();
        AudioManager.Instance.UnsetLevel();
    }
}
