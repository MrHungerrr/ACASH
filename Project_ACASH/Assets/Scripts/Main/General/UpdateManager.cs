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
using UnityEngine.Events;

public class UpdateManager: MonoSingleton<UpdateManager>
{
    private Action _onUpdate;
    private Action _onFixUpdate;


    private void Update()
    {
        TimeManager.Instance.Update();
        ThreadTaskQueuer.Update();
        LevelManager.Instance.Update();
        OverwatchManager.Update();
        _onUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        _onFixUpdate?.Invoke();
    }


    public void AddUpdate(Action action)
    {
        _onUpdate += action;
    }

    public void RemoveUpdate(Action action)
    {
        _onUpdate -= action;
    }

    public void AddFixUpdate(Action action)
    {
        _onFixUpdate += action;
    }

    public void RemoveFixUpdate(Action action)
    {
        _onFixUpdate -= action;
    }
}
