using UnityEngine;
using UnityEngine.Events;
using Vkimow.Unity.Tools.Single;
using Exam;
using System;

public class LevelSettings : MonoSingleton<LevelSettings>
{
    private enum nextExamMode
    {
        Auto,
        Manual,
    }

    public event Action OnExamNext;
    public event Action OnExamOver;



    [SerializeField]
    private nextExamMode _nextExam;

    [SerializeField]
    [Range(0, 20)]
    private int _examsCount;

    private int _examIndex;


    public void SetLevel()
    {
        _examIndex = 0;
    }

    public void RestartExam()
    {
        OnExamNext.Invoke();
    }

    public void NextExam()
    {
        if (_examIndex < _examsCount)
        {
            _examIndex++;
            OnExamNext.Invoke();
        }
        else
        {
            OnExamOver.Invoke();
        }
    }


}
