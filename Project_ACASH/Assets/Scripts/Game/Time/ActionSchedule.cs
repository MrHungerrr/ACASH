using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class ActionSchedule: Singleton<ActionSchedule>
{
    private Dictionary<int, Action> _actionsSchedule;

    public void SetLevel()
    {
        _actionsSchedule = new Dictionary<int, Action>();
        TimeManager.Instance.OnSecondDone += TryInvokeAction;
    }


    public void AddActionInTime(int inTime, Action action)
    {
        int time = TimeManager.Instance.TimeInSec + inTime;
        AddActionAtTime(time, action);
    }


    public void AddActionAtTime(int time, Action action)
    {
        if(_actionsSchedule.ContainsKey(time))
        {
            _actionsSchedule[time] += action;
        }
        else
        {
            _actionsSchedule.Add(time, action);
        }
    }


    public void TryInvokeAction()
    {
        int time = TimeManager.Instance.TimeInSec;

        if (_actionsSchedule.ContainsKey(time))
        {
            _actionsSchedule[time]?.Invoke();
            _actionsSchedule[time] = null;
        }
    }
}







