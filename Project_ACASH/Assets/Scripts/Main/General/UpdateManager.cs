using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects._2D.Places;
using GameTime;
using MultiTasking;
using Vkimow.Unity.Tools.Single;
using Overwatch;
using UnityEngine.Events;
using AI.Scholars;

public class UpdateManager: MonoSingleton<UpdateManager>
{
    public event Action OnUpdate;
    public event Action OnFixUpdate;


    private void Update()
    {
        TimeManager.Instance.Update();
        ThreadTaskQueuer.Update();
        LevelManager.Instance.Update();
        ScholarManager.Instance.Update();
        OverwatchManager.Update();
        OnUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        ScholarManager.Instance.FixUpdate();
        OnFixUpdate?.Invoke();
    }
}
