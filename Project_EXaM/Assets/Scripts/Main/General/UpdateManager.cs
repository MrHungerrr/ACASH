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
        if(GameManager.Instance.Game)
        {
            TimeManager.Instance.Update();
            ScholarManager.Instance.Update();
            OverwatchManager.Update();
        }

        ThreadTaskQueuer.Update();
        OnUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.Game)
        {
            ScholarManager.Instance.FixUpdate();
        }

        OnFixUpdate?.Invoke();
    }
}
