using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostProcessing;
using Places;
using GameTime;
using MultiTasking;
using Single;
using Overwatch;

public class GeneralManager: MonoSingleton<GeneralManager>
{

    [SerializeField]
    private bool test;


    private void Update()
    {
        TimeManager.Instance.Update();
        ThreadTaskQueuer.Update();
        OverwatchManager.Update();
    }

    public void SetLevel()
    {
        TimeManager.Instance.SetLevel();
        ThreadTaskQueuer.SetLevel();
        LevelDataManager.SetLevel();
        SoundManager.Instance.SetLevel();
        ExamManager.Instance.SetLevel();
        PlaceManager.SetLevel();
        OverwatchManager.SetLevel();
        ScholarObjectsManager.Instance.SetLevel();
        ScholarManager.Instance.SetLevel();
        ClassManager.SetLevel();
        OverwatchCameraManager.Instance.SetLevel();
        LevelSettings.Instance.SetLevel();
        ComputerManager.Instance.SetLevel();

        //if (!test)
        //    A_Level.Instance.StartLevel();
    }

    public void UnsetLevel()
    {
        ExamManager.Instance.UnsetLevel();
        SoundManager.Instance.UnsetLevel();
        ComputerManager.Instance.UnsetLevel();
    }
}
