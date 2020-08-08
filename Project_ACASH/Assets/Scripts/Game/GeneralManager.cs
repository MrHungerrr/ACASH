using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostProcessing;
using MultiTasking;
using Single;

public class GeneralManager: MonoSingleton<GeneralManager>
{

    [SerializeField]
    private bool test;


    private void Update()
    {
        TimeManager.Instance.Update();
        ThreadTaskQueuer.Update();
    }

    public void SetLevel()
    {
        PlaceManager.Instance.SetLevel();
        ExamManager.Instance.SetLevel();
        ScholarObjectsManager.Instance.SetLevel();
        ScholarManager.Instance.SetLevel();
        OverwatchCameraManager.Instance.SetLevel();
        LevelSettings.Instance.SetLevel();
        ComputerManager.Instance.SetLevel();
        TimeManager.Instance.SetLevel();
        SoundManager.Instance.SetLevel();
        ThreadTaskQueuer.SetLevel();
        LevelDataManager.SetLevel();

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
